using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverDestroyTextureCallback([NativeTypeName("unsigned int")] uint textureId);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverDestroyTextureCallback(uint textureId);

  }

}