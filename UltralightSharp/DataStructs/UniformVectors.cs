using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULvec4 [8]")]
  [StructLayout(LayoutKind.Sequential)]
  public struct UniformVectors : IReadOnlyList<Vector4> {

    [NativeTypeName("ULvec4")]
    internal Vector4 _0;

    [NativeTypeName("ULvec4")]
    internal Vector4 _1;

    [NativeTypeName("ULvec4")]
    internal Vector4 _2;

    [NativeTypeName("ULvec4")]
    internal Vector4 _3;

    [NativeTypeName("ULvec4")]
    internal Vector4 _4;

    [NativeTypeName("ULvec4")]
    internal Vector4 _5;

    [NativeTypeName("ULvec4")]
    internal Vector4 _6;

    [NativeTypeName("ULvec4")]
    internal Vector4 _7;

    public ref Vector4 this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => ref AsSpan()[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETFRAMEWORK || NETSTANDARD2_0
    public unsafe Span<Vector4> AsSpan() => new Span<Vector4>(Unsafe.AsPointer(ref _0), 8);
#else
    public Span<Vector4> AsSpan() => MemoryMarshal.CreateSpan(ref _0, 8);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETFRAMEWORK || NETSTANDARD2_0
    public unsafe Span<float> AsFloatSpan() => new Span<float>(Unsafe.AsPointer(ref _0), 8*4);
#else
    public Span<float> AsFloatSpan() => MemoryMarshal.CreateSpan(ref _0.W, 8 * 4);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Span<Vector4>(UniformVectors o) => o.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Span<float>(UniformVectors o) => o.AsFloatSpan();

    Vector4 IReadOnlyList<Vector4>.this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => this[index];
    }

    public IEnumerator<Vector4> GetEnumerator() {
      for (var i = 0; i < 8; ++i)
        yield return this[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => 8;
    }

  }

}