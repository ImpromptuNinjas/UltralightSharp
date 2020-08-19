using System;
using System.ComponentModel;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct MouseEvent {

    public static MouseEvent* Create(MouseEventType type, int x, int y, MouseButton button)
      => Ultralight.CreateMouseEvent(type, x, y, button);

  }

  [PublicAPI]
  public static unsafe class MouseEventExtensions {

    public static void Destroy(in this MouseEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyMouseEvent((MouseEvent*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class MouseEvent : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.MouseEvent* Unsafe => _;

      internal readonly UltralightSharp.MouseEvent* _;

      public MouseEvent(UltralightSharp.MouseEvent* p)
        => _ = p;

      public MouseEvent(MouseEventType type, int x, int y, MouseButton button)
        => _ = UltralightSharp.MouseEvent.Create(type, x, y, button);

      public void Dispose()
        => _->Destroy();

    }

  }

}