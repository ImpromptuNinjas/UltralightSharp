using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  public struct GCHandle<T> : IEquatable<GCHandle>, IEquatable<T>, IEquatable<GCHandle<T>>
    where T : class {

    public GCHandle Handle;

    public GCHandle(GCHandle handle)
      => Handle = handle;

    public GCHandle(T target)
      => Handle = GCHandle.Alloc(target);

    public GCHandle(T target, GCHandleType handleType)
      => Handle = GCHandle.Alloc(target, handleType);

    public unsafe ref T? Target {
      get {
        if (!IsAllocated) throw new NullReferenceException();

        var p = Unsafe.AsPointer(ref Handle);
        return ref Unsafe.AsRef<T?>(p);
      }
    }

    public bool TryRefTarget(
#if !NETFRAMEWORK && !NETSTANDARD2_0
      [NotNullWhen(true)]
#endif
      ref T? target
    ) {
      if (!IsAllocated)
        return false;

      target = Target!;
      return true;
    }

    public void Free() => Handle.Free();

    public bool IsAllocated => Handle.IsAllocated;

    public IntPtr AddrOfPinnedObject() => Handle.AddrOfPinnedObject();

    public bool Equals(GCHandle other)
      => Handle.Equals(other);

    public bool Equals(GCHandle<T> other)
      => Handle.Equals(other.Handle);

    public bool Equals(T? other) {
      if (!IsAllocated) return false;

      ref var tgt = ref Target;
      return tgt == null
        ? other == null
        : tgt!.Equals(other);
    }

    public override int GetHashCode()
      // ReSharper disable once NonReadonlyMemberInGetHashCode
      => Handle.GetHashCode();

  }

}