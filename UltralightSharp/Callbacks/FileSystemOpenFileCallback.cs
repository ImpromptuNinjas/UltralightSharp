using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULFileHandle")]
  public unsafe delegate UIntPtr FileSystemOpenFileCallback([NativeTypeName("ULString")] String* path, bool openForWriting);

  namespace Safe {

    [PublicAPI]
    public delegate UIntPtr FileSystemOpenFileCallback(string? path, bool openForWriting);

  }

}