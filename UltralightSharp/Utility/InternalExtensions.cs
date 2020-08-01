using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  internal static class InternalExtensions {

#if NETSTANDARD1_1
    internal static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new() {
      if (dictionary.TryGetValue(key, out var value))
        return value;

      return dictionary[key] = new TValue();
    }
#endif

    internal static string GetLocalCodeBaseDirectory(this Assembly asm)
#if NETSTANDARD1_1 || NETSTANDARD1_4
      => Path.GetDirectoryName(new Uri((asm.ManifestModule?.FullyQualifiedName
          ?? throw new PlatformNotSupportedException()).Replace("#", "%23")).LocalPath)
        ?? throw new PlatformNotSupportedException();
#else
      => Path.GetDirectoryName(new Uri((asm.CodeBase
            ?? throw new PlatformNotSupportedException())
          .Replace("#", "%23")).LocalPath)
        ?? throw new PlatformNotSupportedException();
#endif

#if NETSTANDARD1_1 || NETSTANDARD1_4
    internal static Assembly GetAssembly(this Type type)
      => type.GetTypeInfo().Assembly;
#else
    internal static Assembly GetAssembly(this Type type)
      => type.Assembly;
#endif

  }

}