using System;
using System.Diagnostics;
using Silk.NET.OpenGLES;
using Silk.NET.OpenGLES.Extensions.KHR;

partial class Program {

  public static unsafe void EnableDebugExtension() {
    if (!_gl.TryGetExtension(out KhrDebug dbg)) {
      Console.Error.WriteLine("Can't enable GL_KHR_debug extension not present.");
      Console.Error.Flush();
      return;
    }

    _gl.Enable(EnableCap.DebugOutput);
    _gl.Enable(EnableCap.DebugOutputSynchronous);

    Console.WriteLine("GL_KHR_debug extension enabled.");

    dbg.DebugMessageCallback(DebugMessageHandler, default);
  }

  private static unsafe void DebugMessageHandler(KHR source, KHR type, int id, KHR severity, int length, IntPtr message, IntPtr param) {
    var msg = new string((sbyte*) message, 0, length);
    var fi = 2;
    var cause = new StackFrame(fi, true);
    while (cause.GetFileName() == null)
      cause = new StackFrame(++fi, true);

    var est = new StackTrace(cause);

    var level = (DebugSeverity) severity;
    var prefixedMsg = $"{level}: {type} ({id}): {msg}\n{est}";

    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
    switch ((DebugType) type) {
      case DebugType.DebugTypeUndefinedBehavior:
      case DebugType.DebugTypeDeprecatedBehavior:
      case DebugType.DebugTypeError: {
        Console.Error.WriteLine(prefixedMsg);
        Console.Error.Flush();
        Debugger.Break();
        break;
      }
      default: {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (level) {
          case DebugSeverity.DontCare:
            Console.WriteLine(prefixedMsg);
            break;
          case DebugSeverity.DebugSeverityNotification:
            Console.WriteLine(prefixedMsg);
            break;
          case DebugSeverity.DebugSeverityLow:
            Console.WriteLine(prefixedMsg);
            break;
          case DebugSeverity.DebugSeverityMedium:
            Console.Error.WriteLine(prefixedMsg);
            Console.Error.Flush();
            break;
          case DebugSeverity.DebugSeverityHigh:
            Console.Error.WriteLine(prefixedMsg);
            Console.Error.Flush();
            break;
          default:
            Console.Error.WriteLine(prefixedMsg);
            Console.Error.Flush();
            break;
        }

        break;
      }
      case DebugType.DebugTypeMarker:
      case DebugType.DebugTypePushGroup:
      case DebugType.DebugTypePopGroup: {
        Console.WriteLine(prefixedMsg);
        break;
      }
    }
  }

}