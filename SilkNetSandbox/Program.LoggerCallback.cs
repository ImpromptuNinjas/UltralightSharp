using System;
using System.Diagnostics;
using ImpromptuNinjas.UltralightSharp.Enums;
using ImpromptuNinjas.UltralightSharp.Safe;
using SixLabors.ImageSharp.PixelFormats;

partial class Program {

  private static void LoggerCallback(LogLevel logLevel, string? msg) {
    switch (logLevel) {
      case LogLevel.Error:
      case LogLevel.Warning: {
        Console.Error.WriteLine($"{logLevel.ToString()}: {msg}");
        Console.Error.Flush();
        Debugger.Break();
        break;
      }
      case LogLevel.Info: {
        Console.WriteLine($"{logLevel.ToString()}: {msg}");
        break;
      }
      default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
    }
  }

  private static void ConsoleMessageCallback(IntPtr ud, View caller, MessageSource source, MessageLevel level, string? message, uint lineNumber, uint columnNumber, string? sourceId) {
    switch (level) {
      case MessageLevel.Warning:
      case MessageLevel.Error: {
        Console.Error.WriteLine($"{level.ToString()} {source}:{lineNumber}:{columnNumber}: {message}");
        Console.Error.Flush();
        Debugger.Break();
        break;
      }

      default: {
        Console.WriteLine($"{level.ToString()} {source}:{lineNumber}:{columnNumber}: {message}");
        break;
      }
    }
  }

}