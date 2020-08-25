using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool ObjectHasInstanceCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* constructor,
    [NativeTypeName("JSValueRef")] JsValue* possibleInstance,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate bool ObjectHasInstanceCallback(
      JsObject constructor,
      JsValueLike? possibleInstance,
      out JsValueLike? exception
    );

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool ObjectHasInstanceCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSObjectRef")] JsValue* constructor,
    [NativeTypeName("JSValueRef")] JsValue* possibleInstance,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate bool ObjectHasInstanceCallbackEx(
      JsClass jsClass,
      JsObject constructor,
      JsValueLike? possibleInstance,
      out JsValueLike? exception
    );

  }

}