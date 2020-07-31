using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeUrlCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* url);

}