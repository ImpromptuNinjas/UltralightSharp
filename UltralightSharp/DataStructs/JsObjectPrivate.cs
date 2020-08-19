using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public unsafe struct JsObjectPrivate {

    public bool IsContextGlobal;

    public JsContext* Context;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public sealed unsafe class JsObjectPrivate {

      public readonly UltralightSharp.JsObjectPrivate* _;

      public static implicit operator UltralightSharp.JsObjectPrivate*(in JsObjectPrivate x)
        => x._;

      public JsContext Context {
        get {
          if (_->IsContextGlobal)
            return new JsGlobalContext(_->Context);

          return new JsLocalContext(_->Context);
        }
        set {
          _->IsContextGlobal = value is JsGlobalContext;
          _->Context = value._;
        }
      }

    }

  }

}