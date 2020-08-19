using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverDestroyRenderBufferCallback([NativeTypeName("unsigned int")] uint renderBufferId);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverDestroyRenderBufferCallback(uint renderBufferId);

  }

}