using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool ObjectSetPropertyCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* @object,
    [NativeTypeName("JSStringRef")] JsString* propertyName,
    [NativeTypeName("JSValueRef")] JsValue* value,
    [NativeTypeName("JSValueRef *")] JsValue** exception
  );

  namespace Safe {

    [PublicAPI]
    public delegate bool ObjectSetPropertyCallback(
      JsObject @object,
      JsString propertyName,
      JsValueLike? value,
      out JsValueLike? exception
    );

  }

}