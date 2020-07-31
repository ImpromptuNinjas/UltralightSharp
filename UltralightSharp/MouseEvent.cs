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

}