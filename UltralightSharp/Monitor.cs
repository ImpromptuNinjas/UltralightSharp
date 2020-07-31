using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

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

}