using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public struct RenderTarget {

    public bool IsEmpty;

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