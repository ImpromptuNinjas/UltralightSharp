using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ClipboardReadPlainTextCallback([NativeTypeName("ULString")] String* result);

  namespace Safe {

    [PublicAPI]
    public delegate bool ClipboardReadPlainTextCallback(string? path);

  }
  
}