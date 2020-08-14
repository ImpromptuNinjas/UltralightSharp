using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Safe;
using ImpromptuNinjas.UltralightSharp.Enums;
using Nvidia.Nsight.Injection;
using Silk.NET.GLFW;
using Silk.NET.OpenGL.Extensions.KHR;
using SixLabors.ImageSharp.PixelFormats;

partial class Program {

  static Program() {
    // ReSharper disable once InvertIf
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Windows.User32.SetProcessDPIAware();
      Windows.User32.SetProcessDpiAwarenessContext(Windows.User32.DpiAwarenessContext.PerMonitorAwareV2);
      Windows.ShCore.SetProcessDpiAwareness(Windows.ShCore.ProcessDpiAwareness.PerMonitorDpiAware);
    }
  }

  private static IWindow _wnd = null!;

  private static GL _gl = null!;

  private static uint _qvb;

  private static uint _qeb;

  private static uint _qva;

  private static uint _qpg;

  private static uint _fillShader;

  private static uint _fillPathShader;

  private static readonly SlottingList<GeometryEntry> GeometryEntries
    = new SlottingList<GeometryEntry>(32, 8);

  private static readonly SlottingList<TextureEntry> TextureEntries
    = new SlottingList<TextureEntry>(32, 8);

  private static readonly SlottingList<RenderBufferEntry> RenderBufferEntries
    = new SlottingList<RenderBufferEntry>(8, 2);

  private static int _indicesSize;

  private static Renderer _renderer = null!;

  private static Session _session = null!;

  private static View _view = null!;

  private static List<Command> _commands = new List<Command>();

  private static unsafe void Main(string[] args) {
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Console.OutputEncoding = Encoding.UTF8;
      Ansi.WindowsConsole.TryEnableVirtualTerminalProcessing();
    }

    //InjectNsight();

    //InjectRenderDoc();
    if (Silk.NET.Windowing.Window.IsGlfw || Silk.NET.Windowing.Window.Platform == null) {
      var glfw = GlfwProvider.GLFW.Value;
      //glfw.InitHint(InitHint.CocoaMenubar, true);
      //glfw.InitHint(InitHint.CocoaChdirResources, true);
      if (!glfw.Init()) {
        var code = glfw.GetError(out var pDesc);
        var str = new string(pDesc);
        throw new GlfwException($"GLFW Init failed, {code}: {str}");
      }

      Console.WriteLine("GLFW Init Succeeded.");
    }

    Silk.NET.Windowing.Window.Init();

    Console.WriteLine($"Windowing Platform: {Silk.NET.Windowing.Window.Platform!.GetType().Name}");

    var options = WindowOptions.Default;
    options.API = new GraphicsAPI(
      ContextAPI.OpenGL,
      ContextProfile.Core,
      ContextFlags.ForwardCompatible,
      new APIVersion(3, 2)
    );
    options.Size = new Size(1024, 576);
    options.Title = "UltralightSharp - OpenGL (Silk.NET)";
    options.VSync = VSyncMode.On;
    _wnd = Silk.NET.Windowing.Window.Create(options);

    _wnd.Load += OnLoad;
    _wnd.Render += OnRender;
    _wnd.Update += OnUpdate;
    _wnd.Closing += OnClose;
    _wnd.Resize += OnResize;

    {
      // setup logging
      Ultralight.SetLogger(new Logger {LogMessage = LoggerCallback});

      var asmPath = new Uri(typeof(Program).Assembly.CodeBase!).LocalPath;
      var asmDir = Path.GetDirectoryName(asmPath)!;
      var tempDir = Path.GetTempPath();
      // find a place to stash instance storage
      string storagePath;
      do storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      while (Directory.Exists(storagePath) || File.Exists(storagePath));

      using var cfg = new Config();

      var cachePath = Path.Combine(storagePath, "Cache");
      cfg.SetCachePath(cachePath);

      var resourcePath = Path.Combine(asmDir, "resources");
      cfg.SetResourcePath(resourcePath);

      cfg.SetUseGpuRenderer(true);
      cfg.SetEnableImages(true);
      cfg.SetEnableJavaScript(false);

      //cfg.SetForceRepaint(true);

      Ultralight.SetGpuDriver(new GpuDriver {
        BeginSynchronize = () => {
          //Console.WriteLine("BeginSynchronize");
        },
        EndSynchronize = () => {
          //Console.WriteLine("EndSynchronize");
        },
        NextGeometryId = () => {
          var id = (uint) GeometryEntries.Add(new GeometryEntry()) + 1;
          Console.WriteLine($"NextGeometryId: {id}");
          return id;
        },
        CreateGeometry = (id, vertices, indices) => {
          Console.WriteLine($"CreateGeometry: {id}");
          var index = (int) id - 1;

          var entry = GeometryEntries[index];

          CheckGl();

          var vao = _gl.GenVertexArray();
          entry.VertexArray = vao;
          _gl.BindVertexArray(vao);
          //LabelObject(ObjectIdentifier.VertexArray, vao, $"Ultralight Geometry VAO {id}");
          CheckGl();

          var buf = _gl.GenBuffer();
          entry.Vertices = buf;
          _gl.BindBuffer(BufferTargetARB.ArrayBuffer, buf);
          LabelObject(ObjectIdentifier.Buffer, buf, $"Ultralight Geometry VBO {id}");
          _gl.BufferData(BufferTargetARB.ArrayBuffer, vertices.Size, vertices.DataSpan, BufferUsageARB.DynamicDraw);
          CheckGl();

          switch (vertices.Format) {
            case VertexBufferFormat._2F4Ub2F2F28F: {
              const uint stride = 140;

              _gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, (void*) 0);
              _gl.VertexAttribPointer(1, 4, VertexAttribPointerType.UnsignedByte, true, stride, (void*) 8);
              _gl.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, stride, (void*) 12);
              _gl.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, stride, (void*) 20);
              _gl.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, stride, (void*) 28);
              _gl.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, stride, (void*) 44);
              _gl.VertexAttribPointer(6, 4, VertexAttribPointerType.Float, false, stride, (void*) 60);
              _gl.VertexAttribPointer(7, 4, VertexAttribPointerType.Float, false, stride, (void*) 76);
              _gl.VertexAttribPointer(8, 4, VertexAttribPointerType.Float, false, stride, (void*) 92);
              _gl.VertexAttribPointer(9, 4, VertexAttribPointerType.Float, false, stride, (void*) 108);
              _gl.VertexAttribPointer(10, 4, VertexAttribPointerType.Float, false, stride, (void*) 124);
              CheckGl();

              _gl.EnableVertexAttribArray(0);
              _gl.EnableVertexAttribArray(1);
              _gl.EnableVertexAttribArray(2);
              _gl.EnableVertexAttribArray(3);
              _gl.EnableVertexAttribArray(4);
              _gl.EnableVertexAttribArray(5);
              _gl.EnableVertexAttribArray(6);
              _gl.EnableVertexAttribArray(7);
              _gl.EnableVertexAttribArray(8);
              _gl.EnableVertexAttribArray(9);
              _gl.EnableVertexAttribArray(10);
              CheckGl();
              break;
            }
            case VertexBufferFormat._2F4Ub2F: {
              const uint stride = 20;

              _gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, (void*) 0);
              _gl.VertexAttribPointer(1, 4, VertexAttribPointerType.UnsignedByte, true, stride, (void*) 8);
              _gl.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, stride, (void*) 12);
              CheckGl();

              _gl.EnableVertexAttribArray(0);
              _gl.EnableVertexAttribArray(1);
              _gl.EnableVertexAttribArray(2);
              CheckGl();
              break;
            }
            default: throw new NotImplementedException(vertices.Format.ToString());
          }

          buf = _gl.GenBuffer();
          entry.Indices = buf;
          _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, buf);
          LabelObject(ObjectIdentifier.Buffer, buf, $"Ultralight Geometry EBO {id}");
          _gl.BufferData(BufferTargetARB.ElementArrayBuffer, indices.Size, indices.Data, BufferUsageARB.StaticDraw);
          CheckGl();
        },
        UpdateGeometry = (id, vertices, indices) => {
          Console.WriteLine($"UpdateGeometry: {id}");
          var index = (int) id - 1;

          var entry = GeometryEntries[index];

          CheckGl();

          _gl.BindVertexArray(entry.VertexArray);
          CheckGl();

          _gl.BindBuffer(BufferTargetARB.ArrayBuffer, entry.Vertices);
          _gl.BufferData(BufferTargetARB.ArrayBuffer, vertices.Size, vertices.DataSpan, BufferUsageARB.DynamicDraw);
          CheckGl();

          _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, entry.Vertices);
          _gl.BufferData(BufferTargetARB.ElementArrayBuffer, vertices.Size, vertices.DataSpan, BufferUsageARB.DynamicDraw);
          CheckGl();

          _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, entry.Indices);
          _gl.BufferData(BufferTargetARB.ElementArrayBuffer, indices.Size, indices.Data, BufferUsageARB.StaticDraw);
          CheckGl();
        },
        DestroyGeometry = id => {
          Console.WriteLine($"DestroyGeometry: {id}");
          var index = (int) id - 1;
          var entry = GeometryEntries.RemoveAt(index);
          _gl.DeleteBuffer(entry.Indices);
          _gl.DeleteBuffer(entry.Vertices);
          _gl.DeleteVertexArray(entry.VertexArray);
          CheckGl();
        },
        NextRenderBufferId = () => {
          var id = (uint) RenderBufferEntries.Add(new RenderBufferEntry()) + 1;
          Console.WriteLine($"NextRenderBufferId: {id}");
          return id;
        },
        CreateRenderBuffer = (id, buffer) => {
          Console.WriteLine($"CreateRenderBuffer: {id}");
          var index = (int) id - 1;
          var entry = RenderBufferEntries[index];
          CheckGl();

          var fb = _gl.GenFramebuffer();
          entry.FrameBuffer = fb;
          _gl.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
          LabelObject(ObjectIdentifier.Framebuffer, fb, $"Ultralight RenderBuffer {id}");
          CheckGl();

          var texIndex = (int) buffer.TextureId - 1;
          var texEntry = TextureEntries[texIndex];
          var tex = texEntry.Texure;
          _gl.BindTexture(TextureTarget.Texture2D, tex);
          _gl.FramebufferTexture2D(
            FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
            TextureTarget.Texture2D, tex, 0);
          entry.TextureEntry = texEntry;
          CheckGl();

          _gl.DrawBuffers(1, stackalloc[] {DrawBufferMode.ColorAttachment0});
          var result = _gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
          if (result != GLEnum.FramebufferComplete) {
            Console.Error.WriteLine($"Error creating FBO: {result}");
            Console.Error.Flush();
            //Debugger.Break();
          }

          CheckGl();
        },
        DestroyRenderBuffer = id => {
          Console.WriteLine($"DestroyRenderBuffer: {id}");
          var index = (int) id - 1;
          var entry = RenderBufferEntries.RemoveAt(index);
          _gl.DeleteFramebuffer(entry.FrameBuffer);
          CheckGl();
        },
        NextTextureId = () => {
          var id = (uint) TextureEntries.Add(new TextureEntry()) + 1;
          Console.WriteLine($"NextTextureId: {id}");
          return id;
        },
        CreateTexture = (id, bitmap) => {
          var index = (int) id - 1;
          var entry = TextureEntries[index];
          var texWidth = bitmap.GetWidth();
          entry.Width = texWidth;
          var texHeight = bitmap.GetHeight();
          entry.Height = texHeight;
          Console.WriteLine($"CreateTexture: {id} {texWidth}x{texHeight}");

          var tex = _gl.GenTexture();
          entry.Texure = tex;
          _gl.ActiveTexture(TextureUnit.Texture0);
          _gl.BindTexture(TextureTarget.Texture2D, tex);

          var linear = (int) GLEnum.Linear;
          _gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref linear);
          _gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref linear);
          CheckGl();

          var clampToEdge = (int) GLEnum.ClampToEdge;
          _gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ref clampToEdge);
          _gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ref clampToEdge);
          CheckGl();

          if (bitmap.IsEmpty()) {
            LabelObject(ObjectIdentifier.Texture, tex, $"Ultralight Texture {id} (RT)");
            CheckGl();

            _gl.TexImage2D(TextureTarget.Texture2D, 0,
              (int) InternalFormat.Rgba8, texWidth, texHeight, 0,
              PixelFormat.Rgba, PixelType.UnsignedByte,
              default);
            CheckGl();

            _gl.GenerateMipmap(TextureTarget.Texture2D);
            CheckGl();
          }
          else {
            LabelObject(ObjectIdentifier.Texture, tex, $"Ultralight Texture {id}");
            CheckGl();

            _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            _gl.PixelStore(PixelStoreParameter.UnpackRowLength, (int) (bitmap.GetRowBytes() / bitmap.GetBpp()));
            CheckGl();

            var format = bitmap.GetFormat();

            var pixels = (void*) bitmap.LockPixels();

            switch (format) {
              case BitmapFormat.A8UNorm: {
                _gl.TexImage2D(TextureTarget.Texture2D, 0,
                  (int) InternalFormat.R8, texWidth, texHeight, 0,
                  PixelFormat.Red, PixelType.UnsignedByte, pixels);
                Utilities.RenderAnsi<L8>(pixels, texWidth, texHeight, 1, 20);
                break;
              }
              case BitmapFormat.Bgra8UNormSrgb: {
                _gl.TexImage2D(TextureTarget.Texture2D, 0,
                  (int) InternalFormat.Srgb8Alpha8, texWidth, texHeight, 0,
                  PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
                Utilities.RenderAnsi<Rgba32>(pixels, texWidth, texHeight, 1, 20);
                break;
              }
              default: throw new ArgumentOutOfRangeException(nameof(BitmapFormat));
            }

            CheckGl();

            bitmap.UnlockPixels();
            _gl.GenerateMipmap(TextureTarget.Texture2D);
            CheckGl();
          }
        },
        UpdateTexture = (id, bitmap) => {
          Console.WriteLine($"UpdateTexture: {id}");
          var index = (int) id - 1;
          var entry = TextureEntries[index];
          var tex = entry.Texure;
          //var width = bitmap.GetWidth();
          var texWidth = entry.Width;
          //var height = bitmap.GetHeight();
          var texHeight = entry.Height;
          CheckGl();

          _gl.ActiveTexture(TextureUnit.Texture0);
          CheckGl();
          _gl.BindTexture(TextureTarget.Texture2D, tex);
          CheckGl();

          _gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
          _gl.PixelStore(PixelStoreParameter.UnpackRowLength, (int) (bitmap.GetRowBytes() / bitmap.GetBpp()));
          CheckGl();

          var pixels = bitmap.LockPixels();

          var format = bitmap.GetFormat();
          switch (format) {
            case BitmapFormat.A8UNorm: {
              _gl.TexImage2D(TextureTarget.Texture2D, 0,
                (int) InternalFormat.R8, texWidth, texHeight, 0,
                PixelFormat.Red, PixelType.UnsignedByte, (void*) pixels);
              break;
            }
            case BitmapFormat.Bgra8UNormSrgb: {
              _gl.TexImage2D(TextureTarget.Texture2D, 0,
                (int) InternalFormat.Srgb8Alpha8, texWidth, texHeight, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, (void*) pixels);
              break;
            }
            default: throw new ArgumentOutOfRangeException(nameof(BitmapFormat));
          }

          CheckGl();

          bitmap.UnlockPixels();
          _gl.GenerateMipmap(TextureTarget.Texture2D);
          CheckGl();
        },
        DestroyTexture = id => {
          Console.WriteLine($"DestroyTexture: {id}");
          var index = (int) id - 1;
          var entry = TextureEntries.RemoveAt(index);
          _gl.DeleteTexture(entry.Texure);
          CheckGl();
        },
        UpdateCommandList = list => {
          Console.WriteLine("UpdateCommandList");
          _commands.AddRange(list);
        }
      });

      AppCore.EnablePlatformFontLoader();

      AppCore.EnablePlatformFileSystem(Path.Combine(asmDir, "assets"));

      _renderer = new Renderer(cfg);
      _session = new Session(_renderer, false, "Demo");

      var wndSize = _wnd.Size;
      var width = (uint) wndSize.Width;
      var height = (uint) wndSize.Height;
      _view = new View(_renderer, width, height, false, _session);
      _view.SetAddConsoleMessageCallback(ConsoleMessageCallback, default);
    }

    _wnd.Run();
  }

  private static unsafe void OnResize(Size size) {
    _view.Resize((uint) size.Width, (uint) size.Height);
  }

  private static void InjectRenderDoc() {
    try {
      Console.WriteLine("Injecting RenderDoc...");
      using var proc = Process.GetCurrentProcess();
      var ps = Process.Start(@"\\?\C:\Program Files\RenderDoc\renderdoccmd.exe", $"inject --PID={proc.Id} --opt-disallow-vsync --opt-disallow-fullscreen --opt-ref-all-resources");
      ps?.WaitForExit(250);
      //Process.Start(@"\\?\C:\Program Files\RenderDoc\qrenderdoc.exe", $"inject --PID={proc.Id}");
    }
    catch (Exception ex) {
      Console.Error.WriteLine("Couldn't inject RenderDoc");
      Console.Error.WriteLine(ex.ToString());
      Console.Error.Flush();
    }
  }

  private static unsafe void InjectNsight() {
    try {
      Console.WriteLine("Injecting Nsight...");
      NativeLibrary.Load(@"C:\Program Files\NVIDIA Corporation\Nsight Graphics 2020.3.1\SDKs\NsightGraphicsSDK\0.7.0\lib\x64\NGFX_Injection.dll");
      var installCount = 0;
      var r = Nsight.EnumerateInstallations((uint*) &installCount, null);
      if (r != NsightInjectionResult.Ok) throw new Exception(r.ToString());

      var installs = stackalloc NsightInjectionInstallationInfo[installCount];
      r = Nsight.EnumerateInstallations((uint*) &installCount, installs);
      if (r != NsightInjectionResult.Ok) throw new Exception(r.ToString());

      var injected = false;
      for (var x = 0; x < installCount; ++x) {
        var activityCount = 0;
        var install = &installs[x];
        r = Nsight.EnumerateActivities(install, (uint*) &activityCount, null);
        if (r != NsightInjectionResult.Ok) throw new Exception(r.ToString());

        // ReSharper disable once StackAllocInsideLoop
        var activities = stackalloc NsightInjectionActivity[activityCount];
        r = Nsight.EnumerateActivities(install, (uint*) &activityCount, activities);
        if (r != NsightInjectionResult.Ok) throw new Exception(r.ToString());

        for (var i = 0; i < activityCount; ++i) {
          var activity = &activities[i];
          if (activity->Type != NsightInjectionActivityType.FrameDebugger)
            continue;

          r = Nsight.InjectToProcess(install, activity);
          if (r != NsightInjectionResult.Ok) throw new Exception(r.ToString());

          injected = true;
          break;
        }

        if (injected)
          break;
      }

      if (!injected)
        throw new Exception("Found no injectable FrameDebugger activity.");
    }
    catch (Exception ex) {
      Console.Error.WriteLine("Couldn't inject Nsight");
      Console.Error.WriteLine(ex.ToString());
      Console.Error.Flush();
    }
  }

  private static void LabelObject(ObjectIdentifier objId, uint vao, string name) {
    _gl.ObjectLabel(objId, vao, (uint) name.Length, name);
    CheckGl();
  }

  private static void OnUpdate(double obj) {
    _renderer.Update();
  }

  private static void OnClose() {
    //Remember to delete the buffers.
    _view.Dispose();
    _renderer.Dispose();
    _session.Dispose();
    _gl.DeleteBuffer(_qvb);
    _gl.DeleteBuffer(_qeb);
    _gl.DeleteVertexArray(_qva);
    _gl.DeleteProgram(_qpg);
  }

  private static void CheckGl([CallerLineNumber] int lineNumber = default) {
    var error = _gl.GetError();
    if (error == default)
      return;

    Console.Error.WriteLine($"Line {lineNumber}, GL Error: {error}");
    Console.Error.Flush();
    //Debugger.Break();
  }

}