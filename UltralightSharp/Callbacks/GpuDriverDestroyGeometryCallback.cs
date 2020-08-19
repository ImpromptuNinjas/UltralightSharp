using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverDestroyGeometryCallback([NativeTypeName("unsigned int")] uint geometryId);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverDestroyGeometryCallback(uint geometryId);

  }

}