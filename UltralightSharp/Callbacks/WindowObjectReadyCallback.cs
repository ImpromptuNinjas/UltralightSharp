using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void WindowObjectReadyCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url);

  namespace Safe {

    [PublicAPI]
    public delegate void WindowObjectReadyCallback(IntPtr userData, View caller, ulong frameId, bool isMainFrame,
      string? url);

  }

}