using Silk.NET.Core.Loader;

/// <summary>Contains the library name of OpenGLES.</summary>
internal class CustomGlEsLibNameContainer : SearchPathContainer {

  /// <inheritdoc />
  public override string Linux => "libGLESv2.so";

  /// <inheritdoc />
  public override string MacOS => "libGLESv2.so";

  /// <inheritdoc />
  public override string Android => "libGLESv2.so";

  /// <inheritdoc />
  public override string IOS => "libGLESv2.so";

  /// <inheritdoc />
  public override string Windows64 => "libGLESv2.dll";

  /// <inheritdoc />
  public override string Windows86 => "libGLESv2.dll";

}