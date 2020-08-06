using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("float [4][7]")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DataVectors : IReadOnlyList<Vector4> {

    internal Vector4 _0;

    internal Vector4 _1;

    internal Vector4 _2;

    internal Vector4 _3;

    internal Vector4 _4;

    internal Vector4 _5;

    internal Vector4 _6;

    public ref Vector4 this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => ref AsSpan()[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETFRAMEWORK || NETSTANDARD2_0
    public unsafe Span<Vector4> AsSpan() => new Span<Vector4>(Unsafe.AsPointer(ref _0), 7);
#else
    public Span<Vector4> AsSpan() => MemoryMarshal.CreateSpan(ref _0, 7);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Span<Vector4>(DataVectors o) => o.AsSpan();

    Vector4 IReadOnlyList<Vector4>.this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => this[index];
    }

    public IEnumerator<Vector4> GetEnumerator() {
      for (var i = 0; i < 7; ++i)
        yield return this[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => 7;
    }

  }

}