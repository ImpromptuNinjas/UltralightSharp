using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public delegate uint GpuDriverNextTextureIdCallback();



  namespace Safe {

    [PublicAPI]
    public delegate uint GpuDriverNextTextureIdCallback();

  }
}