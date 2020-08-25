using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct Logger {

    [NativeTypeName("ULLoggerLogMessageCallback")]
    public FnPtr<LoggerLogMessageCallback> LogMessage;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Logger {

      internal UltralightSharp.Logger _;

      public static implicit operator UltralightSharp.Logger(in Logger x)
        => x._;

      public unsafe LoggerLogMessageCallback LogMessage {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.LoggerLogMessageCallback cb = (level, message)
            => value(level, message->Read());
          _.LogMessage = cb;
        }
      }

    }

  }

}