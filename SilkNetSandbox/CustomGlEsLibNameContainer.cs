using System;
using System.IO;
using Silk.NET.Core.Loader;

/// <summary>Contains the library name of OpenGLES.</summary>
internal class CustomGlEsLibNameContainer : SearchPathContainer {

  private static readonly string AssemblyPath
    = new Uri(typeof(CustomGlEsLibNameContainer).Assembly.CodeBase!).LocalPath;

  private static readonly string AssemblyDirectory
    = Path.GetDirectoryName(AssemblyPath)!;
  
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