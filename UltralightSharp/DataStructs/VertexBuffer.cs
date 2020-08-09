using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULVertexBuffer")]
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct VertexBuffer {

    public VertexBufferFormat Format;

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("unsigned char *")]
    public byte* Data;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VertexBuffer {

      public VertexBufferFormat Format;

      public uint Size;

      private void* _Data;

      public SafeVertexBuffer? Data {
        get {
          return Format switch {
            VertexBufferFormat._2F4Ub2F => Vertex2F4Ub2F.FromUnsafe(_Data),
            VertexBufferFormat._2F4Ub2F2F28F => Vertex2F4Ub2F2F28F.FromUnsafe(_Data),
            _ => throw new NotImplementedException($"Vertex Buffer Format {Format} not implemented.")
          };
        }
        set {
          if (value == null) {
            _Data = null;
            return;
          }

          switch (value) {
            case Vertex2F4Ub2F _ when Format != VertexBufferFormat._2F4Ub2F:
              throw new InvalidCastException($"Format must be {nameof(VertexBufferFormat._2F4Ub2F)}.");
            case Vertex2F4Ub2F2F28F _ when Format != VertexBufferFormat._2F4Ub2F2F28F:
              throw new InvalidCastException($"Format must be {nameof(VertexBufferFormat._2F4Ub2F2F28F)}.");
          }

          _Data = value.Pointer;
        }
      }

      public Span<byte> DataSpan => new Span<byte>(_Data, (int) Size);

      [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator UltralightSharp.VertexBuffer(in VertexBuffer b)
        => b.AsUnsafe();

      [Obsolete("This performs a copy operation. Use AsSafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator VertexBuffer(in UltralightSharp.VertexBuffer b)
        => b.AsSafe();

    }

  }

}