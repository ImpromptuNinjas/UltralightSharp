using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSObjectRef")]
  public unsafe delegate JsValue* ObjectCallAsConstructorCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* constructor,
    [NativeTypeName("size_t")] UIntPtr argumentCount,
    [NativeTypeName("const JSValueRef []")]
    JsValue** arguments,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectCallAsConstructorCallback(
      JsObject ctor,
      IReadOnlyCollection<JsValueLike?> arguments,
      out JsValueLike? exception
    );

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSObjectRef")]
  public unsafe delegate JsValue* ObjectCallAsConstructorCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSObjectRef")] JsValue* constructor,
    [NativeTypeName("size_t")] UIntPtr argumentCount,
    [NativeTypeName("const JSValueRef []")]
    JsValue** arguments,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectCallAsConstructorCallbackEx(
      JsClass jsClass,
      JsObject ctor,
      IReadOnlyCollection<JsValueLike?> arguments,
      out JsValueLike? exception
    );

  }

}