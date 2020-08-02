using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void SurfaceDefinitionUnlockPixelsCallback([NativeTypeName("void *")] void* userData);

  namespace Safe {

    [PublicAPI]
    public delegate void SurfaceDefinitionUnlockPixelsCallback(IntPtr userData);

  }

}