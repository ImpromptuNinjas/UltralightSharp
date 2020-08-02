using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemGetFileMimeTypeCallback([NativeTypeName("ULString")] String* path, [NativeTypeName("ULString")] String* result);

  namespace Safe {

    [PublicAPI]
    public delegate bool FileSystemGetFileMimeTypeCallback(string? path, string? result);

  }

}