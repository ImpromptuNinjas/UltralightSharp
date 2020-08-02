using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemFileExistsCallback([NativeTypeName("ULString")] String* path);

  namespace Safe {

    [PublicAPI]
    public delegate bool FileSystemFileExistsCallback(string? path);

  }

}