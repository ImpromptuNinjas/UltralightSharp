using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULRenderBuffer")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct RenderBuffer {

    [NativeTypeName("unsigned int")]
    public uint TextureId;

    [NativeTypeName("unsigned int")]
    public uint Width;

    [NativeTypeName("unsigned int")]
    public uint Height;

    public bool HasStencilBuffer;

    public bool HasDepthBuffer;

  }

}