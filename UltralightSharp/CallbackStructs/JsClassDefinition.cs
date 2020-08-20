using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

#if NETFRAMEWORK || NETSTANDARD2_0
using System.Buffers;
#endif

#if DEBUG
using System.Diagnostics;
#endif

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public unsafe struct JsClassDefinition {

    public int Version;

    [NativeTypeName("JSClassAttributes")]
    public JsClassAttribute Attributes;

    [NativeTypeName("const char *")]
    public sbyte* ClassName;

    [NativeTypeName("JSClassRef")]
    public JsClass* ParentClass;

    [NativeTypeName("const JSStaticValue *")]
    public JsStaticValue* StaticValues;

    [NativeTypeName("const JSStaticFunction *")]
    public JsStaticFunction* StaticFunctions;

    [NativeTypeName("JSObjectInitializeCallback")]
    public FnPtr<ObjectInitializeCallback> Initialize;

    [NativeTypeName("JSObjectFinalizeCallback")]
    public FnPtr<ObjectFinalizeCallback> Finalizer;

    [NativeTypeName("JSObjectHasPropertyCallback")]
    public FnPtr<ObjectHasPropertyCallback> HasProperty;

    [NativeTypeName("JSObjectGetPropertyCallback")]
    public FnPtr<ObjectGetPropertyCallback> GetProperty;

    [NativeTypeName("JSObjectSetPropertyCallback")]
    public FnPtr<ObjectSetPropertyCallback> SetProperty;

    [NativeTypeName("JSObjectDeletePropertyCallback")]
    public FnPtr<ObjectDeletePropertyCallback> DeleteProperty;

    [NativeTypeName("JSObjectGetPropertyNamesCallback")]
    public FnPtr<ObjectGetPropertyNamesCallback> GetPropertyNames;

    [NativeTypeName("JSObjectCallAsFunctionCallback")]
    public FnPtr<ObjectCallAsFunctionCallback> CallAsFunction;

    [NativeTypeName("JSObjectCallAsConstructorCallback")]
    public FnPtr<ObjectCallAsConstructorCallback> CallAsConstructor;

    [NativeTypeName("JSObjectHasInstanceCallback")]
    public FnPtr<ObjectHasInstanceCallback> HasInstance;

    [NativeTypeName("JSObjectConvertToTypeCallback")]
    public FnPtr<ObjectConvertToTypeCallback> ConvertToType;

  }

  [PublicAPI]
  public static unsafe class JsClassDefinitionExtensions {

    public static JsClass* CreateClass(in this JsClassDefinition _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var definition);
      return JavaScriptCore.JsClassCreate((JsClassDefinition*) definition);
    }

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    public sealed unsafe class JsClassDefinition {

      private UltralightSharp.JsClassDefinition _;

      public static implicit operator UltralightSharp.JsClassDefinition(in JsClassDefinition x)
        => x._;

      [MustUseReturnValue]
      public GCHandle SetName(string name) {
        var bytes = Encoding.UTF8.GetBytes(name);
        var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        var ptr = Unsafe.AsPointer(ref bytes[0]);
        _.ClassName = (sbyte*) ptr;
        return pin;
      }

      public string Name {
        get {
          var len = new ReadOnlySpan<byte>(_.ClassName, int.MaxValue).IndexOf<byte>(0);
          if (len == -1)
            throw new InvalidOperationException("String length is too long (>2GB) or indeterminate.");

#if NETFRAMEWORK || NETSTANDARD2_0
          var buf = ArrayPool<byte>.Shared.Rent(len);
          fixed (byte* pBuf = buf)
            Unsafe.CopyBlockUnaligned(pBuf, _.ClassName, (uint) len);
          return Encoding.UTF8.GetString(buf);
#else
          var span = new ReadOnlySpan<byte>(_.ClassName, len);
          return Encoding.UTF8.GetString(span);
#endif
        }
        [Obsolete("Use the SetName method instead.", true)]
        set => throw new NotSupportedException("Use the SetName method instead.");
      }

      public JsClassAttribute Attributes {
        get => _.Attributes;
        set => _.Attributes = value;
      }

      public ObjectInitializeCallback Initialize {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectInitializeCallback cb
            = (ctx, o) => {
              var obj = new JsObject(o, new JsLocalContext(ctx));
              
              
              JavaScriptCore.JsObjectSetPrivate(o, );

              value(o);
            };
          _.Initialize = cb;
        }
      }

      public ObjectFinalizeCallback Finalizer {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectFinalizeCallback cb
            = o => {
              if (o == null)
                // nothing we can do here
                return;

              if (!JsValueLike.TryGetJsObjectPrivate(o, out var objPvt))
                return;

              var ctx = objPvt.CreateContext();

              if (ctx == null)
                // nothing we can do here
                return;

              value(new JsObject(o, ctx));
            };
          _.Finalizer = cb;
        }
      }

      public ObjectHasPropertyCallback HasProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectHasPropertyCallback cb
            = (ctx, o, pn) => value(
              new JsObject(o, new JsLocalContext(ctx)),
              new JsString(pn)
            );
          _.HasProperty = cb;
        }
      }

      public ObjectGetPropertyCallback GetProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectGetPropertyCallback cb
            = (ctx, o, pn, exception) => {
              var r = value(
                new JsObject(o, new JsLocalContext(ctx)),
                new JsString(pn),
                out var exc
              );

              *exception = exc == null ? null : exc.Unsafe;
              return r == null ? null : r.Unsafe;
            };
          _.GetProperty = cb;
        }
      }

      public ObjectSetPropertyCallback SetProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectSetPropertyCallback cb
            = (ctx, o, pn, v, exception) => {
              var localCtx = new JsLocalContext(ctx);
              var r = value(
                new JsObject(o, localCtx),
                new JsString(pn),
                JsValueLike.Create(v, localCtx),
                out var exc
              );

              *exception = exc == null ? null : exc.Unsafe;
              return r;
            };
          _.SetProperty = cb;
        }
      }

      public ObjectDeletePropertyCallback DeleteProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectDeletePropertyCallback cb
            = (ctx, o, pn, exception) => {
              var r = value(
                new JsObject(o, new JsLocalContext(ctx)),
                new JsString(pn),
                out var exc
              );

              *exception = exc == null ? null : exc.Unsafe;
              return r;
            };
          _.DeleteProperty = cb;
        }
      }

      public ObjectGetPropertyNamesCallback GetPropertyNames {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectGetPropertyNamesCallback cb
            = (ctx, o, pna) => {
              value(
                new JsObject(o, new JsLocalContext(ctx)),
                new JsPropertyNameAccumulator(pna)
              );
            };
          _.GetPropertyNames = cb;
        }
      }

      public ObjectCallAsFunctionCallback CallAsFunction {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectCallAsFunctionCallback cb
            = (ctx, function, thisObject, count, arguments, exception)
              => JsUtilities.SafeWrapperForObjectCallAsFunctionCallback
                (value, ctx, function, thisObject, count, arguments, exception);
          _.CallAsFunction = cb;
        }
      }

      public ObjectCallAsConstructorCallback CallAsConstructor {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectCallAsConstructorCallback cb
            = (ctx, ctor, count, arguments, exception)
              => {
              var localCtx = new JsLocalContext(ctx);
              var ctorObj = new JsObject(ctor, localCtx);
              var argCount = (int) count;
              var args = new JsValueLike?[argCount];
              for (var i = 0; i < argCount; ++i)
                args[i] = JsValueLike.Create(arguments[i], localCtx);
              var rv = value(
                ctorObj,
                args,
                out var exc
              );
              var r = rv == null ? null : rv.Unsafe;
              if (exc != null && exception != null)
                *exception = exc.Unsafe;
              if (r == null)
                r = localCtx._->MakeUndefined();
              return r;
            };
          _.CallAsConstructor = cb;
        }
      }

      public ObjectHasInstanceCallback HasInstance {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectHasInstanceCallback cb
            = (ctx, o, pi, exception) => {
              var localCtx = new JsLocalContext(ctx);
              var r = value(
                new JsObject(o, localCtx),
                JsValueLike.Create(pi, localCtx),
                out var exc
              );
              if (exc != null && exception != null)
                *exception = exc.Unsafe;
              return r;
            };
          _.HasInstance = cb;
        }
      }

      public ObjectConvertToTypeCallback ConvertToType {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectConvertToTypeCallback cb
            = (ctx, o, t, exception) => {
              var localCtx = new JsLocalContext(ctx);
              var rv = value(
                new JsObject(o, localCtx),
                t,
                out var exc
              );
              var r = rv == null ? null : rv.Unsafe;
              if (exc != null && exception != null)
                *exception = exc.Unsafe;
              if (r == null)
                r = localCtx._->MakeUndefined();
              return r;
            };
          _.ConvertToType = cb;
        }
      }

    }

  }

}