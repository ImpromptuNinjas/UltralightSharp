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

  public unsafe struct JsStaticFunction {

    [NativeTypeName("const char *")]
    public sbyte* Name;

    [NativeTypeName("JSObjectCallAsFunctionCallback")]
    public FnPtr<ObjectCallAsFunctionCallback> CallAsFunction;

    [NativeTypeName("JSPropertyAttributes")]
    public JsPropertyAttribute Attributes;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    public unsafe struct JsStaticFunction {

      private UltralightSharp.JsStaticFunction _;

      public static implicit operator UltralightSharp.JsStaticFunction(in JsStaticFunction x)
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

    }

  }

}