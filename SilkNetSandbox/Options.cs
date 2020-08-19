using CommandLine;

internal class Options {

  [Option('a', "api", Required = false, HelpText = "Set GL API to OpenGL (gl) or OpenGL ES (gles).")]
  public string? GlApi { get; set; }

  [Option('c', "ctx", Required = false, HelpText = "Set context to Native (n) or EGL (egl).")]
  public string? Context { get; set; }

  [Option('m', "maj", Required = false, HelpText = "Set major version OpenGL to request.")]
  public int? GlMajorVersion { get; set; }

  [Option('n', "min", Required = false, HelpText = "Set minor version OpenGL to request.")]
  public int? GlMinorVersion { get; set; }

}