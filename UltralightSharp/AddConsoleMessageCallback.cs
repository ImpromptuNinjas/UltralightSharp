using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void AddConsoleMessageCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, MessageSource source, MessageLevel level, [NativeTypeName("ULString")] String* message,
    [NativeTypeName("unsigned int")] uint lineNumber, [NativeTypeName("unsigned int")] uint columnNumber, [NativeTypeName("ULString")] String* sourceId);

}