using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void LoggerLogMessageCallback(LogLevel logLevel, [NativeTypeName("ULString")] String* message);

}