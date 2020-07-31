using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void FailLoadingCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url, [NativeTypeName("ULString")] String* description, [NativeTypeName("ULString")] String* errorDomain, int errorCode);

}