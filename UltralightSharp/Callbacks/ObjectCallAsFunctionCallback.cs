using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSValueRef")]
  public unsafe delegate JsValue* ObjectCallAsFunctionCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* function,
    [NativeTypeName("JSObjectRef")] JsValue* thisObject,
    [NativeTypeName("size_t")] UIntPtr argumentCount,
    [NativeTypeName("const JSValueRef []")]
    JsValue** arguments,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectCallAsFunctionCallback(
      JsObject function,
      JsValueLike? thisObject,
      IReadOnlyCollection<JsValueLike?> arguments,
      out JsValueLike? exception
    );

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSValueRef")]
  public unsafe delegate JsValue* ObjectCallAsFunctionCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSStringRef")] JsString* className,
    [NativeTypeName("JSObjectRef")] JsValue* function,
    [NativeTypeName("JSObjectRef")] JsValue* thisObject,
    [NativeTypeName("size_t")] UIntPtr argumentCount,
    [NativeTypeName("const JSValueRef []")]
    JsValue** arguments,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectCallAsFunctionCallbackEx(
      JsClass jsClass,
      JsString className,
      JsObject function,
      JsValueLike? thisObject,
      IReadOnlyCollection<JsValueLike?> arguments,
      out JsValueLike? exception
    );

  }

}