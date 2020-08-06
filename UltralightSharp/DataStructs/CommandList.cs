using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULCommandList")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public unsafe struct CommandList {

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("ULCommand *")]
    public Command* Commands;

  }

}