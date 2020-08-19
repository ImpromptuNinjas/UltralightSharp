using System;
using System.ComponentModel;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct KeyEvent {

    public static KeyEvent* Create(KeyEventType type, uint modifiers, int virtualKeyCode, int nativeKeyCode, String* text, String* unmodifiedText, bool isKeypad, bool isAutoRepeat, bool isSystemKey)
      => Ultralight.CreateKeyEvent(type, modifiers, virtualKeyCode, nativeKeyCode, text, unmodifiedText, isKeypad, isAutoRepeat, isSystemKey);

    public static KeyEvent* CreateWindows(KeyEventType type, UIntPtr wParam, IntPtr lParam, bool isSystemKey)
      => Ultralight.CreateKeyEventWindows(type, wParam, lParam, isSystemKey);

  }

  [PublicAPI]
  public static unsafe class KeyEventExtensions {

    public static void Destroy(in this KeyEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyKeyEvent((KeyEvent*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class KeyEvent : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.KeyEvent* Unsafe => _;

      internal readonly UltralightSharp.KeyEvent* _;

      public KeyEvent(UltralightSharp.KeyEvent* p)
        => _ = p;

      public KeyEvent(KeyEventType type, uint modifiers, int virtualKeyCode, int nativeKeyCode, String* text, String* unmodifiedText, bool isKeypad, bool isAutoRepeat, bool isSystemKey)
        => _ = UltralightSharp.KeyEvent.Create(type, modifiers, virtualKeyCode, nativeKeyCode, text, unmodifiedText, isKeypad, isAutoRepeat, isSystemKey);

      public KeyEvent(KeyEventType type, UIntPtr wParam, IntPtr lParam, bool isSystemKey)
        => _ = UltralightSharp.KeyEvent.CreateWindows(type, wParam, lParam, isSystemKey);

      public void Dispose()
        => _->Destroy();

    }

  }

}