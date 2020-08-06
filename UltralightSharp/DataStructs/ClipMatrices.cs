using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULMatrix4x4 [8]")]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct ClipMatrices : IReadOnlyList<Matrix4x4> {

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _0;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _1;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _2;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _3;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _4;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _5;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _6;

    [NativeTypeName("ULMatrix4x4")]
    internal Matrix4x4 _7;

    public ref Matrix4x4 this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => ref AsSpan()[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETFRAMEWORK || NETSTANDARD2_0
    public unsafe Span<Matrix4x4> AsSpan()=> new Span<Matrix4x4>(Unsafe.AsPointer(ref _0), 8);
#else
    public Span<Matrix4x4> AsSpan() => MemoryMarshal.CreateSpan(ref _0, 8);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Span<Matrix4x4>(ClipMatrices o) => o.AsSpan();

    Matrix4x4 IReadOnlyList<Matrix4x4>.this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => this[index];
    }

    public IEnumerator<Matrix4x4> GetEnumerator() {
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