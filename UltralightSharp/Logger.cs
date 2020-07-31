using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct Logger {

    [NativeTypeName("ULLoggerLogMessageCallback")]
    public FnPtr<LoggerLogMessageCallback> LogMessage;

  }

}