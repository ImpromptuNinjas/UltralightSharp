using System;
using System.Diagnostics;
using ImpromptuNinjas.UltralightSharp.Enums;
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

}