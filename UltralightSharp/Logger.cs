using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public struct Logger {

    [NativeTypeName("ULLoggerLogMessageCallback")]
    public FnPtr<LoggerLogMessageCallback> LogMessage;

  }

}