using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverBeginSynchronizeCallback();

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverBeginSynchronizeCallback();

  }

}