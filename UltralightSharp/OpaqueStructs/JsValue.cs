using System;
using System.Diagnostics.CodeAnalysis;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsValue {

  }

  [PublicAPI]
  public static unsafe class JsValueExtensions {

    public static bool TryGetJsObjectPrivate(
      in this JsValue _,
#if NETFRAMEWORK || NETSTANDARD2_0
      out Safe.JsObjectPrivate objPrivate
#else
      [NotNullWhen(true)] out Safe.JsObjectPrivate? objPrivate
#endif
    ) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var pValue);
      var value = (JsValue*) pValue;
      objPrivate = null!;

      var p = JavaScriptCore.JsObjectGetPrivate(value);

      if (p == null)
        // nothing we can do here either
        return false;

      var pStruct = (JsObjectPrivate*) p;
      // back the pointer up to the potential gc obj ref
      var pMethodTable = (void**) (IntPtr) pStruct - sizeof(void*);
      var methodTableValue = ((UIntPtr) pMethodTable).ToUInt64();
      if ((methodTableValue & 0xFFFF000000000007u) != 0)
        // probably not a valid pointer
        return false;

      // check if the method table pointer matches the type
      var methodTable = (IntPtr) (*pMethodTable);

      if (methodTable != typeof(Safe.JsObjectPrivate).TypeHandle.Value)
        // doesn't match, can't interpret it
        return false;

      IL.Push((IntPtr) pMethodTable);
      IL.Pop(out Safe.JsObjectPrivate aGcOp);
      objPrivate = aGcOp;
      return true;
    }

  }

  namespace Safe {

    [PublicAPI]
    public abstract unsafe class JsValueLike : IDisposable {

      public readonly UltralightSharp.JsValue* Unsafe;

      public readonly JsContext Context;

      public static JsValueLike? Create(UltralightSharp.JsValue* p, JsContext ctx)
        => p == null ? null : ctx._->IsObject(p) ? (JsValueLike) new JsObject(p, ctx) : new JsValue(p, ctx);

      internal JsValueLike(UltralightSharp.JsValue* p, JsContext ctx) {
        Unsafe = p;
        Context = ctx;
        Context._->Protect(Unsafe);
      }

      public void Dispose() {
        Context._->Unprotect(Unsafe);
      }

      public JsType GetJsType()
        => Context._->GetJsType(Unsafe);

      public bool IsUndefined()
        => Context._->IsUndefined(Unsafe);

      public bool IsNull()
        => Context._->IsNull(Unsafe);

      public bool IsBoolean()
        => Context._->IsBoolean(Unsafe);

      public bool IsNumber()
        => Context._->IsNumber(Unsafe);

      public bool IsString()
        => Context._->IsString(Unsafe);

      public bool IsSymbol()
        => Context._->IsSymbol(Unsafe);

      public bool IsObject()
        => Context._->IsObject(Unsafe);

      public bool IsObjectOfClass(JsClass jsClass)
        => Context._->IsObjectOfClass(Unsafe, jsClass._);

      public bool IsArray()
        => Context._->IsArray(Unsafe);

      public bool IsDate()
        => Context._->IsDate(Unsafe);

      public JsTypedArrayType GetTypedArrayType(out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->GetTypedArrayType(Unsafe, &exc);
        exception = Create(exc, Context);
        return r;
      }

      public bool IsEqual(JsValue b, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->IsEqual(Unsafe, b.Unsafe, &exc);
        exception = Create(exc, Context);
        return r;
      }

      public OneByteBoolean IsStrictEqual(JsValue b)
        => Context._->IsStrictEqual(Unsafe, b.Unsafe);

      public OneByteBoolean IsInstanceOfConstructor(JsValueLike constructor, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->IsInstanceOfConstructor(Unsafe, constructor.Unsafe, &exc);
        exception = Create(exc, Context);
        return r;
      }

      public bool ToBoolean()
        => Context._->ToBoolean(Unsafe);

      public double ToNumber(out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->ToNumber(Unsafe, &exc);
        exception = Create(exc, Context);
        return r;
      }

      public JsString ToStringCopy(out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->ToStringCopy(Unsafe, &exc);
        exception = Create(exc, Context);
        return new JsString(r);
      }

      public JsObject ToObject(out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = Context._->ToObject(Unsafe, &exc);
        exception = Create(exc, Context);
        return new JsObject(r, Context);
      }

    }

    [PublicAPI]
    public sealed unsafe class JsValue : JsValueLike {

      internal JsValue(UltralightSharp.JsValue* p, JsContext ctx) : base(p, ctx) {
      }

    }

    [PublicAPI]
    public sealed unsafe class JsObject : JsValueLike {

      internal JsObject(UltralightSharp.JsValue* p, JsContext ctx) : base(p, ctx) {
      }

    }

  }

}