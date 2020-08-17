using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsContext {

  }

  namespace Safe {

    [PublicAPI]
    public sealed class JsContext {

      internal readonly unsafe UltralightSharp.JsContext* _;

      public unsafe JsContext(UltralightSharp.JsContext* p)
        => _ = p;

    }

  }

}