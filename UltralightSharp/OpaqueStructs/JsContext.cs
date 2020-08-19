using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsContext {

  }

  [SuppressMessage("ReSharper", "UnusedParameter.Global")]
  public static unsafe class JsContextExtensions {

    public static JsType GetJsType(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueGetType((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsUndefined(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsUndefined((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsNull(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsNull((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsBoolean(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsBoolean((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsNumber(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsNumber((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsString(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsString((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsSymbol(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsSymbol((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsObject(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsObject((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsObjectOfClass(in this JsContext _, JsValue* value, JsClass* jsClass) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsObjectOfClass((JsContext*) ctx, value, jsClass);
    }

    public static OneByteBoolean IsArray(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsArray((JsContext*) ctx, value);
    }

    public static OneByteBoolean IsDate(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsDate((JsContext*) ctx, value);
    }

    public static JsTypedArrayType GetTypedArrayType(in this JsContext _, JsValue* value, JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueGetTypedArrayType((JsContext*) ctx, value, exception);
    }

    public static OneByteBoolean IsEqual(in this JsContext _, JsValue* a, JsValue* b,
      JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsEqual((JsContext*) ctx, a, b, exception);
    }

    public static OneByteBoolean IsStrictEqual(in this JsContext _, JsValue* a, JsValue* b) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsStrictEqual((JsContext*) ctx, a, b);
    }

    public static OneByteBoolean IsInstanceOfConstructor(in this JsContext _, JsValue* value, JsValue* constructor,
      JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueIsInstanceOfConstructor((JsContext*) ctx, value, constructor, exception);
    }

    public static JsValue* MakeUndefined(in this JsContext _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeUndefined((JsContext*) ctx);
    }

    public static JsValue* MakeNull(in this JsContext _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeNull((JsContext*) ctx);
    }

    public static JsValue* MakeBoolean(in this JsContext _, OneByteBoolean boolean) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeBoolean((JsContext*) ctx, boolean);
    }

    public static JsValue* MakeNumber(in this JsContext _, double number) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeNumber((JsContext*) ctx, number);
    }

    public static JsValue* MakeString(in this JsContext _, JsString* @string) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeString((JsContext*) ctx, @string);
    }

    public static JsValue* MakeSymbol(in this JsContext _, JsString* description) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeSymbol((JsContext*) ctx, description);
    }

    public static JsValue* MakeFromJsonString(in this JsContext _, JsString* @string) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueMakeFromJsonString((JsContext*) ctx, @string);
    }

    public static JsString* CreateJsonString(in this JsContext _, JsValue* value, uint indent,
      JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueCreateJsonString((JsContext*) ctx, value, indent, exception);
    }

    public static OneByteBoolean ToBoolean(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueToBoolean((JsContext*) ctx, value);
    }

    public static double ToNumber(in this JsContext _, JsValue* value, JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueToNumber((JsContext*) ctx, value, exception);
    }

    public static JsString* ToStringCopy(in this JsContext _, JsValue* value, JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueToStringCopy((JsContext*) ctx, value, exception);
    }

    public static JsValue* ToObject(in this JsContext _, JsValue* value, JsValue** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      return JavaScriptCore.JsValueToObject((JsContext*) ctx, value, exception);
    }

    public static void Protect(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      JavaScriptCore.JsValueProtect((JsContext*) ctx, value);
    }

    public static void Unprotect(in this JsContext _, JsValue* value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ctx);
      JavaScriptCore.JsValueUnprotect((JsContext*) ctx, value);
    }

  }

  namespace Safe {

    [PublicAPI]
    public abstract class JsContext : IDisposable {

      private static ThreadLocal<(Thread Thread, LinkedList<JsContext> ContextStack)>
        _tlsContextStack = new ThreadLocal<(Thread, LinkedList<JsContext>)>
          (() => (Thread.CurrentThread, new LinkedList<JsContext>()), true);

      private static LinkedList<JsContext> ContextStack
        => _tlsContextStack.Value!.ContextStack;

      public static void PushThreadContext(JsContext ctx)
        => ContextStack.AddLast(ctx);

      public static JsContext? GetThreadContext()
        => ContextStack.Count == 0 ? null : ContextStack.Last!.Value;

      public static bool RemoveThreadContext(JsContext? ctx) {
        if (ctx == null)
          return false;
        if (ContextStack.Count == 0)
          return false;

        var node = ContextStack.FindLast(ctx);

        //var value = node!.Value;
        //value.Dispose();

        if (node == null)
          return false;

        ContextStack.Remove(node);
        return true;
      }

      public static void CleanAllThreadContextStacks() {
        foreach (var tls in _tlsContextStack.Values) {
          if (tls.Thread.IsAlive)
            continue;

          foreach (var ctx in tls.ContextStack)
            ctx.Dispose();
        }
      }

      public static void ReleaseAllThreadContextStacks() {
        foreach (var tls in _tlsContextStack.Values) {
          foreach (var ctx in tls.ContextStack)
            ctx.Dispose();
        }
      }

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public unsafe UltralightSharp.JsContext* Unsafe => _;

      internal readonly unsafe UltralightSharp.JsContext* _;

      internal unsafe JsContext(UltralightSharp.JsContext* p)
        => _ = p;

      public abstract void Dispose();

      public unsafe JsType GetJsType(JsValueLike value)
        => _->GetJsType(value.Unsafe);

      public unsafe bool IsUndefined(JsValueLike value)
        => _->IsUndefined(value.Unsafe);

      public unsafe bool IsNull(JsValueLike value)
        => _->IsNull(value.Unsafe);

      public unsafe bool IsBoolean(JsValueLike value)
        => _->IsBoolean(value.Unsafe);

      public unsafe bool IsNumber(JsValueLike value)
        => _->IsNumber(value.Unsafe);

      public unsafe bool IsString(JsValueLike value)
        => _->IsString(value.Unsafe);

      public unsafe bool IsSymbol(JsValueLike value)
        => _->IsSymbol(value.Unsafe);

      public unsafe bool IsObject(JsValueLike value)
        => _->IsObject(value.Unsafe);

      public unsafe bool IsObjectOfClass(JsValueLike value, JsClass jsClass)
        => _->IsObjectOfClass(value.Unsafe, jsClass._);

      public unsafe bool IsArray(JsValueLike value)
        => _->IsArray(value.Unsafe);

      public unsafe bool IsDate(JsValueLike value)
        => _->IsDate(value.Unsafe);

      public unsafe JsTypedArrayType GetTypedArrayType(JsValueLike value, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->GetTypedArrayType(value.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r;
      }

      public unsafe bool IsEqual(JsValueLike a, JsValueLike b, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->IsEqual(a.Unsafe, b.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r;
      }

      public unsafe bool IsStrictEqual(JsValueLike a, JsValueLike b)
        => _->IsStrictEqual(a.Unsafe, b.Unsafe);

      public unsafe bool IsInstanceOfConstructor(JsValueLike value, JsValueLike constructor, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->IsInstanceOfConstructor(value.Unsafe, constructor.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r;
      }

      public unsafe JsValue MakeUndefined()
        => new JsValue(_->MakeUndefined(), this)!;

      public unsafe JsValue MakeNull()
        => new JsValue(_->MakeNull(), this);

      public unsafe JsValue MakeBoolean(bool boolean)
        => new JsValue(_->MakeBoolean(boolean), this);

      public unsafe JsValue MakeNumber(double number)
        => new JsValue(_->MakeNumber(number), this);

      public unsafe JsValue? MakeString(JsString @string) {
        var r = _->MakeString(@string.Unsafe);
        return r == null ? null : new JsValue(r, this);
      }

      public unsafe JsValue? MakeSymbol(JsString description) {
        var r = _->MakeSymbol(description.Unsafe);
        return r == null ? null : new JsValue(r, this);
      }

      public unsafe JsValueLike? MakeFromJsonString(JsString @string)
        => JsValueLike.Create(_->MakeFromJsonString(@string.Unsafe), this);

      public unsafe JsString? CreateJsonString(JsValueLike value, uint indent, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->CreateJsonString(value.Unsafe, indent, &exc);
        exception = exc != null ? JsValueLike.Create(exc, this) : null;
        return r == null ? null : new JsString(r);
      }

      public unsafe bool ToBoolean(JsValueLike value)
        => _->ToBoolean(value.Unsafe);

      public unsafe double ToNumber(JsValueLike value, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->ToNumber(value.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r;
      }

      public unsafe JsString? ToStringCopy(JsValueLike value, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->ToStringCopy(value.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r == null ? null : new JsString(r);
      }

      public unsafe JsObject? ToObject(JsValueLike value, out JsValueLike? exception) {
        UltralightSharp.JsValue* exc;
        var r = _->ToObject(value.Unsafe, &exc);
        exception = JsValueLike.Create(exc, this);
        return r == null ? null : new JsObject(r, this);
      }

    }

    [PublicAPI]
    public sealed class JsGlobalContext : JsContext {

      public unsafe JsGlobalContext(UltralightSharp.JsContext* p) : base(p) {
        JavaScriptCore.GlobalContextRetain(_);
      }

      public override unsafe void Dispose() {
        JavaScriptCore.GlobalContextRelease(_);
      }

    }

    [PublicAPI]
    public sealed class JsLocalContext : JsContext {

      public unsafe JsLocalContext(UltralightSharp.JsContext* p) : base(p) {
      }

      public override void Dispose() {
      }

    }

  }

}