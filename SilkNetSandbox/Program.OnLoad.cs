using System;
using System.IO;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Safe;
using Silk.NET.Core;
using Silk.NET.Input;
using Silk.NET.OpenGLES;
using Silk.NET.Windowing.Common;
using Ultz.SuperInvoke;

partial class Program {

  private static readonly float[] _quadVerts = {
    //X Y Z
    1f, 1f, 0f,
    1f, -1f, 0f,
    -1f, -1f, 0f,
    -1f, 1f, 1f
  };

  private static readonly uint[] _quadIndices = {
    0, 1, 3,
    1, 2, 3
  };

  private static OpenGlEsGpuDriverSite _gpuDriverSite;

  private static unsafe void OnLoad() {
    Console.WriteLine($"Loading...");
    //Getting the opengl api for drawing to the screen.
    _gl = LibraryActivator.CreateInstance<GL>(
      new CustomGlEsLibNameContainer().GetLibraryName(),
      TemporarySuperInvokeClass.GetLoader(_snView.GLContext)
    );

    var glVersionInfo = _gl.GetString(StringName.Version);
    var glVersionMajor = _gl.GetInteger(GetPName.MajorVersion);
    if (glVersionMajor == 0) {
      Console.WriteLine("Unable to retrieve API major version.");
      glVersionMajor = !_automaticFallback ? _glMaj : _useOpenGL ? 3 : _majOES; // bug?
    }
    var glVersionMinor = _gl.GetInteger(GetPName.MinorVersion);
    Console.WriteLine($"{(_useOpenGL?"OpenGL":"OpenGL ES")} v{glVersionMajor}.{glVersionMinor} ({glVersionInfo})");

    var glVendor = _gl.GetString(StringName.Vendor);
    var glDevice = _gl.GetString(StringName.Renderer);
    Console.WriteLine($"{glVendor} {glDevice}");

    var glShaderVersionInfo = _gl.GetString(StringName.ShadingLanguageVersion);
    Console.WriteLine($"Shader Language: {glShaderVersionInfo}");

    _gpuDriverSite = new OpenGlEsGpuDriverSite(_gl, _dbg);

    var gpuDriver = _gpuDriverSite.CreateGpuDriver();
    Ultralight.SetGpuDriver(gpuDriver);

    using var cfg = new Config();

    var cachePath = Path.Combine(_storagePath, "Cache");
    cfg.SetCachePath(cachePath);

    var resourcePath = Path.Combine(AsmDir, "resources");
    cfg.SetResourcePath(resourcePath);

    cfg.SetUseGpuRenderer(true);
    cfg.SetEnableImages(true);
    cfg.SetEnableJavaScript(false);

    //cfg.SetForceRepaint(true);

    _ulRenderer = new Renderer(cfg);
    _ulSession = new Session(_ulRenderer, false, "Demo");
    var wndSize = _snView.Size;
    var wndWidth = (uint) wndSize.Width;
    var wndHeight = (uint) wndSize.Height;
    var width = (uint) (_scaleX * wndWidth);
    var height = (uint) (_scaleY * wndHeight);

    _ulView = new View(_ulRenderer, width, height, false, _ulSession);
    _ulView.SetAddConsoleMessageCallback(ConsoleMessageCallback, default);

    if (_snView is IWindow wnd)
      wnd.Title = _useOpenGL
        ? $"UltralightSharp - OpenGL v{glVersionMajor}.{glVersionMinor} (Silk.NET)"
        : $"UltralightSharp - OpenGL ES v{glVersionMajor}.{glVersionMinor} (Silk.NET)";

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
      var monitors = _glfw.GetMonitors(out var monitorCount);
      var monitorInterface = ((IWindow) _snView).Monitor;
      var monitor = monitors[monitorInterface.Index];
      _glfw.GetMonitorContentScale(monitor, out var xScale, out var yScale);
      _scaleX = xScale;
      _scaleY = yScale;
    }

    EnableDebugExtension();

    _gl.Disable(EnableCap.Dither);
    CheckGl();
    //_gl.Disable(EnableCap.PointSmooth);
    //CheckGl();
    //_gl.Disable(EnableCap.LineSmooth);
    //CheckGl();
    //_gl.Disable(EnableCap.PolygonSmooth);
    //CheckGl();
    //_gl.Hint(HintTarget.PointSmoothHint, HintMode.DontCare);
    //CheckGl();
    //_gl.Hint(HintTarget.LineSmoothHint, HintMode.DontCare);
    //CheckGl();
    //_gl.Hint(HintTarget.PolygonSmoothHint, HintMode.DontCare);
    //CheckGl();
    //_gl.Disable(EnableCap.Multisample);
    CheckGl();

    //Vertex data, uploaded to the VBO.
    var quadVertsLen = (uint) (_quadVerts.Length * sizeof(float));

    //Index data, uploaded to the EBO.
    var indicesSize = (uint) (_quadIndices.Length * sizeof(uint));

    //Creating a vertex array.
    _qva = _gl.GenVertexArray();
    _gl.BindVertexArray(_qva);
    LabelObject(ObjectIdentifier.VertexArray, _qva, "Quad VAO");

