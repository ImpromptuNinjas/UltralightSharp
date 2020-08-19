using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ClipboardWritePlainTextCallback([NativeTypeName("ULString")] String* text);

  namespace Safe {

    [PublicAPI]
    public delegate bool ClipboardWritePlainTextCallback(string? path);

  }

}