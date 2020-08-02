using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void LoggerLogMessageCallback(LogLevel logLevel, [NativeTypeName("ULString")] String* message);

  namespace Safe {

    [PublicAPI]
    public delegate void LoggerLogMessageCallback(LogLevel logLevel, string message);

  }

}