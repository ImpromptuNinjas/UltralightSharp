using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp.Enums;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULCommand")]
  [StructLayout(LayoutKind.Sequential)]
  public struct Command {

    [NativeTypeName("unsigned char")]
    public CommandType CommandType;

    public GpuState GpuState;

    [NativeTypeName("unsigned int")]
    public uint GeometryId;

    [NativeTypeName("unsigned int")]
    public uint IndicesCount;

    [NativeTypeName("unsigned int")]
    public uint IndicesOffset;

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public struct Command {

      public CommandType CommandType;

      public GpuState GpuState;

      public uint GeometryId;

      public uint IndicesCount;

      public uint IndicesOffset;

      public static implicit operator UltralightSharp.Command(Command l)
        => Unsafe.As<Command, UltralightSharp.Command>(ref l);

      public static implicit operator Command(UltralightSharp.Command l)
        => Unsafe.As<UltralightSharp.Command, Command>(ref l);

    }

  }

}