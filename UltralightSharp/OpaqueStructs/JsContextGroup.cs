using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsContextGroup {

    public static unsafe JsContextGroup* Create()
      => JavaScriptCore.ContextGroupCreate();

  }

  [PublicAPI]
  [SuppressMessage("ReSharper", "UnusedParameter.Global")]
  public static unsafe class JsContextGroupExtensions {

    public static JsContextGroup* Retain(in this JsContextGroup _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var g);
      return JavaScriptCore.ContextGroupRetain((JsContextGroup*) g);
    }

    public static void Release(in this JsContextGroup _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var g);
      JavaScriptCore.ContextGroupRelease((JsContextGroup*) g);
    }

    public static JsContext* CreateContext(in this JsContextGroup _, JsClass* globalObjectClass) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var g);
      return JavaScriptCore.GlobalContextCreateInGroup((JsContextGroup*) g, globalObjectClass);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsContextGroup : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsContextGroup* Unsafe => _;

      internal readonly UltralightSharp.JsContextGroup* _;

      public JsContextGroup(UltralightSharp.JsContextGroup* p) {
        _ = p;
        _->Retain();
      }

      public void Dispose() {
        _->Release();
      }

      public JsContext CreateContext(JsClass globalObjectClass)
        => new JsGlobalContext(_->CreateContext(globalObjectClass._));

    }

  }

}