using System.Numerics;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULGPUState")]
  [StructLayout(LayoutKind.Sequential)]
  public struct GpuState {
    [NativeTypeName("unsigned int")]
    public uint ViewportWidth; // 4
    [NativeTypeName("unsigned int")]
    public uint ViewportHeight; // 4
    public Matrix4x4 Transform; // 64
    public OneByteBoolean EnableTexturing; // 1
    public OneByteBoolean EnableBlend; // 1
    [NativeTypeName("unsigned char")]
    public ShaderType ShaderType; // 1
    [NativeTypeName("unsigned int")]
    public uint RenderBufferId; // 4
    [NativeTypeName("unsigned int")]
    public uint Texture1Id; // 4
    [NativeTypeName("unsigned int")]
    public uint Texture2Id; // 4
    [NativeTypeName("unsigned int")]
    public uint Texture3Id; // 4
    [NativeTypeName("float [8]")]
    public UniformScalars UniformScalars; // 32
    [NativeTypeName("ULvec4 [8]")]
    public UniformVectors UniformVectors; // 128
    [NativeTypeName("unsigned int")]
    public byte ClipSize; // 1
    [NativeTypeName("ULMatrix4x4 [8]")]
    public ClipMatrices Clip; // 512
    public OneByteBoolean EnableScissor; // 1
    public IntRect ScissorRect; // 16
  }

}