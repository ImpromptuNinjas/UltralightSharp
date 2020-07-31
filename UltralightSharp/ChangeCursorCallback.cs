using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeCursorCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, Cursor cursor);

}