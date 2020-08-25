using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ObjectInitializeCallback(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSObjectRef")] JsValue* @object
  );

  namespace Safe {

    [PublicAPI]
    public delegate void ObjectInitializeCallback(JsObject? @object);

  }

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ObjectInitializeCallbackEx(
    [NativeTypeName("JSContextRef")] JsContext* ctx,
    [NativeTypeName("JSClassRef")] JsClass* jsClass,
    [NativeTypeName("JSObjectRef")] JsValue* @object
  );

  namespace Safe {

    [PublicAPI]
    public delegate void ObjectInitializeCallbackEx(
      JsClass jsClass,
      JsObject? @object
    );

  }

}