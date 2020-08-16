using System.IO;

/// <summary>Contains the library name of OpenGL ES.</summary>
internal class CustomGlEsLibNameContainer : LocalLibNameContainer {
  
  /// <inheritdoc />
  public override string Linux { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.so");

  /// <inheritdoc />
  public override string Android { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.so");

  /// <inheritdoc />
  public override string MacOS { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.dylib");

  /// <inheritdoc />
  public override string IOS { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.dylib");

  /// <inheritdoc />
  public override string Windows64 { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.dll");

  /// <inheritdoc />
  public override string Windows86 { get; } = Path.Combine(AssemblyDirectory, "libGLESv2.dll");

}