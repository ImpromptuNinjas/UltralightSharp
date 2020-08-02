using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void UpdateCallback([NativeTypeName("void *")] void* userData);

  namespace Safe {

    [PublicAPI]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UpdateCallback(IntPtr userData);

  }

}