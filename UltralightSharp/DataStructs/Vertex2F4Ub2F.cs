using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULVertex_2f_4ub_2f")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct Vertex2F4Ub2F {

    [NativeTypeName("float [2]")]
    public Vector2 Pos;

    [NativeTypeName("unsigned char [4]")]
    public uint Color;

    [NativeTypeName("float [2]")]
    public Vector2 Obj;

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Vertex2F4Ub2F : SafeVertexBuffer {

      internal override bool Owned { get; }

      internal override unsafe void Free() {
        if (!Owned) return;

        Marshal.FreeHGlobal((IntPtr) _ptr);
      }

      private Vertex2F4Ub2F(bool owned) => Owned = owned;

      public unsafe Vertex2F4Ub2F() : this(true) {
        var size = sizeof(UltralightSharp.Vertex2F4Ub2F);
        _ptr = (UltralightSharp.Vertex2F4Ub2F*)
          Marshal.AllocHGlobal(size);
        Unsafe.InitBlockUnaligned(_ptr, 0, (uint) size);
      }

      private unsafe UltralightSharp.Vertex2F4Ub2F* _ptr;

      private unsafe ref UltralightSharp.Vertex2F4Ub2F Ref {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Unsafe.AsRef<UltralightSharp.Vertex2F4Ub2F>(_ptr);
      }

      public ref Vector2 Pos {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Ref.Pos;
      }

      public ref uint Color {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Ref.Color;
      }

      public ref Vector2 Obj {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Ref.Obj;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static unsafe Vertex2F4Ub2F FromUnsafe(void* p)
        => new Vertex2F4Ub2F(false) {_ptr = (UltralightSharp.Vertex2F4Ub2F*) p};

      public override unsafe void* Pointer {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _ptr;
      }

    }

  }

}