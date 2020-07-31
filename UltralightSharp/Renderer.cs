using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct Renderer {

    public static unsafe Renderer* Create(Config* config)
      => Ultralight.CreateRenderer(config);

  }

  public static class RendererExtensions {

    public static unsafe void Destroy(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyRenderer((Renderer*) p);
    }

  }

}