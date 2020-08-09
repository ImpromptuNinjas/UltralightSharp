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

}