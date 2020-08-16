using System.IO;

/// <summary>Contains the library name of EGL.</summary>
internal class CustomEglLibNameContainer : LocalLibNameContainer {
  
  /// <inheritdoc />
  public override string Linux { get; } = Path.Combine(AssemblyDirectory, "libEGL.so");

  /// <inheritdoc />
  public override string Android { get; } = Path.Combine(AssemblyDirectory, "libEGL.so");

  /// <inheritdoc />
  public override string MacOS { get; } = Path.Combine(AssemblyDirectory, "libEGL.dylib");

  /// <inheritdoc />
  public override string IOS { get; } = Path.Combine(AssemblyDirectory, "libEGL.dylib");

  /// <inheritdoc />
  public override string Windows64 { get; } = Path.Combine(AssemblyDirectory, "libEGL.dll");

  /// <inheritdoc />
  public override string Windows86 { get; } = Path.Combine(AssemblyDirectory, "libEGL.dll");

}