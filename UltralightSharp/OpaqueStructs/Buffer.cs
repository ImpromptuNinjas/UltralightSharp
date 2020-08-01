using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Buffer {

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Buffer {

      internal readonly unsafe UltralightSharp.Buffer* _;

      public unsafe Buffer(UltralightSharp.Buffer* p)
        => _ = p;

    }

  }

}