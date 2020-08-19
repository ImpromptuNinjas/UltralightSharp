using System.ComponentModel;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  public readonly ref struct JsContextGroup {

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsContextGroup {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsContextGroup* Unsafe => _;

      internal readonly UltralightSharp.JsContextGroup* _;

      public JsContextGroup(UltralightSharp.JsContextGroup* p)
        => _ = p;

    }

  }

}