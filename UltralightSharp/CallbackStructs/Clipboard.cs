using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public struct Clipboard {

    [NativeTypeName("ULClipboardClear")]
    public FnPtr<ClipboardClearCallback> Clear;

    [NativeTypeName("ULClipboardReadPlainText")]
    public FnPtr<ClipboardReadPlainTextCallback> ReadPlainText;

    [NativeTypeName("ULClipboardWritePlainText")]
    public FnPtr<ClipboardWritePlainTextCallback> WritePlainText;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Clipboard {

      private UltralightSharp.Clipboard _;

      public static implicit operator UltralightSharp.Clipboard(in Clipboard x)
        => x._;

      public ClipboardClearCallback Clear {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ClipboardClearCallback cb
            = () => value();
          _.Clear = cb;
        }
      }

      public unsafe ClipboardReadPlainTextCallback ReadPlainText {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ClipboardReadPlainTextCallback cb
            = txt => value(txt->Read());
          _.ReadPlainText = cb;
        }
      }

      public unsafe ClipboardWritePlainTextCallback WritePlainText {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ClipboardWritePlainTextCallback cb
            = path => value(path->Read());
          _.WritePlainText = cb;
        }
      }

    }

  }

}