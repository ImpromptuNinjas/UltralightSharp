using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("float [8]")]
  [StructLayout(LayoutKind.Sequential)]
  public struct UniformScalars : IReadOnlyList<float> {

    internal float _0;

    internal float _1;

    internal float _2;

    internal float _3;

    internal float _4;

    internal float _5;

    internal float _6;

    internal float _7;

    public ref float this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => ref AsSpan()[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETFRAMEWORK || NETSTANDARD2_0
    public unsafe Span<float> AsSpan() => new Span<float>(Unsafe.AsPointer(ref _0), 8);
#else
    public Span<float> AsSpan() => MemoryMarshal.CreateSpan(ref _0, 8);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Span<float>(UniformScalars o) => o.AsSpan();

    float IReadOnlyList<float>.this[int index] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => this[index];
    }

    public IEnumerator<float> GetEnumerator() {
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