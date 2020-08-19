using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool ObjectHasPropertyCallback([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSStringRef")] JsString* propertyName);

  namespace Safe {

    [PublicAPI]
    public delegate bool ObjectHasPropertyCallback(
      JsObject @object,
      JsString propertyName
    );

  }

}