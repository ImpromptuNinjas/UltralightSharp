using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULView")]
  public unsafe delegate View* CreateChildViewCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* openerUrl,
    [NativeTypeName("ULString")] String* targetUrl, bool isPopup, IntRect popupRect);

}