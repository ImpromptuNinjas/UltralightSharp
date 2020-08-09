using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImpromptuNinjas.UltralightSharp {

  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public readonly struct OneByteBoolean : IEquatable<OneByteBoolean>, IEquatable<bool> {

    private readonly byte _value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
      => obj is OneByteBoolean other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
      => ((OneByteBoolean)(_value != 0))._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(OneByteBoolean left, OneByteBoolean right)
      => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(OneByteBoolean left, OneByteBoolean right)
      => !left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator bool(OneByteBoolean o)
      => o._value != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator OneByteBoolean(bool b)
      => Unsafe.As<bool, OneByteBoolean>(ref b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(OneByteBoolean other)
      => _value != 0 == (other._value != 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(bool other)
      => _value != 0 == other;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
      => (_value != 0).ToString();

  }

}