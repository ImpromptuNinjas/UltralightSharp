using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct MouseEvent {

    public static unsafe MouseEvent* Create(MouseEventType type, int x, int y, MouseButton button)
      => Ultralight.CreateMouseEvent(type, x, y, button);

  }

  [PublicAPI]
  public static class MouseEventExtensions {

    public static unsafe void Destroy(in this MouseEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyMouseEvent((MouseEvent*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class MouseEvent : IDisposable {

      internal readonly unsafe UltralightSharp.MouseEvent* _;

      public unsafe MouseEvent(UltralightSharp.MouseEvent* p)
        => _ = p;

      public unsafe MouseEvent(MouseEventType type, int x, int y, MouseButton button)
        => _ = UltralightSharp.MouseEvent.Create(type, x, y, button);

      public unsafe void Dispose()
        => _->Destroy();

    }

  }

}