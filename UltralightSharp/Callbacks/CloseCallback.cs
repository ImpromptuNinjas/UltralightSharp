using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void CloseCallback([NativeTypeName("void *")] void* userData);

  namespace Safe {

    [PublicAPI]
    public delegate void CloseCallback(IntPtr userData);

  }

}