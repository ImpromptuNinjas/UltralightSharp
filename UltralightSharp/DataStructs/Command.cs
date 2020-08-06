using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULCommand")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct Command {

    [NativeTypeName("unsigned char")]
    public byte CommandType;

    public GpuState GpuState;

    [NativeTypeName("unsigned int")]
    public uint GeometryId;

    [NativeTypeName("unsigned int")]
    public uint IndicesCount;

    [NativeTypeName("unsigned int")]
    public uint IndicesOffset;

  }

}