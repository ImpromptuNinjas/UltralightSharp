using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core;
using Silk.NET.GLFW;
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

  private static unsafe void OnLoad() {
    //Getting the opengl api for drawing to the screen.
    _gl = LibraryActivator.CreateInstance<GL>(
      new CustomGlEsLibNameContainer().GetLibraryName(),
      TemporarySuperInvokeClass.GetLoader(_snView.GLContext)
    );

    var glVersionInfo = _gl.GetString(StringName.Version);
    var glVersionMajor = _gl.GetInteger(GetPName.MajorVersion);
    var glVersionMinor = _gl.GetInteger(GetPName.MinorVersion);
    Console.WriteLine($"OpenGL v{glVersionMajor}.{glVersionMinor} ({glVersionInfo})");

    var glVendor = _gl.GetString(StringName.Vendor);
    var glDevice = _gl.GetString(StringName.Renderer);
    Console.WriteLine($"{glVendor} {glDevice}");

    var glShaderVersionInfo = _gl.GetString(StringName.ShadingLanguageVersion);
    Console.WriteLine($"Shader Language: {glShaderVersionInfo}");

    if (_snView is IWindow wnd)
      wnd.Title = $"UltralightSharp - OpenGL v{glVersionMajor}.{glVersionMinor} (Silk.NET)";

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

    var input = _snView.CreateInput();
    foreach (var kb in input.Keyboards)
      kb.KeyDown += KeyDown;

    Console.WriteLine("Loading index.html");
    _ulView.LoadUrl("file:///index.html");
  }

}