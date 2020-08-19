using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsClass {

  }

  [PublicAPI]
  public static unsafe class JsClassExtensions {

    public static JsClass* Retain(in this JsClass _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var jsClass);
      return JavaScriptCore.JsClassRetain((JsClass*) jsClass);
    }

    public static void Release(in this JsClass _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var jsClass);
      JavaScriptCore.JsClassRelease((JsClass*) jsClass);
    }

    public static JsContext* CreateGlobalContext(in this JsClass _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var globalObjectClass);
      return JavaScriptCore.GlobalContextCreate((JsClass*) globalObjectClass);
    }

    public static JsContext* CreateGlobalContextInGroup(in this JsClass _, JsContextGroup* group) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var globalObjectClass);
      return JavaScriptCore.GlobalContextCreateInGroup(group, (JsClass*) globalObjectClass);
    }

    public static OneByteBoolean IsObjectOfClass(in this JsClass _, JsContext* ctx, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var jsClass);
      return JavaScriptCore.JsValueIsObjectOfClass(ctx, value, (JsClass*) jsClass);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsClass : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsClass* Unsafe => _;

      internal readonly UltralightSharp.JsClass* _;

      public JsClass(UltralightSharp.JsClass* p) {
        if (p == null) throw new ArgumentNullException(nameof(p));

        _ = p->Retain();
      }

      public void Dispose() {
        _->Release();
      }

      public JsContext CreateGlobalContext()
        => new JsGlobalContext(_->CreateGlobalContext());

      public JsContext CreateGlobalContextInGroup(JsContextGroup group)
        => new JsGlobalContext(_->CreateGlobalContextInGroup(group._));

      public bool IsObjectOfClass(JsValueLike value)
        => _->IsObjectOfClass(value.Context._, value.Unsafe);

    }

  }

}