using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ImpromptuNinjas.UltralightSharp.Safe;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;
using Silk.NET.OpenGLES;
using Silk.NET.OpenGLES.Extensions.KHR;
using Silk.NET.Windowing.Common;
using SixLabors.ImageSharp.PixelFormats;
using ShaderType = Silk.NET.OpenGLES.ShaderType;
using ULShaderType = ImpromptuNinjas.UltralightSharp.Enums.ShaderType;

public class OpenGlEsGpuDriverSite {

  public class GeometryEntry {

    public uint VertexArray { get; set; }

    public uint Vertices { get; set; }

    public uint Indices { get; set; }

  }

  public class RenderBufferEntry {

    public uint FrameBuffer { get; set; }

    public TextureEntry TextureEntry { get; set; } = null!;

  }

  public class TextureEntry {

    public uint Texure { get; set; }

    public uint Width { get; set; }

    public uint Height { get; set; }

  }

  private static readonly SlottingList<GeometryEntry> GeometryEntries
    = new SlottingList<GeometryEntry>(32, 8);

  private static readonly SlottingList<TextureEntry> TextureEntries
    = new SlottingList<TextureEntry>(32, 8);

  private static readonly SlottingList<RenderBufferEntry> RenderBufferEntries
    = new SlottingList<RenderBufferEntry>(8, 2);

  private readonly GL _gl;

  private readonly KhrDebug? _dbg;

  private uint _fillShader;

  private uint _fillPathShader;

  private Queue<Command> _queuedCommands = new Queue<Command>();

  [PublicAPI]
  public bool RenderAnsiTexturePreviews;

  public OpenGlEsGpuDriverSite(GL gl, KhrDebug? dbg = null) {
    _gl = gl;
    _dbg = dbg;
  }

  internal unsafe GpuDriver CreateGpuDriver() {
    var gpuDriver = new GpuDriver {
      BeginSynchronize = BeginSynchronize,
      EndSynchronize = EndSynchronize,
      NextGeometryId = NextGeometryId,
      CreateGeometry = CreateGeometry,
      UpdateGeometry = UpdateGeometry,
      DestroyGeometry = DestroyGeometry,
      NextRenderBufferId = NextRenderBufferId,
      CreateRenderBuffer = CreateRenderBuffer,
      DestroyRenderBuffer = DestroyRenderBuffer,
      NextTextureId = NextTextureId,
      CreateTexture = CreateTexture,
      UpdateTexture = UpdateTexture,
      DestroyTexture = DestroyTexture,
      UpdateCommandList = UpdateCommandList
    };
    return gpuDriver;
  }

  private unsafe void UpdateCommandList(CommandList list) {
    Console.WriteLine("UpdateCommandList");
    foreach (var cmd in list)
      _queuedCommands.Enqueue(cmd);
  }

  private unsafe void DestroyTexture(uint id) {
    Console.WriteLine($"DestroyTexture: {id}");
    var index = (int) id - 1;
    var entry = TextureEntries.RemoveAt(index);
    _gl.DeleteTexture(entry.Texure);
    CheckGl();
  }

