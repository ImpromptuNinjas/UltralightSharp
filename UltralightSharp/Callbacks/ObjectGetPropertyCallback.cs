using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSValueRef")]
  public unsafe delegate JsValue* ObjectGetPropertyCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* @object,
    [NativeTypeName("JSStringRef")] JsString* propertyName,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectGetPropertyCallback(
      JsObject @object,
      JsString propertyName,
      out JsValueLike? exception
    );

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSValueRef")]
  public unsafe delegate JsValue* ObjectGetPropertyCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSObjectRef")] JsValue* @object,
    [NativeTypeName("JSStringRef")] JsString* propertyName,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectGetPropertyCallbackEx(
      JsClass jsClass,
      JsObject @object,
      JsString propertyName,
      out JsValueLike? exception
    );

  }

}