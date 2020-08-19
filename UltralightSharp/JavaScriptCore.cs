using System;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public static unsafe class JavaScriptCore {

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSEvaluateScript")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* EvaluateScript([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* script, [NativeTypeName("JSObjectRef")] JsValue* thisObject,
      [NativeTypeName("JSStringRef")] JsString* sourceUrl, int startingLineNumber, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSCheckScriptSyntax")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean CheckScriptSyntax([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* script, [NativeTypeName("JSStringRef")] JsString* sourceUrl, int startingLineNumber,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGarbageCollect")]
    public static extern void GarbageCollect([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueGetType")]
    public static extern JsType JsValueGetType([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsUndefined")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsUndefined([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsNull")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsNull([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsBoolean")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsBoolean([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsNumber")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsNumber([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsString")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsString([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsSymbol")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsSymbol([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsObject")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsObject([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsObjectOfClass")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsObjectOfClass([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSClassRef")] JsClass* jsClass);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsArray")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsArray([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsDate")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsDate([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueGetTypedArrayType")]
    public static extern JsTypedArrayType JsValueGetTypedArrayType([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsEqual")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsEqual([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* a, [NativeTypeName("JSValueRef")] JsValue* b,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsStrictEqual")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsStrictEqual([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* a, [NativeTypeName("JSValueRef")] JsValue* b);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueIsInstanceOfConstructor")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueIsInstanceOfConstructor([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSObjectRef")] JsValue* constructor,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeUndefined")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeUndefined([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeNull")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeNull([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeBoolean")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeBoolean([NativeTypeName("JSContextRef")] JsContext* ctx, OneByteBoolean boolean);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeNumber")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeNumber([NativeTypeName("JSContextRef")] JsContext* ctx, double number);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeString")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeString([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeSymbol")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeSymbol([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* description);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueMakeFromJSONString")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsValueMakeFromJsonString([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueCreateJSONString")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* JsValueCreateJsonString([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("unsigned int")] uint indent,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueToBoolean")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsValueToBoolean([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueToNumber")]
    public static extern double JsValueToNumber([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueToStringCopy")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* JsValueToStringCopy([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueToObject")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsValueToObject([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueProtect")]
    public static extern void JsValueProtect([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSValueUnprotect")]
    public static extern void JsValueUnprotect([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSClassCreate")]
    [return: NativeTypeName("JSClassRef")]
    public static extern JsClass* JsClassCreate([NativeTypeName("const JSClassDefinition *")]
      JsClassDefinition* definition);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSClassRetain")]
    [return: NativeTypeName("JSClassRef")]
    public static extern JsClass* JsClassRetain([NativeTypeName("JSClassRef")] JsClass* jsClass);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSClassRelease")]
    public static extern void JsClassRelease([NativeTypeName("JSClassRef")] JsClass* jsClass);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMake")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMake([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSClassRef")] JsClass* jsClass, [NativeTypeName("void *")] void* data);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeFunctionWithCallback")]
    [return: NativeTypeName("JSObjectRef")]
    private static extern JsValue* JsObjectMakeFunctionWithCallback([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* name,
      [NativeTypeName("JSObjectCallAsFunctionCallback")]
      IntPtr callAsFunction);

    public static JsValue* JsObjectMakeFunctionWithCallback(JsContext* ctx, JsString* name, FnPtr<ObjectCallAsFunctionCallback> callAsFunction)
      => JsObjectMakeFunctionWithCallback(ctx, name, (IntPtr) callAsFunction);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeConstructor")]
    [return: NativeTypeName("JSObjectRef")]
    private static extern JsValue* JsObjectMakeConstructor([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSClassRef")] JsClass* jsClass,
      [NativeTypeName("JSObjectCallAsConstructorCallback")]
      IntPtr callAsConstructor);

    public static JsValue* JsObjectMakeConstructor(JsContext* ctx, JsClass* jsClass, FnPtr<ObjectCallAsConstructorCallback> callAsConstructor)
      => JsObjectMakeConstructor(ctx, jsClass, (IntPtr) callAsConstructor);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeArray")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeArray([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("size_t")] UIntPtr argumentCount, [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeDate")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeDate([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("size_t")] UIntPtr argumentCount, [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeError")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeError([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("size_t")] UIntPtr argumentCount, [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeRegExp")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeRegExp([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("size_t")] UIntPtr argumentCount, [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeDeferredPromise")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeDeferredPromise([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef *")] JsValue** resolve, [NativeTypeName("JSObjectRef *")] JsValue** reject,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeFunction")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeFunction([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* name, [NativeTypeName("unsigned int")] uint parameterCount,
      [NativeTypeName("const JSStringRef []")]
      JsString** parameterNames, [NativeTypeName("JSStringRef")] JsString* body, [NativeTypeName("JSStringRef")] JsString* sourceUrl, int startingLineNumber, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetPrototype")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsObjectGetPrototype([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectSetPrototype")]
    public static extern void JsObjectSetPrototype([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef")] JsValue* value);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectHasProperty")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectHasProperty([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSStringRef")] JsString* propertyName);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetProperty")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsObjectGetProperty([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSStringRef")] JsString* propertyName,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectSetProperty")]
    public static extern void JsObjectSetProperty([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSStringRef")] JsString* propertyName,
      [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSPropertyAttributes")]
      JsPropertyAttribute attributes, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectDeleteProperty")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectDeleteProperty([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSStringRef")] JsString* propertyName,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectHasPropertyForKey")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectHasPropertyForKey([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef")] JsValue* propertyKey,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetPropertyForKey")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsObjectGetPropertyForKey([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef")] JsValue* propertyKey,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectSetPropertyForKey")]
    public static extern void JsObjectSetPropertyForKey([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef")] JsValue* propertyKey,
      [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSPropertyAttributes")]
      JsPropertyAttribute attributes, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectDeletePropertyForKey")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectDeletePropertyForKey([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef")] JsValue* propertyKey,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetPropertyAtIndex")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsObjectGetPropertyAtIndex([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("unsigned int")] uint propertyIndex,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectSetPropertyAtIndex")]
    public static extern void JsObjectSetPropertyAtIndex([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("unsigned int")] uint propertyIndex,
      [NativeTypeName("JSValueRef")] JsValue* value, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetPrivate")]
    [return: NativeTypeName("void *")]
    public static extern void* JsObjectGetPrivate([NativeTypeName("JSObjectRef")] JsValue* @object);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectSetPrivate")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectSetPrivate([NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("void *")] void* data);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectIsFunction")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectIsFunction([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectCallAsFunction")]
    [return: NativeTypeName("JSValueRef")]
    public static extern JsValue* JsObjectCallAsFunction([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSObjectRef")] JsValue* thisObject,
      [NativeTypeName("size_t")] UIntPtr argumentCount, [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectIsConstructor")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean JsObjectIsConstructor([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectCallAsConstructor")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectCallAsConstructor([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("size_t")] UIntPtr argumentCount,
      [NativeTypeName("const JSValueRef []")]
      JsValue** arguments, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectCopyPropertyNames")]
    [return: NativeTypeName("JSPropertyNameArrayRef")]
    public static extern JsPropertyNameArray* JsObjectCopyPropertyNames([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSPropertyNameArrayRetain")]
    [return: NativeTypeName("JSPropertyNameArrayRef")]
    public static extern JsPropertyNameArray* PropertyNameArrayRetain([NativeTypeName("JSPropertyNameArrayRef")]
      JsPropertyNameArray* array);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSPropertyNameArrayRelease")]
    public static extern void PropertyNameArrayRelease([NativeTypeName("JSPropertyNameArrayRef")]
      JsPropertyNameArray* array);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSPropertyNameArrayGetCount")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr PropertyNameArrayGetCount([NativeTypeName("JSPropertyNameArrayRef")]
      JsPropertyNameArray* array);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSPropertyNameArrayGetNameAtIndex")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* PropertyNameArrayGetNameAtIndex([NativeTypeName("JSPropertyNameArrayRef")]
      JsPropertyNameArray* array, [NativeTypeName("size_t")] UIntPtr index);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSPropertyNameAccumulatorAddName")]
    public static extern void PropertyNameAccumulatorAddName([NativeTypeName("JSPropertyNameAccumulatorRef")]
      JsPropertyNameAccumulator* accumulator, [NativeTypeName("JSStringRef")] JsString* propertyName);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGroupCreate")]
    [return: NativeTypeName("JSContextGroupRef")]
    public static extern JsContextGroup* ContextGroupCreate();

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGroupRetain")]
    [return: NativeTypeName("JSContextGroupRef")]
    public static extern JsContextGroup* ContextGroupRetain([NativeTypeName("JSContextGroupRef")] JsContextGroup* group);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGroupRelease")]
    public static extern void ContextGroupRelease([NativeTypeName("JSContextGroupRef")] JsContextGroup* group);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextCreate")]
    [return: NativeTypeName("JSGlobalContextRef")]
    public static extern JsContext* GlobalContextCreate([NativeTypeName("JSClassRef")] JsClass* globalObjectClass);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextCreateInGroup")]
    [return: NativeTypeName("JSGlobalContextRef")]
    public static extern JsContext* GlobalContextCreateInGroup([NativeTypeName("JSContextGroupRef")] JsContextGroup* group, [NativeTypeName("JSClassRef")] JsClass* globalObjectClass);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextRetain")]
    [return: NativeTypeName("JSGlobalContextRef")]
    public static extern JsContext* GlobalContextRetain([NativeTypeName("JSGlobalContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextRelease")]
    public static extern void GlobalContextRelease([NativeTypeName("JSGlobalContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGetGlobalObject")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* ContextGetGlobalObject([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGetGroup")]
    [return: NativeTypeName("JSContextGroupRef")]
    public static extern JsContextGroup* ContextGetGroup([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSContextGetGlobalContext")]
    [return: NativeTypeName("JSGlobalContextRef")]
    public static extern JsContext* ContextGetGlobalContext([NativeTypeName("JSContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextCopyName")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* GlobalContextCopyName([NativeTypeName("JSGlobalContextRef")] JsContext* ctx);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSGlobalContextSetName")]
    public static extern void GlobalContextSetName([NativeTypeName("JSGlobalContextRef")] JsContext* ctx, [NativeTypeName("JSStringRef")] JsString* name);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringCreateWithCharacters")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* StringCreateWithCharacters([NativeTypeName("const JSChar *")] char* chars, [NativeTypeName("size_t")] UIntPtr numChars);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringCreateWithUTF8CString")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* StringCreateWithUtf8CString([NativeTypeName("const char *")] sbyte* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringRetain")]
    [return: NativeTypeName("JSStringRef")]
    public static extern JsString* StringRetain([NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringRelease")]
    public static extern void StringRelease([NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringGetLength")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr StringGetLength([NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringGetCharactersPtr")]
    [return: NativeTypeName("const JSChar *")]
    public static extern char* StringGetCharactersPtr([NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringGetMaximumUTF8CStringSize")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr StringGetMaximumUtf8CStringSize([NativeTypeName("JSStringRef")] JsString* @string);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringGetUTF8CString")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr StringGetUtf8CString([NativeTypeName("JSStringRef")] JsString* @string, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("size_t")] UIntPtr bufferSize);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringIsEqual")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean StringIsEqual([NativeTypeName("JSStringRef")] JsString* a, [NativeTypeName("JSStringRef")] JsString* b);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSStringIsEqualToUTF8CString")]
    [return: NativeTypeName("bool")]
    public static extern OneByteBoolean StringIsEqualToUtf8CString([NativeTypeName("JSStringRef")] JsString* a, [NativeTypeName("const char *")] sbyte* b);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeTypedArray")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeTypedArray([NativeTypeName("JSContextRef")] JsContext* ctx, JsTypedArrayType arrayType, [NativeTypeName("size_t")] UIntPtr length, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeTypedArrayWithBytesNoCopy")]
    [return: NativeTypeName("JSObjectRef")]
    private static extern JsValue* JsObjectMakeTypedArrayWithBytesNoCopy([NativeTypeName("JSContextRef")] JsContext* ctx, JsTypedArrayType arrayType, [NativeTypeName("void *")] void* bytes, [NativeTypeName("size_t")] UIntPtr byteLength,
      [NativeTypeName("JSTypedArrayBytesDeallocator")]
      IntPtr bytesDeallocator, [NativeTypeName("void *")] void* deallocatorContext, [NativeTypeName("JSValueRef *")] JsValue** exception);

    public static JsValue* JsObjectMakeTypedArrayWithBytesNoCopy(JsContext* ctx, JsTypedArrayType arrayType, void* bytes, UIntPtr byteLength, FnPtr<TypedArrayBytesDeallocatorCallback> bytesDeallocator, void* deallocatorContext,
      JsValue** exception)
      => JsObjectMakeTypedArrayWithBytesNoCopy(ctx, arrayType, bytes, byteLength, (IntPtr) bytesDeallocator, deallocatorContext, exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeTypedArrayWithArrayBuffer")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeTypedArrayWithArrayBuffer([NativeTypeName("JSContextRef")] JsContext* ctx, JsTypedArrayType arrayType, [NativeTypeName("JSObjectRef")] JsValue* buffer,
      [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeTypedArrayWithArrayBufferAndOffset")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeTypedArrayWithArrayBufferAndOffset([NativeTypeName("JSContextRef")] JsContext* ctx, JsTypedArrayType arrayType, [NativeTypeName("JSObjectRef")] JsValue* buffer,
      [NativeTypeName("size_t")] UIntPtr byteOffset, [NativeTypeName("size_t")] UIntPtr length, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetTypedArrayBytesPtr")]
    [return: NativeTypeName("void *")]
    public static extern void* JsObjectGetTypedArrayBytesPtr([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetTypedArrayLength")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr JsObjectGetTypedArrayLength([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetTypedArrayByteLength")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr JsObjectGetTypedArrayByteLength([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetTypedArrayByteOffset")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr JsObjectGetTypedArrayByteOffset([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetTypedArrayBuffer")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectGetTypedArrayBuffer([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectMakeArrayBufferWithBytesNoCopy")]
    [return: NativeTypeName("JSObjectRef")]
    public static extern JsValue* JsObjectMakeArrayBufferWithBytesNoCopy([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("void *")] void* bytes, [NativeTypeName("size_t")] UIntPtr byteLength,
      [NativeTypeName("JSTypedArrayBytesDeallocator")]
      IntPtr bytesDeallocator, [NativeTypeName("void *")] void* deallocatorContext, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetArrayBufferBytesPtr")]
    [return: NativeTypeName("void *")]
    public static extern void* JsObjectGetArrayBufferBytesPtr([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

    [DllImport("WebCore", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, EntryPoint = "JSObjectGetArrayBufferByteLength")]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr JsObjectGetArrayBufferByteLength([NativeTypeName("JSContextRef")] JsContext* ctx, [NativeTypeName("JSObjectRef")] JsValue* @object, [NativeTypeName("JSValueRef *")] JsValue** exception);

  }

}