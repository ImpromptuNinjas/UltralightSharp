using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Monitor {

  }

  [PublicAPI]
  public static unsafe class MonitorExtensions {

    public static uint GetHeight(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetHeight((Monitor*) p);
    }

    public static uint GetWidth(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetWidth((Monitor*) p);
    }

    public static double GetScale(in this Monitor _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.MonitorGetScale((Monitor*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Monitor {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Monitor* Unsafe => _;

      internal readonly UltralightSharp.Monitor* _;

      public Monitor(UltralightSharp.Monitor* p)
        => _ = p;

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public double GetScale()
        => _->GetScale();

    }

  }

}