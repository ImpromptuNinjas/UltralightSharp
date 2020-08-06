using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULVertex_2f_4ub_2f_2f_28f")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct Vertex2F4Ub2F2F28F {

    [NativeTypeName("float [2]")]
    public Vector2 Pos;

    [NativeTypeName("unsigned char [4]")]
    public uint Color;

    [NativeTypeName("float [2]")]
    public Vector2 Tex;

    [NativeTypeName("float [2]")]
    public Vector2 Obj;

    [NativeTypeName("float [4][7]")]
    public DataVectors Data;

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Vertex2F4Ub2F2F28F : SafeVertexBuffer {

      public override unsafe IntPtr Pointer => (IntPtr) Unsafe.AsPointer(ref _);

      private UltralightSharp.Vertex2F4Ub2F2F28F _;

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

      public ref Vector2 Tex {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return ref _.Tex;
        }
      }

      public ref Vector2 Obj {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return ref _.Obj;
        }
      }

      public Span<Vector4> Data {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get {
          ThrowIfDisposed();
          return _.Data;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          ThrowIfDisposed();
          _.Data = MemoryMarshal.Cast<Vector4, DataVectors>(value)[0];
        }
      }

    }

  }

}