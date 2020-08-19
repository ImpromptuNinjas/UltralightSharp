using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULRenderBuffer")]
  [StructLayout(LayoutKind.Sequential)]
  public struct RenderBuffer {

    [NativeTypeName("unsigned int")]
    public uint TextureId;

    [NativeTypeName("unsigned int")]
    public uint Width;

    [NativeTypeName("unsigned int")]
    public uint Height;

    public OneByteBoolean HasStencilBuffer;

    public OneByteBoolean HasDepthBuffer;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct RenderBuffer {

      [NativeTypeName("unsigned int")]
      public uint TextureId;

      [NativeTypeName("unsigned int")]
      public uint Width;

      [NativeTypeName("unsigned int")]
      public uint Height;

      public OneByteBoolean HasStencilBuffer;

      public OneByteBoolean HasDepthBuffer;

      [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator UltralightSharp.RenderBuffer(in RenderBuffer b)
        => b.AsUnsafe();

      [Obsolete("This performs a copy operation. Use AsSafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator RenderBuffer(in UltralightSharp.RenderBuffer b)
        => b.AsSafe();

    }

  }

}