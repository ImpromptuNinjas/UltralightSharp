using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;
#if NETFRAMEWORK || NETSTANDARD2_0
using System.Buffers;

#endif

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public unsafe partial struct JsStaticValue {

    [NativeTypeName("const char *")]
    public sbyte* Name;

    [NativeTypeName("JSObjectGetPropertyCallback")]
    public FnPtr<ObjectGetPropertyCallback> GetProperty;

    [NativeTypeName("JSObjectSetPropertyCallback")]
    public FnPtr<ObjectSetPropertyCallback> SetProperty;

    [NativeTypeName("JSPropertyAttributes")]
    public JsPropertyAttribute Attributes;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct JsStaticValue {

      private UltralightSharp.JsStaticValue _;

      public static implicit operator UltralightSharp.JsStaticValue(in JsStaticValue x)
        => x._;

      [MustUseReturnValue]
      public GCHandle SetName(string name) {
        var bytes = Encoding.UTF8.GetBytes(name);
        var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        var ptr = Unsafe.AsPointer(ref bytes[0]);
        _.Name = (sbyte*) ptr;
        return pin;
      }

      public string Name {
        get {
          var len = new ReadOnlySpan<byte>(_.Name, int.MaxValue).IndexOf<byte>(0);
          if (len == -1)
            throw new InvalidOperationException("String length is too long (>2GB) or indeterminate.");

#if NETFRAMEWORK || NETSTANDARD2_0
          var buf = ArrayPool<byte>.Shared.Rent(len);
          fixed (byte* pBuf = buf)
            Unsafe.CopyBlockUnaligned(pBuf, _.Name, (uint) len);
          return Encoding.UTF8.GetString(buf);
#else
          var span = new ReadOnlySpan<byte>(_.Name, len);
          return Encoding.UTF8.GetString(span);
#endif
        }
        [Obsolete("Use the SetName method instead.", true)]
        set => throw new NotSupportedException("Use the SetName method instead.");
      }

      public JsPropertyAttribute Attributes {
        get => _.Attributes;
        set => _.Attributes = value;
      }

      public ObjectGetPropertyCallback GetProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectGetPropertyCallback cb
            = (ctx, @object, propertyName, exception) => {
              var localCtx = new JsLocalContext(ctx);
              var jsObj = new JsObject(@object, localCtx);
              var propName = new JsString(propertyName);
              var rv = value(
                jsObj,
                propName,
                out var exc
              );
              var r = rv == null ? null : rv.Unsafe;
              if (exc != null && exception != null)
                *exception = exc.Unsafe;
              return r;
            };
          _.GetProperty = cb;
        }
      }

      public ObjectSetPropertyCallback SetProperty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.ObjectSetPropertyCallback cb
            = (ctx, @object, propertyName, unsafeValue, exception) => {
              var localCtx = new JsLocalContext(ctx);
              var jsObj = new JsObject(@object, localCtx);
              var propName = new JsString(propertyName);
              var safeValue = JsValueLike.Create(unsafeValue, localCtx);
              var r = value(
                jsObj,
                propName,
                safeValue,
                out var exc
              );
              if (exc != null && exception != null)
                *exception = exc.Unsafe;
              return r;
            };
          _.SetProperty = cb;
        }
      }

    }

  }

}