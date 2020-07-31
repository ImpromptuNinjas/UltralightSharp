using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct ScrollEvent {

    public static unsafe ScrollEvent* Create(ScrollEventType type, int deltaX, int deltaY)
      => Ultralight.CreateScrollEvent(type, deltaX, deltaY);

  }

  [PublicAPI]
  public static class ScrollEventExtensions {

    public static unsafe void Destroy(in this ScrollEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyScrollEvent((ScrollEvent*) p);
    }

  }

}