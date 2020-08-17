using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Monitor {

  }

  [PublicAPI]
  public static class MonitorExtensions {

    public static unsafe uint GetHeight(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetHeight((Monitor*) p);
    }

    public static unsafe uint GetWidth(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetWidth((Monitor*) p);
    }

    public static unsafe double GetScale(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetScale((Monitor*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Monitor {

      public unsafe UltralightSharp.Monitor* Unsafe => _;

      internal readonly unsafe UltralightSharp.Monitor* _;

      public unsafe Monitor(UltralightSharp.Monitor* p)
        => _ = p;

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe double GetScale()
        => _->GetScale();

    }

  }

}