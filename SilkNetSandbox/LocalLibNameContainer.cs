using System;
using System.IO;
using Silk.NET.Core.Loader;

internal abstract class LocalLibNameContainer : SearchPathContainer {

  protected static readonly string AssemblyPath
    = new Uri(typeof(CustomGlEsLibNameContainer).Assembly.CodeBase!).LocalPath;

  protected static readonly string AssemblyDirectory
    = Path.GetDirectoryName(AssemblyPath)!;

}