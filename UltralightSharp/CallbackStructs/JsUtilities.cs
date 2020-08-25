using System;
using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  internal static class JsUtilities {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe UltralightSharp.JsValue*
      SafeWrapperForObjectCallAsFunctionCallback(
        ObjectCallAsFunctionCallback value,
        UltralightSharp.JsContext* ctx,
        UltralightSharp.JsValue* function,
        UltralightSharp.JsValue* thisObject,
        UIntPtr count,
        UltralightSharp.JsValue** arguments,
        UltralightSharp.JsValue** exception
      ) {
      var localCtx = new JsLocalContext(ctx);
      var argCount = (int) count;
      var args = new JsValueLike?[argCount];
      for (var i = 0; i < argCount; ++i)
        args[i] = JsValueLike.Create(arguments[i], localCtx);
      var rv = value(
        new JsObject(function, localCtx),
        JsValueLike.Create(thisObject, localCtx),
        args,
        out var exc
      );
      var r = rv == null ? null : rv.Unsafe;
      if (exc != null && exception != null)
        *exception = exc.Unsafe;
      if (r == null)
        r = localCtx._->MakeUndefined();
      return r;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe UltralightSharp.JsValue*
      SafeWrapperForObjectCallAsFunctionCallback(
        ObjectCallAsFunctionCallbackEx value,
        UltralightSharp.JsContext* ctx,
        UltralightSharp.JsClass* c,
        UltralightSharp.JsString* className,
        UltralightSharp.JsValue* function,
        UltralightSharp.JsValue* thisObject,
        UIntPtr count,
        UltralightSharp.JsValue** arguments,
        UltralightSharp.JsValue** exception
      ) {
      var localCtx = new JsLocalContext(ctx);
      var argCount = (int) count;
      var args = new JsValueLike?[argCount];
      for (var i = 0; i < argCount; ++i)
        args[i] = JsValueLike.Create(arguments[i], localCtx);
      var rv = value(
        new JsClass(c),
        new JsString(className),
        new JsObject(function, localCtx),
        JsValueLike.Create(thisObject, localCtx),
        args,
        out var exc
      );
      var r = rv == null ? null : rv.Unsafe;
      if (exc != null && exception != null)
        *exception = exc.Unsafe;
      if (r == null)
        r = localCtx._->MakeUndefined();
      return r;
    }

  }

}