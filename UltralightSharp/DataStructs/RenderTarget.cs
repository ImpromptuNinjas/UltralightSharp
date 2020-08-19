using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using ImpromptuNinjas.UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  public struct RenderTarget {

    public OneByteBoolean IsEmpty;

    [NativeTypeName("unsigned int")]
    public uint Width;

    [NativeTypeName("unsigned int")]
    public uint Height;

    [NativeTypeName("unsigned int")]
    public uint TextureId;

    [NativeTypeName("unsigned int")]
    public uint TextureWidth;

    [NativeTypeName("unsigned int")]
    public uint TextureHeight;

    public BitmapFormat TextureFormat;

    public Rect UvCoords;

    [NativeTypeName("unsigned int")]
    public uint RenderBufferId;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct RenderTarget {

      public OneByteBoolean IsEmpty;

      [NativeTypeName("unsigned int")]
      public uint Width;

      [NativeTypeName("unsigned int")]
      public uint Height;

      [NativeTypeName("unsigned int")]
      public uint TextureId;

      [NativeTypeName("unsigned int")]
      public uint TextureWidth;

      [NativeTypeName("unsigned int")]
      public uint TextureHeight;

      public BitmapFormat TextureFormat;

      public Rect UvCoords;

      [NativeTypeName("unsigned int")]
      public uint RenderBufferId;

      [Obsolete("This performs a copy operation. Use AsUnsafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator UltralightSharp.RenderTarget(in RenderTarget b)
        => b.AsUnsafe();

      [Obsolete("This performs a copy operation. Use AsSafe instead.")]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static explicit operator RenderTarget(in UltralightSharp.RenderTarget b)
        => b.AsSafe();

    }

  }

}