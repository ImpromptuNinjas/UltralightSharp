using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULView")]
  public unsafe delegate View* CreateChildViewCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* openerUrl,
    [NativeTypeName("ULString")] String* targetUrl, bool isPopup, IntRect popupRect);

  namespace Safe {

    [PublicAPI]
    public delegate View CreateChildViewCallback(IntPtr userData, View caller, string? openerUrl,
      string? targetUrl, bool isPopup, IntRect popupRect);

  }

}