  private unsafe void UpdateTexture(uint id, Bitmap bitmap) {
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
        _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.R8, texWidth, texHeight, 0, PixelFormat.Red, PixelType.UnsignedByte, (void*) pixels);
        break;
      }
      case BitmapFormat.Bgra8UNormSrgb: {
        _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Srgb8Alpha8, texWidth, texHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, (void*) pixels);
        break;
      }
      default: throw new ArgumentOutOfRangeException(nameof(BitmapFormat));
    }

    CheckGl();

    bitmap.UnlockPixels();
    _gl.GenerateMipmap(TextureTarget.Texture2D);
    CheckGl();
  }

  private unsafe void CreateTexture(uint id, Bitmap bitmap) {
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
    _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, ref linear);
    _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, ref linear);
    CheckGl();

    var clampToEdge = (int) GLEnum.ClampToEdge;
    _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ref clampToEdge);
    _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ref clampToEdge);
    CheckGl();

    if (bitmap.IsEmpty()) {
      LabelObject(ObjectIdentifier.Texture, tex, $"Ultralight Texture {id} (RT)");
      CheckGl();

      _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba8, texWidth, texHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, default);
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
          _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.R8, texWidth, texHeight, 0, PixelFormat.Red, PixelType.UnsignedByte, pixels);
          if (RenderAnsiTexturePreviews)
            Utilities.RenderAnsi<L8>(pixels, texWidth, texHeight, 1, 20);
          break;
        }
        case BitmapFormat.Bgra8UNormSrgb: {
          _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Srgb8Alpha8, texWidth, texHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
          if (RenderAnsiTexturePreviews)
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
  }

  private unsafe uint NextTextureId() {
    var id = (uint) TextureEntries.Add(new TextureEntry()) + 1;
    Console.WriteLine($"NextTextureId: {id}");
    return id;
  }

  private unsafe void DestroyRenderBuffer(uint id) {
    Console.WriteLine($"DestroyRenderBuffer: {id}");
    var index = (int) id - 1;
    var entry = RenderBufferEntries.RemoveAt(index);
    _gl.DeleteFramebuffer(entry.FrameBuffer);
    CheckGl();
  }

  private unsafe void CreateRenderBuffer(uint id, RenderBuffer buffer) {
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
    _gl.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, tex, 0);
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
  }

  private unsafe uint NextRenderBufferId() {
    var id = (uint) RenderBufferEntries.Add(new RenderBufferEntry()) + 1;
    Console.WriteLine($"NextRenderBufferId: {id}");
    return id;
  }

  private unsafe void DestroyGeometry(uint id) {
    Console.WriteLine($"DestroyGeometry: {id}");
    var index = (int) id - 1;
    var entry = GeometryEntries.RemoveAt(index);
    _gl.DeleteBuffer(entry.Indices);
    _gl.DeleteBuffer(entry.Vertices);
    _gl.DeleteVertexArray(entry.VertexArray);
    CheckGl();
  }

  private unsafe void UpdateGeometry(uint id, VertexBuffer vertices, IndexBuffer indices) {
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
  }

  private unsafe void CreateGeometry(uint id, VertexBuffer vertices, IndexBuffer indices) {
    Console.WriteLine($"CreateGeometry: {id}");
    var index = (int) id - 1;

    var entry = GeometryEntries[index];

    CheckGl();

    var vao = _gl.GenVertexArray();
    entry.VertexArray = vao;
    _gl.BindVertexArray(vao);
    LabelObject(ObjectIdentifier.VertexArray, vao, $"Ultralight Geometry VAO {id}");
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
  }

  private unsafe uint NextGeometryId() {
    var id = (uint) GeometryEntries.Add(new GeometryEntry()) + 1;
    Console.WriteLine($"NextGeometryId: {id}");
    return id;
  }

  private unsafe void EndSynchronize() {
    //Console.WriteLine("EndSynchronize");
  }

  private unsafe void BeginSynchronize() {
    //Console.WriteLine("BeginSynchronize");
  }

  private void CheckGl([CallerLineNumber] int lineNumber = default) {
#if DEBUG
    var error = _gl.GetError();
    if (error == default)
      return;

    Console.Error.WriteLine($"Line {lineNumber}, GL Error: {error}");
    Console.Error.Flush();
    //Debugger.Break();
#endif
  }

  private void LabelObject(ObjectIdentifier objId, uint vao, string name) {
    if (_dbg != null) {
      _dbg.ObjectLabel(objId, vao, (uint) name.Length, name);
      CheckGl();
    }
  }

  public void InitializeShaders() {
    {
      //Creating a vertex shader.
      var vs = _gl.CreateShader(ShaderType.VertexShader);
      LabelObject(ObjectIdentifier.Shader, vs, "Fill VS");
      _gl.ShaderSource(vs, Utilities.LoadEmbeddedUtf8String("embedded.shader_v2f_c4f_t2f_t2f_d28f.vert.glsl"));
      CheckGl();
      _gl.CompileShader(vs);
      CheckGl();
      _gl.GetShader(vs, ShaderParameterName.CompileStatus, out var vsCompileStatus);

      //Checking the shader for compilation errors.
      var vsLog = _gl.GetShaderInfoLog(vs);
      var vsLogEmpty = string.IsNullOrWhiteSpace(vsLog);
      var vsCompileSuccess = vsCompileStatus == (int) GLEnum.True;
      if (!vsCompileSuccess || !vsLogEmpty) {
        (vsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(vsCompileSuccess ? "Messages" : "Errors")} compiling fill vertex shader\n{vsLog}");
        if (!vsCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      CheckGl();

      //Creating a fragment shader.
      var fs = _gl.CreateShader(ShaderType.FragmentShader);
      LabelObject(ObjectIdentifier.Shader, fs, "Fill FS");
      _gl.ShaderSource(fs, Utilities.LoadEmbeddedUtf8String("embedded.shader_fill.frag.glsl"));
      CheckGl();
      _gl.CompileShader(fs);
      CheckGl();
      _gl.GetShader(fs, ShaderParameterName.CompileStatus, out var fsCompileStatus);

      //Checking the shader for compilation errors.
      var fsLog = _gl.GetShaderInfoLog(fs);
      var fsLogEmpty = string.IsNullOrWhiteSpace(fsLog);
      var fsCompileSuccess = fsCompileStatus == (int) GLEnum.True;
      if (!fsCompileSuccess || !fsLogEmpty) {
        (fsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(fsCompileSuccess ? "Messages" : "Errors")} compiling fill fragment shader\n{fsLog}");
        if (!fsCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      CheckGl();

      //Combining the shaders under one shader program.
      var pg = _gl.CreateProgram();
      LabelObject(ObjectIdentifier.Program, pg, "Fill Program");
      _gl.AttachShader(pg, vs);
      _gl.AttachShader(pg, fs);

      _gl.BindAttribLocation(pg, 0, "in_Position");
      _gl.BindAttribLocation(pg, 1, "in_Color");
      _gl.BindAttribLocation(pg, 2, "in_TexCoord");
      _gl.BindAttribLocation(pg, 3, "in_ObjCoord");
      _gl.BindAttribLocation(pg, 4, "in_Data0");
      _gl.BindAttribLocation(pg, 5, "in_Data1");
      _gl.BindAttribLocation(pg, 6, "in_Data2");
      _gl.BindAttribLocation(pg, 7, "in_Data3");
      _gl.BindAttribLocation(pg, 8, "in_Data4");
      _gl.BindAttribLocation(pg, 9, "in_Data5");
      _gl.BindAttribLocation(pg, 10, "in_Data6");

      _gl.LinkProgram(pg);
      CheckGl();
      _gl.GetProgram(pg, ProgramPropertyARB.LinkStatus, out var pgLinkStatus);

      //Checking the linking for errors.
      var pgLog = _gl.GetProgramInfoLog(pg);
      var pgLogEmpty = string.IsNullOrWhiteSpace(pgLog);
      var pgCompileSuccess = pgLinkStatus == (int) GLEnum.True;
      if (!pgCompileSuccess || !pgLogEmpty) {
        (pgCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(pgCompileSuccess ? "Messages" : "Errors")} linking fill shader program\n{pgLog}");
        if (!pgCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      CheckGl();
      _gl.ValidateProgram(pg);
      CheckGl();

      //Delete the no longer useful individual shaders;
      _gl.DetachShader(pg, vs);
      _gl.DetachShader(pg, fs);
      _gl.DeleteShader(vs);
      _gl.DeleteShader(fs);

      //Tell opengl how to give the data to the shaders.
      _gl.UseProgram(pg);
      _gl.Uniform1(_gl.GetUniformLocation(pg, "Texture1"), 0);
      _gl.Uniform1(_gl.GetUniformLocation(pg, "Texture2"), 1);
      _gl.Uniform1(_gl.GetUniformLocation(pg, "Texture3"), 2);
      CheckGl();

      _fillShader = pg;
    }

    {
      //Creating a vertex shader.
      var vs = _gl.CreateShader(ShaderType.VertexShader);
      LabelObject(ObjectIdentifier.Shader, vs, "Fill Path VS");
      _gl.ShaderSource(vs, Utilities.LoadEmbeddedUtf8String("embedded.shader_v2f_c4f_t2f.vert.glsl"));
      CheckGl();
      _gl.CompileShader(vs);
      CheckGl();
      _gl.GetShader(vs, ShaderParameterName.CompileStatus, out var vsCompileStatus);

      //Checking the shader for compilation errors.
      var vsLog = _gl.GetShaderInfoLog(vs);
      var vsLogEmpty = string.IsNullOrWhiteSpace(vsLog);
      var vsCompileSuccess = vsCompileStatus == (int) GLEnum.True;
      if (!vsCompileSuccess || !vsLogEmpty) {
        (vsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(vsCompileSuccess ? "Messages" : "Errors")} compiling fill path vertex shader\n{vsLog}");
        if (!vsCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      //Creating a fragment shader.
      var fs = _gl.CreateShader(ShaderType.FragmentShader);
      LabelObject(ObjectIdentifier.Shader, fs, "Fill Path FS");
      _gl.ShaderSource(fs, Utilities.LoadEmbeddedUtf8String("embedded.shader_fill_path.frag.glsl"));
      CheckGl();
      _gl.CompileShader(fs);
      CheckGl();

      _gl.GetShader(fs, ShaderParameterName.CompileStatus, out var fsCompileStatus);

      //Checking the shader for compilation errors.
      var fsLog = _gl.GetShaderInfoLog(fs);
      var fsLogEmpty = string.IsNullOrWhiteSpace(fsLog);
      var fsCompileSuccess = fsCompileStatus == (int) GLEnum.True;
      if (!fsCompileSuccess || !fsLogEmpty) {
        (fsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(fsCompileSuccess ? "Messages" : "Errors")} compiling fill path fragment shader\n{fsLog}");
        if (!fsCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      CheckGl();

      //Combining the shaders under one shader program.
      var pg = _gl.CreateProgram();
      LabelObject(ObjectIdentifier.Program, pg, "Fill Path Program");
      _gl.AttachShader(pg, vs);
      _gl.AttachShader(pg, fs);

      _gl.BindAttribLocation(pg, 0, "in_Position");
      _gl.BindAttribLocation(pg, 1, "in_Color");
      _gl.BindAttribLocation(pg, 2, "in_TexCoord");

      _gl.LinkProgram(pg);
      CheckGl();
      _gl.GetProgram(pg, ProgramPropertyARB.LinkStatus, out var pgLinkStatus);

      //Checking the linking for errors.
      var pgLog = _gl.GetProgramInfoLog(pg);
      var pgLogEmpty = string.IsNullOrWhiteSpace(pgLog);
      var pgCompileSuccess = pgLinkStatus == (int) GLEnum.True;
      if (!pgCompileSuccess || !pgLogEmpty) {
        (pgCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(pgCompileSuccess ? "Messages" : "Errors")} linking fill path shader program\n{pgLog}");
        if (!pgCompileSuccess) {
          Console.Error.Flush();
          //Debugger.Break();
        }
      }

      CheckGl();
      _gl.ValidateProgram(pg);
      CheckGl();

      //Delete the no longer useful individual shaders;
      _gl.DetachShader(pg, vs);
      _gl.DetachShader(pg, fs);
      _gl.DeleteShader(vs);
      _gl.DeleteShader(fs);

      //Tell opengl how to give the data to the shaders.
      CheckGl();

      _fillPathShader = pg;
    }
  }

  public unsafe void Render(IView view) {
    var commands = _queuedCommands;
    if (commands.Count <= 0)
      return;

    Console.WriteLine($"Running {commands.Count} Commands");
    _queuedCommands = new Queue<Command>();
    _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

    // render view
    _gl.Enable(EnableCap.Blend);
    //_gl.Enable(EnableCap.FramebufferSrgb);
    _gl.Disable(EnableCap.ScissorTest);
    _gl.Disable(EnableCap.DepthTest);
    _gl.DepthFunc(DepthFunction.Never);
    _gl.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);

    foreach (var command in commands)
      switch (command.CommandType) {
        case CommandType.ClearRenderBuffer: {
          //Console.WriteLine("Clearing Render Buffer");
          var index = (int) command.GpuState.RenderBufferId - 1;
          var rb = RenderBufferEntries[index];

          // bind render buffer
          _gl.BindFramebuffer(FramebufferTarget.Framebuffer, rb.FrameBuffer);

          // disable scissor state
          _gl.Disable(GLEnum.ScissorTest);

          // set clear color
          _gl.ClearColor(0, 0, 0, 0);

          // clear
          _gl.Clear((uint) GLEnum.ColorBufferBit);
          break;
        }
        case CommandType.DrawGeometry: {
          //Console.WriteLine("Drawing Geometry");
          ref readonly var state = ref command.GpuState;
          var index = (int) command.GpuState.RenderBufferId - 1;
          var rb = RenderBufferEntries[index];

          // bind render buffer
          _gl.BindFramebuffer(FramebufferTarget.Framebuffer, rb.FrameBuffer);

          // set viewport
          _gl.Viewport(0, 0, state.ViewportWidth, state.ViewportHeight);
          var entry = GeometryEntries[(int) command.GeometryId - 1];

          // select program
          var shaderType = state.ShaderType;

          uint pg;
          switch (shaderType) {
            default: throw new ArgumentOutOfRangeException(nameof(ULShaderType), shaderType, "Unexpected value.");
            case ULShaderType.Fill: {
              pg = _fillShader;
              break;
            }
            case ULShaderType.FillPath: {
              pg = _fillPathShader;
              break;
            }
          }

          _gl.UseProgram(pg);

          CheckGl();

          // update uniforms
          _gl.Uniform4(
            _gl.GetUniformLocation(pg, "State"),
            (float) view.Time,
            state.ViewportWidth,
            state.ViewportHeight,
            1
          );
          var txf =
            Ultralight.ApplyProjection(
              state.Transform,
              state.ViewportWidth,
              state.ViewportHeight,
              false
            );
          _gl.UniformMatrix4(
            _gl.GetUniformLocation(pg, "Transform"), 1, false,
            (float*) Unsafe.AsPointer(ref Unsafe.AsRef(txf))
          );
          _gl.Uniform4(
            _gl.GetUniformLocation(pg, "Scalar4"), 2,
            state.UniformScalars
          );
          _gl.Uniform4(
            _gl.GetUniformLocation(pg, "Vector"), 8,
            state.UniformVectors
          );
          _gl.Uniform1(
            _gl.GetUniformLocation(pg, "fClipSize"),
            (float) state.ClipSize
          );
          _gl.UniformMatrix4(
            _gl.GetUniformLocation(pg, "Clip"), 8, false,
            (float*) Unsafe.AsPointer(ref Unsafe.AsRef(state.Clip))
          );

          CheckGl();

          // bind vertex array
          _gl.BindVertexArray(entry.VertexArray);

          CheckGl();

          // bind textures
          var texIndex1 = (int) state.Texture1Id - 1;
          if (texIndex1 > 0) {
            var texEntry1 = TextureEntries[texIndex1];
            _gl.ActiveTexture(TextureUnit.Texture0);
            _gl.BindTexture(GLEnum.Texture2D, texEntry1.Texure);
            CheckGl();
          }
          else {
            //Console.WriteLine($"Texture1 Invalid: {texIndex1 + 1}");
            _gl.ActiveTexture(TextureUnit.Texture0);
            _gl.BindTexture(GLEnum.Texture2D, 0);
          }

          var texIndex2 = (int) state.Texture2Id - 1;
          if (texIndex2 > 0) {
            var texEntry2 = TextureEntries[texIndex2];
            _gl.ActiveTexture(TextureUnit.Texture1);
            _gl.BindTexture(GLEnum.Texture2D, texEntry2.Texure);
            CheckGl();
          }
          else {
            //Console.WriteLine($"Texture2 Invalid: {texIndex2 + 1}");
            _gl.ActiveTexture(TextureUnit.Texture1);
            _gl.BindTexture(GLEnum.Texture2D, 0);
          }

          var texIndex3 = (int) state.Texture3Id - 1;
          if (texIndex3 > 0) {
            var texEntry3 = TextureEntries[texIndex3];
            _gl.ActiveTexture(TextureUnit.Texture2);
            _gl.BindTexture(GLEnum.Texture2D, texEntry3.Texure);
            CheckGl();
          }
          else {
            //Console.WriteLine($"Texture3 Invalid: {texIndex3 + 1}");
            _gl.ActiveTexture(TextureUnit.Texture2);
            _gl.BindTexture(GLEnum.Texture2D, 0);
          }

          // scissor state
          if (state.EnableScissor) {
            _gl.Enable(EnableCap.ScissorTest);
            CheckGl();

            ref readonly var r = ref state.ScissorRect;
            _gl.Scissor(r.Left, r.Top,
              (uint) (r.Right - r.Left),
              (uint) (r.Bottom - r.Top));
          }
          else
            _gl.Disable(EnableCap.ScissorTest);

          CheckGl();

          // blend state
          if (state.EnableBlend)
            _gl.Enable(EnableCap.Blend);
          else
            _gl.Disable(EnableCap.Blend);

          CheckGl();

          // draw elements
          _gl.DrawElements(PrimitiveType.Triangles,
            command.IndicesCount, DrawElementsType.UnsignedInt,
            (void*) (command.IndicesOffset * sizeof(uint)));
          CheckGl();

          // reset vertex array
          _gl.BindVertexArray(0);

          break;
        }
        default: throw new ArgumentOutOfRangeException(nameof(CommandType));
      }
  }

  public bool TryGetFrameBufferAndTexture(View view, out uint framebuffer, out uint texture, out uint width, out uint height) {
    var renderTarget = view.GetRenderTarget();
    var rbIndex = (int) renderTarget.RenderBufferId - 1;
    if (!RenderBufferEntries.TryGet(rbIndex, out var rbEntry)) {
      framebuffer = 0;
      texture = 0;
      width = 0;
      height = 0;
      return false;
    }

    framebuffer = rbEntry.FrameBuffer;
    var texEntry = rbEntry.TextureEntry;
    texture = texEntry.Texure;
    width = texEntry.Width;
    height = texEntry.Height;
    return true;
  }

}