using System;
using System.ComponentModel;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct ScrollEvent {

    public static ScrollEvent* Create(ScrollEventType type, int deltaX, int deltaY)
      => Ultralight.CreateScrollEvent(type, deltaX, deltaY);

  }

  [PublicAPI]
  public static unsafe class ScrollEventExtensions {

    public static void Destroy(in this ScrollEvent _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyScrollEvent((ScrollEvent*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class ScrollEvent : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.ScrollEvent* Unsafe => _;

      internal readonly UltralightSharp.ScrollEvent* _;

      public ScrollEvent(UltralightSharp.ScrollEvent* p)
        => _ = p;

      public ScrollEvent(ScrollEventType type, int deltaX, int deltaY)
        => _ = UltralightSharp.ScrollEvent.Create(type, deltaX, deltaY);

      public void Dispose()
        => _->Destroy();

    }

  }

}