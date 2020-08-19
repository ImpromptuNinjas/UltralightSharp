using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("JSValueRef")]
  public unsafe delegate JsValue* ObjectConvertToTypeCallback([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, JsType type, [NativeTypeName("JSValueRef *")] JsValue** exception);

  namespace Safe {

    [PublicAPI]
    public delegate JsValueLike? ObjectConvertToTypeCallback(
      JsObject @object,
      JsType type,
      out JsValueLike? exception
    );

  }

}