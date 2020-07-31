using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct Config {

    public static unsafe Config* Create()
      => Ultralight.CreateConfig();

  }

  public static class ConfigExtensions {

    public static unsafe void Destroy(in this Config _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyConfig((Config*) p);
    }

  }

}