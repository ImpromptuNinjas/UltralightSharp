using System.ComponentModel;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  public readonly ref struct JsPropertyNameAccumulator {

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsPropertyNameAccumulator {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsPropertyNameAccumulator* Unsafe => _;

      internal UltralightSharp.JsPropertyNameAccumulator* _;

      public JsPropertyNameAccumulator(UltralightSharp.JsPropertyNameAccumulator* p)
        => _ = p;

    }

  }

}