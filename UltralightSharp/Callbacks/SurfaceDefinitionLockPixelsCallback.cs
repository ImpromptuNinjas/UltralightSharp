using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("void *")]
  public unsafe delegate void* SurfaceDefinitionLockPixelsCallback([NativeTypeName("void *")] void* userData);

  namespace Safe {

    [PublicAPI]
    public delegate IntPtr SurfaceDefinitionLockPixelsCallback(IntPtr userData);

  }

}