using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

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

    public static unsafe Session* GetDefaultSession(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.DefaultSession((Renderer*) p);
    }

  }

}