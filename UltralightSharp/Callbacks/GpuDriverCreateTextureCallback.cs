using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void GpuDriverCreateTextureCallback([NativeTypeName("unsigned int")] uint textureId, [NativeTypeName("ULBitmap")] Bitmap* bitmap);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverCreateTextureCallback(uint textureId, Bitmap bitmap);

  }

}