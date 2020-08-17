using Silk.NET.OpenGLES;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Safe;
using Nvidia.Nsight.Injection;
using Silk.NET.Core;
using Silk.NET.EGL;
using Silk.NET.GLFW;
using Silk.NET.Windowing.Common;
using Ultz.SuperInvoke;
using Ultz.SuperInvoke.Loader;
using ErrorCode = Silk.NET.GLFW.ErrorCode;
using Renderer = ImpromptuNinjas.UltralightSharp.Safe.Renderer;
using Window = Silk.NET.Windowing.Window;

partial class Program {

  static Program() {
    // ReSharper disable once InvertIf
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Windows.User32.SetProcessDPIAware();
      Windows.User32.SetProcessDpiAwarenessContext(Windows.User32.DpiAwarenessContext.PerMonitorAwareV2);
      Windows.ShCore.SetProcessDpiAwareness(Windows.ShCore.ProcessDpiAwareness.PerMonitorDpiAware);
    }
  }

  private static int _majOES = 3;

  private static readonly string AsmPath = new Uri(typeof(Program).Assembly.CodeBase!).LocalPath;

  private static readonly string AsmDir = Path.GetDirectoryName(AsmPath)!;

  private static readonly string AssetsDir = Path.Combine(AsmDir, "assets");

  private static IView _snView = null!;

  private static EGL _egl = null!;

  private static GL _gl = null!;

  private static uint _qvb;

  private static uint _qeb;

  private static uint _qva;

  private static uint _qpg;

  private static Renderer _ulRenderer = null!;

  private static Session _ulSession = null!;

  private static View _ulView = null!;

  private static Glfw _glfw;

  private static string _storagePath;

  private static bool _useOpenGL;

  private static unsafe void Main(string[] args) {
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
      Console.OutputEncoding = Encoding.UTF8;
      Ansi.WindowsConsole.TryEnableVirtualTerminalProcessing();
    }

    AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
    AppDomain.CurrentDomain.FirstChanceException += OnFirstChanceException;

    //InjectNsight();

    //InjectRenderDoc();

    var options = WindowOptions.Default;

    var size = new Size(1024, 576);

    var title = "UltralightSharp - OpenGL ES via ANGLE (Silk.NET)";

    options.Size = size;
    options.Title = title;
    options.VSync = VSyncMode.On;
    //options.VSync = true;

    /*
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
      var asmDir = Path.GetDirectoryName(new Uri(typeof(Program).Assembly.CodeBase!).LocalPath)!;
      var glfwPath = Path.Combine(asmDir, "libglfw.so.3");
      const string sysGlfwPath = "/lib/libglfw.so.3";
      if (File.Exists(sysGlfwPath)) {
        var sb = new StringBuilder(1024);
        var sbSize = (UIntPtr)sb.Capacity;
        var used = (long)Libc.readlink(glfwPath, sb, sbSize);
        if (used >= 0) {
          var link = sb.ToString(0, (int)(used - 1));
          if (link != sysGlfwPath) {
            File.Delete(glfwPath);
            Libc.symlink(sysGlfwPath, glfwPath);
          }
        }
        else {
          // not a link
          File.Delete(glfwPath);
          Libc.symlink(sysGlfwPath, glfwPath);
          Cleanup += () => {
            File.Delete(glfwPath);
          };
        }
      }
    }
    */

    _glfw = Glfw.GetApi();
    Console.WriteLine($"GLFW v{_glfw.GetVersionString()}");

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
      _glfw.InitHint(InitHint.CocoaMenubar, false);
      _glfw.InitHint(InitHint.CocoaChdirResources, false);
    }

    _glfw = GlfwProvider.GLFW.Value;

    _snView = Window.Create(options);

    _snView.Load += OnLoad;
    _snView.Render += OnRender;
    _snView.Update += OnUpdate;
    _snView.Closing += OnClose;
    _snView.Resize += OnResize;

    {
      // setup logging
      Ultralight.SetLogger(new Logger {LogMessage = LoggerCallback});

      var tempDir = Path.GetTempPath();
      // find a place to stash instance storage
      do _storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      while (Directory.Exists(_storagePath) || File.Exists(_storagePath));

      AppCore.EnablePlatformFontLoader();

      AppCore.EnablePlatformFileSystem(AssetsDir);
    }

    var glCtx = _snView.GLContext;

    Console.WriteLine("Loading LibEGL...");
    _egl = LibraryActivator.CreateInstance<EGL>
    (
      new UnmanagedLibrary(
        new CustomEglLibNameContainer().GetLibraryName(),
        LibraryLoader.GetPlatformDefaultLoader()
      ),
      Strategy.Strategy2
    );

    Console.WriteLine("Loading LibGLES...");
    _gl = LibraryActivator.CreateInstance<GL>
    (
      new CustomGlEsLibNameContainer().GetLibraryName(),
      TemporarySuperInvokeClass.GetLoader(glCtx)
    );

    Console.WriteLine("Checking for supported context...");
    
    for (;;) {

      SetGlfwWindowHints();

      Console.WriteLine(
        _useOpenGL
          ? "Attempting OpenGL v3.2 (Core)"
          : $"Attempting OpenGL ES v{_majOES}.0");
      var wh = _glfw.CreateWindow(1024, 576, title, null, null);
      if (wh != null) {
        Console.WriteLine(
          _useOpenGL
            ? "Created Window with OpenGL v3.2 (Core)"
            : $"Created Window with OpenGL ES v{_majOES}.0");
        _glfw.DestroyWindow(wh);
        break;
      }

      var code = _glfw.GetError(out char* pDesc);
      if (code == ErrorCode.NoError || pDesc == null)
        throw new PlatformNotSupportedException("Can't create a window via GLFW. Unknown error.");

      var strLen = new ReadOnlySpan<byte>((byte*) pDesc, 32768).IndexOf<byte>(0);
      if (strLen == -1) strLen = 0;
      var str = new string((sbyte*) pDesc, 0, strLen, Encoding.UTF8);
      var errMsg = $"{code}: {str}";
      Console.Error.WriteLine(errMsg);
      if (code != ErrorCode.VersionUnavailable)
        throw new GlfwException(errMsg);

      // attempt sequence: OpenGL ES 3.0, OpenGL 3.2, OpenGL ES 2.0
      if (!_useOpenGL && _majOES == 3)
        _useOpenGL = true;
      else if (_majOES == 3 && _useOpenGL) {
        _useOpenGL = false;
        _majOES = 2;
      }
      else
        throw new GlfwException(errMsg);
    }

    if (_useOpenGL)
      options.API = new GraphicsAPI(
        ContextAPI.OpenGL,
        ContextProfile.Core,
        ContextFlags.ForwardCompatible,
        new APIVersion(3, 2)
      );
    else
      options.API = new GraphicsAPI(
        ContextAPI.OpenGLES,
        ContextProfile.Core,
        ContextFlags.ForwardCompatible,
        new APIVersion(_majOES, 0)
      );

    Console.WriteLine("Initializing window...");

    //SetGlfwWindowHints();
    
    _glfw.DefaultWindowHints();
    _snView.Initialize();

    if (_snView.Handle == null) {
      var code = _glfw.GetError(out char* pDesc);
      if (code == ErrorCode.NoError || pDesc == null)
        throw new PlatformNotSupportedException("Can't create a window via GLFW. Unknown error.");

      var strLen = new ReadOnlySpan<byte>((byte*) pDesc, 32768).IndexOf<byte>(0);
      if (strLen == -1) strLen = 0;
      var str = new string((sbyte*) pDesc, 0, strLen, Encoding.UTF8);
      throw new GlfwException($"{code}: {str}");
    }

    Console.WriteLine("Starting main loop...");
    _snView.Run();
  }

  private static void SetGlfwWindowHints() {
    _glfw.DefaultWindowHints();
    if (_useOpenGL) {
      _glfw.WindowHint(WindowHintContextApi.ContextCreationApi, ContextApi.NativeContextApi);
      _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
      _glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

      _glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
      _glfw.WindowHint(WindowHintInt.ContextVersionMinor, 2);
    }
    else {
      _glfw.WindowHint(WindowHintContextApi.ContextCreationApi, ContextApi.EglContextApi);
      _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGLES);

      _glfw.WindowHint(WindowHintInt.ContextVersionMajor, _majOES);
      _glfw.WindowHint(WindowHintInt.ContextVersionMinor, 0);
    }
  }

  public static event Action Cleanup;

  private static unsafe void OnFirstChanceException(object? sender, FirstChanceExceptionEventArgs eventArgs) {
    var sf = new StackFrame(1, true);
    Console.Error.WriteLine("First-Chance Exception Stack Frame:");
    Console.Error.WriteLine(sf.ToString());
    Console.Error.WriteLine($"First-Chance Exception: {eventArgs.Exception.GetType().Name}: {eventArgs.Exception.Message}");
  }

  private static unsafe void OnUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs) {
    var st = new StackTrace(1, true);
    Console.Error.WriteLine("Unhandled Exception Entry Stack:");
    Console.Error.WriteLine(st.ToString());
    st = new StackTrace((Exception) eventArgs.ExceptionObject, 0, true);
    Console.Error.WriteLine("Unhandled Exception:");
    Console.Error.WriteLine(st.ToString());
    if (eventArgs.IsTerminating) {
      Cleanup?.Invoke();
    }
  }

  private static unsafe void OnResize(Size size) {
    _ulView.Resize((uint) size.Width, (uint) size.Height);
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
    _dbg.ObjectLabel(objId, vao, (uint) name.Length, name);
    CheckGl();
  }

  private static void OnUpdate(double obj) {
    _ulRenderer.Update();
  }

  private static void OnClose() {
    //Remember to delete the buffers.
    _ulView.Dispose();
    _ulRenderer.Dispose();
    _ulSession.Dispose();
    _gl.DeleteBuffer(_qvb);
    _gl.DeleteBuffer(_qeb);
    _gl.DeleteVertexArray(_qva);
    _gl.DeleteProgram(_qpg);
  }

  private static void CheckGl([CallerLineNumber] int lineNumber = default) {
#if DEBUG
    var error = _gl.GetError();
    if (error == default)
      return;

    Console.Error.WriteLine($"Line {lineNumber}, GL Error: {error}");
    Console.Error.Flush();
    //Debugger.Break();
#endif
  }

}