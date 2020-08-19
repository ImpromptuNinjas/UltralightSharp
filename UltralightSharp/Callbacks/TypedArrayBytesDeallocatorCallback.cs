using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void TypedArrayBytesDeallocatorCallback([NativeTypeName("void *")] void* bytes, [NativeTypeName("void *")] void* deallocatorContext);

  namespace Safe {

    [PublicAPI]
    public delegate void TypedArrayBytesDeallocatorCallback(
      IntPtr bytes,
      IntPtr deallocatorContext
    );

  }

}