using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

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

  namespace Safe {

    [PublicAPI]
    public sealed class ScrollEvent : IDisposable {

      internal readonly unsafe UltralightSharp.ScrollEvent* _;

      public unsafe ScrollEvent(UltralightSharp.ScrollEvent* p)
        => _ = p;
      public unsafe ScrollEvent(ScrollEventType type, int deltaX, int deltaY)
        => _ = UltralightSharp.ScrollEvent.Create(type, deltaX, deltaY);

      public unsafe void Dispose()
        => _->Destroy();
    }

  }

}