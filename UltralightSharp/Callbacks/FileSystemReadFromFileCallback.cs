using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("long long")]
  public unsafe delegate long FileSystemReadFromFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle, [NativeTypeName("char *")] sbyte* data, [NativeTypeName("long long")] long length);

  namespace Safe {

    [PublicAPI]
    public unsafe delegate long FileSystemReadFromFileCallback(UIntPtr handle, ReadOnlySpan<byte> data);

  }

}