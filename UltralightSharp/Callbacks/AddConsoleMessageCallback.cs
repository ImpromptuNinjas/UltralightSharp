using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using ImpromptuNinjas.UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void AddConsoleMessageCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, MessageSource source, MessageLevel level, [NativeTypeName("ULString")] String* message,
    [NativeTypeName("unsigned int")] uint lineNumber, [NativeTypeName("unsigned int")] uint columnNumber, [NativeTypeName("ULString")] String* sourceId);

  namespace Safe {

    [PublicAPI]
    public delegate void AddConsoleMessageCallback(IntPtr userData, View caller, MessageSource source, MessageLevel level, string? message, uint lineNumber, uint columnNumber, string? sourceId);

  }

}