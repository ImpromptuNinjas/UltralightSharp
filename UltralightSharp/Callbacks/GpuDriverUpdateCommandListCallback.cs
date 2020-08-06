using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverUpdateCommandListCallback(CommandList list);


  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverUpdateCommandListCallback(CommandList list);

  }
}