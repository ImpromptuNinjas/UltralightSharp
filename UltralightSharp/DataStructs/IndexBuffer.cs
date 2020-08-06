using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULIndexBuffer")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public unsafe struct IndexBuffer {

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("unsigned char *")]
    public byte* Data;

  }

}