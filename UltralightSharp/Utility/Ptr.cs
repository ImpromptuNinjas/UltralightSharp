using System;
using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp {

  public readonly unsafe struct Ptr<T> where T : unmanaged {

    public readonly T* Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ptr(T* p) => Value = p;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IntPtr(Ptr<T> p)
      => (IntPtr) p.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UIntPtr(Ptr<T> p)
      => (UIntPtr) p.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T*(Ptr<T> p)
      => p.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Ptr<T>(T* p)
      => new Ptr<T>(p);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Dereference() => *Value;

  }

}