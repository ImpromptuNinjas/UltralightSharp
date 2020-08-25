using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ObjectGetPropertyNamesCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* @object,
    [NativeTypeName("JSPropertyNameAccumulatorRef")]
    JsPropertyNameAccumulator* propertyNames);

  namespace Safe {

    [PublicAPI]
    public delegate void ObjectGetPropertyNamesCallback(
      JsObject @object,
      JsPropertyNameAccumulator propertyNames
    );

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ObjectGetPropertyNamesCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSObjectRef")] JsValue* @object,
    [NativeTypeName("JSPropertyNameAccumulatorRef")]
    JsPropertyNameAccumulator* propertyNames);

  namespace Safe {

    [PublicAPI]
    public delegate void ObjectGetPropertyNamesCallbackEx(
      JsClass jsClass,
      JsObject @object,
      JsPropertyNameAccumulator propertyNames
    );

  }

}