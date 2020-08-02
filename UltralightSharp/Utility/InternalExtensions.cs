using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  internal static class InternalExtensions {


    internal static string GetLocalCodeBaseDirectory(this Assembly asm)
      => Path.GetDirectoryName(new Uri((asm.CodeBase
            ?? throw new PlatformNotSupportedException())
          .Replace("#", "%23")).LocalPath)
        ?? throw new PlatformNotSupportedException();

    internal static Assembly GetAssembly(this Type type)
      => type.Assembly;

  }

}