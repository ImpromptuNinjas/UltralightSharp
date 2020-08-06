using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverCreateRenderBufferCallback([NativeTypeName("unsigned int")] uint renderBufferId, RenderBuffer buffer);


  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverCreateRenderBufferCallback(uint renderBufferId, RenderBuffer buffer);

  }
}