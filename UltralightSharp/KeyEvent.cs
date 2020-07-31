using System;
using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

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

}