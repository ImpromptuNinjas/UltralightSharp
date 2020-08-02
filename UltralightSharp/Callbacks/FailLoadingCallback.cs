using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void FailLoadingCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url, [NativeTypeName("ULString")] String* description, [NativeTypeName("ULString")] String* errorDomain, int errorCode);

  namespace Safe {

    [PublicAPI]
    public unsafe delegate void FailLoadingCallback(IntPtr userData, View caller, ulong frameId, bool isMainFrame,
      string? url, string? description, string? errorDomain, int errorCode);

  }

}