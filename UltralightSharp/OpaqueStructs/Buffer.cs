using System.ComponentModel;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Buffer {

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Buffer {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Buffer* Unsafe => _;

      internal readonly UltralightSharp.Buffer* _;

      public Buffer(UltralightSharp.Buffer* p)
        => _ = p;

    }

  }

}