using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULIndexBuffer")]
  [StructLayout(LayoutKind.Sequential)]
  public unsafe struct IndexBuffer {

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("unsigned char *")]
    public byte* Data;

    [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator IndexBuffer(in Safe.IndexBuffer b)
      => Safe.IndexBufferExtensions.AsUnsafe(b);

    [Obsolete("This performs a copy operation. Use AsSafe instead.")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Safe.IndexBuffer(in IndexBuffer b)
      => Safe.IndexBufferExtensions.AsSafe(b);

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IndexBuffer {

      public uint Size;

      private void* _Data;

      public Span<float> Data {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new Span<float>(_Data, (int) (Size / sizeof(float)));
      }

      [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator UltralightSharp.IndexBuffer(in IndexBuffer b)
        => b.AsUnsafe();

      [Obsolete("This performs a copy operation. Use AsSafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator IndexBuffer(in UltralightSharp.IndexBuffer b)
        => b.AsSafe();

    }

  }

}