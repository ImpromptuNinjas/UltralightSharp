using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ResizeCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

  namespace Safe {

    [PublicAPI]
    public delegate void ResizeCallback(IntPtr userData, uint width, uint height);

  }

}