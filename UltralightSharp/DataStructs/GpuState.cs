using System.Numerics;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULGPUState")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct GpuState {

    [NativeTypeName("unsigned int")]
    public uint ViewportWidth;

    [NativeTypeName("unsigned int")]
    public uint ViewportHeight;

    public Matrix4x4 Transform;

    public bool EnableTexturing;

    public bool EnableBlend;

    [NativeTypeName("unsigned char")]
    public byte ShaderType;

    [NativeTypeName("unsigned int")]
    public uint RenderBufferId;

    [NativeTypeName("unsigned int")]
    public uint Texture1Id;

    [NativeTypeName("unsigned int")]
    public uint Texture2Id;

    [NativeTypeName("unsigned int")]
    public uint Texture3Id;

    [NativeTypeName("float [8]")]
    public UniformScalars UniformScalars;

    [NativeTypeName("ULvec4 [8]")]
    public UniformVectors UniformVectors;

    [NativeTypeName("unsigned int")]
    public uint ClipSize;

    [NativeTypeName("ULMatrix4x4 [8]")]
    public ClipMatrices Clip;

    public bool EnableScissor;

    public IntRect ScissorRect;

  }

}