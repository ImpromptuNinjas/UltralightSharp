using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULVertexBuffer")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public unsafe struct VertexBuffer {

    public VertexBufferFormat Format;

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("unsigned char *")]
    public byte* Data;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct VertexBuffer {

      public VertexBufferFormat Format;

      public uint Size;

      private byte* _Data;

      public SafeVertexBuffer? Data {
        get {
          if (_Data == null)
            return null;

          var buf = SafeVertexBuffer.Lookup((IntPtr) _Data);
          if (buf == null)
            throw new ObjectDisposedException(nameof(SafeVertexBuffer), "The SafeVertexBuffer was previously disposed, or this is not a SafeVertexBuffer.");

          buf.ThrowIfDisposed();
          return buf;
        }
        set {
          if (value == null) {
            _Data = null;
            return;
          }

          value.ThrowIfDisposed();
          var pointer = (byte*) value.Pointer;
          _Data = pointer;
        }
      }

      [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator UltralightSharp.VertexBuffer(in VertexBuffer b) => b.AsUnsafe();

      [Obsolete("This performs a copy operation. Use AsSafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator VertexBuffer(in UltralightSharp.VertexBuffer b) => b.AsSafe();

    }

  }

}