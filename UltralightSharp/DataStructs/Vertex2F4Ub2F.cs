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

      public override unsafe IntPtr Pointer => (IntPtr) Unsafe.AsPointer(ref _);

      private UltralightSharp.Vertex2F4Ub2F _;

      public ref Vector2 Pos {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return ref _.Pos;
        }
      }

      public ref uint Color {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return ref _.Color;
        }
      }

      public ref Vector2 Obj {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return ref _.Obj;
        }
      }

    }

  }

}