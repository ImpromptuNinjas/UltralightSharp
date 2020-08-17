using System;
using InlineIL;
using JetBrains.Annotations;
using ImpromptuNinjas.UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct KeyEvent {

    public static unsafe KeyEvent* Create(KeyEventType type, uint modifiers, int virtualKeyCode, int nativeKeyCode, String* text, String* unmodifiedText, bool isKeypad, bool isAutoRepeat, bool isSystemKey)
      => Ultralight.CreateKeyEvent(type, modifiers, virtualKeyCode, nativeKeyCode, text, unmodifiedText, isKeypad, isAutoRepeat, isSystemKey);

    public static unsafe KeyEvent* CreateWindows(KeyEventType type, UIntPtr wParam, IntPtr lParam, bool isSystemKey)
      => Ultralight.CreateKeyEventWindows(type, wParam, lParam, isSystemKey);

  }

  [PublicAPI]
  public static class KeyEventExtensions {

    public static unsafe void Destroy(in this KeyEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyKeyEvent((KeyEvent*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class KeyEvent : IDisposable {

      public unsafe UltralightSharp.KeyEvent* Unsafe => _;

      internal readonly unsafe UltralightSharp.KeyEvent* _;

      public unsafe KeyEvent(UltralightSharp.KeyEvent* p)
        => _ = p;
      public unsafe KeyEvent(KeyEventType type, uint modifiers, int virtualKeyCode, int nativeKeyCode, String* text, String* unmodifiedText, bool isKeypad, bool isAutoRepeat, bool isSystemKey)
        => _ = UltralightSharp.KeyEvent.Create(type, modifiers, virtualKeyCode, nativeKeyCode, text, unmodifiedText, isKeypad, isAutoRepeat, isSystemKey);

      public unsafe KeyEvent(KeyEventType type, UIntPtr wParam, IntPtr lParam, bool isSystemKey)
        => _ = UltralightSharp.KeyEvent.CreateWindows(type, wParam, lParam, isSystemKey);

      public unsafe void Dispose()
        => _->Destroy();
    }

  }

}