using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void GpuDriverUpdateTextureCallback([NativeTypeName("unsigned int")] uint textureId, [NativeTypeName("ULBitmap")] Bitmap* bitmap);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverUpdateTextureCallback(uint textureId, Bitmap bitmap);

  }
}