    //Initializing a vertex buffer that holds the vertex data.
    _qvb = _gl.GenBuffer(); //Creating the buffer.
    _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _qvb); //Binding the buffer.
    LabelObject(ObjectIdentifier.Buffer, _qvb, "Quad VBO");
    _gl.BufferData(BufferTargetARB.ArrayBuffer, quadVertsLen, new Span<float>(_quadVerts), BufferUsageARB.StaticDraw); //Setting buffer data.

    //Initializing a element buffer that holds the index data.
    _qeb = _gl.GenBuffer(); //Creating the buffer.
    _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _qeb); //Binding the buffer.
    LabelObject(ObjectIdentifier.Buffer, _qeb, "Quad EBO");
    _gl.BufferData(BufferTargetARB.ElementArrayBuffer, indicesSize, new Span<uint>(_quadIndices), BufferUsageARB.StaticDraw); //Setting buffer data.

    //Creating a vertex shader.
    var qvs = _gl.CreateShader(ShaderType.VertexShader);
    LabelObject(ObjectIdentifier.Shader, qvs, "Quad VS");
    _gl.ShaderSource(qvs, Utilities.LoadEmbeddedUtf8String("embedded.basic.vert.glsl"));
    _gl.CompileShader(qvs);
    CheckGl();
    _gl.GetShader(qvs, ShaderParameterName.CompileStatus, out var qvsCompileStatus);

    //Checking the shader for compilation errors.
    var qvsLog = _gl.GetShaderInfoLog(qvs);
    var qvsLogEmpty = string.IsNullOrWhiteSpace(qvsLog);
    var qvsCompileSuccess = qvsCompileStatus == (int) GLEnum.True;
    if (!qvsCompileSuccess || !qvsLogEmpty) {
      (qvsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(qvsCompileSuccess ? "Messages" : "Errors")} compiling quad vertex shader\n{qvsLog}");
      if (!qvsCompileSuccess) {
        Console.Error.Flush();
        //Debugger.Break();
      }
    }

    CheckGl();

    //Creating a fragment shader.
    var qfs = _gl.CreateShader(ShaderType.FragmentShader);
    LabelObject(ObjectIdentifier.Shader, qfs, "Quad FS");
    _gl.ShaderSource(qfs, Utilities.LoadEmbeddedUtf8String("embedded.basic.frag.glsl"));
    _gl.CompileShader(qfs);
    CheckGl();
    _gl.GetShader(qfs, ShaderParameterName.CompileStatus, out var qfsCompileStatus);

    //Checking the shader for compilation errors.
    var qfsLog = _gl.GetShaderInfoLog(qfs);
    var qfsLogEmpty = string.IsNullOrWhiteSpace(qfsLog);
    var qfsCompileSuccess = qfsCompileStatus == (int) GLEnum.True;
    if (!qfsCompileSuccess || !qfsLogEmpty) {
      (qfsCompileSuccess ? Console.Out : Console.Error).WriteLine($"{(qfsCompileSuccess ? "Messages" : "Errors")} compiling quad fragment shader\n{qfsLog}");
      if (!qfsCompileSuccess) {
        Console.Error.Flush();
        //Debugger.Break();
      }
    }

    CheckGl();

    //Combining the shaders under one shader program.
    _qpg = _gl.CreateProgram();
    LabelObject(ObjectIdentifier.Program, _qpg, "Quad Program");
    _gl.AttachShader(_qpg, qvs);
    _gl.AttachShader(_qpg, qfs);
    _gl.BindAttribLocation(_qpg, 0, "vPos");
    CheckGl();
    _gl.EnableVertexAttribArray(0);
    CheckGl();
    _gl.LinkProgram(_qpg);
    CheckGl();
    _gl.GetProgram(_qpg, ProgramPropertyARB.LinkStatus, out var qpgLinkStatus);

    //Checking the linking for errors.
    var qpgLog = _gl.GetProgramInfoLog(_qpg);
    var qpgLogEmpty = string.IsNullOrWhiteSpace(qpgLog);
    var qpgLinkSuccess = qpgLinkStatus == (int) GLEnum.True;
    if (!qpgLinkSuccess || !qpgLogEmpty)
      Console.WriteLine($"{(qpgLinkSuccess ? "Messages" : "Errors")} linking quad shader program:\n{qpgLog}");
    CheckGl();
    _gl.ValidateProgram(_qpg);
    CheckGl();

    _gl.UseProgram(_qpg);
    var vPos = (uint) _gl.GetAttribLocation(_qpg, "vPos");
    CheckGl();
    _gl.VertexAttribPointer(vPos,
      3, VertexAttribPointerType.Float, false, 0, (void*) 0);
    CheckGl();

    //Delete the no longer useful individual shaders;
    _gl.DetachShader(_qpg, qvs);
    _gl.DetachShader(_qpg, qfs);
    _gl.DeleteShader(qvs);
    _gl.DeleteShader(qfs);

    _gpuDriverSite.InitializeShaders();

    var input = _snView.CreateInput();
    foreach (var kb in input.Keyboards)
      kb.KeyDown += KeyDown;

    Console.WriteLine("Loading index.html");
    _ulView.LoadUrl("file:///index.html");
  }

}