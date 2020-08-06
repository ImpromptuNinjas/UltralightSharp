using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  [PublicAPI]
  public abstract class SafeBuffer : IDisposable {

    protected internal GCHandle Pin;

    protected SafeBuffer()
      => Pin = GCHandle.Alloc(this, GCHandleType.Pinned);

    public abstract IntPtr Pointer { get; }

    public bool IsDisposed {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => Pin == default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void Dispose() {
      if (IsDisposed)
        return;

      Pin.Free();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal void ThrowIfDisposed() {
      if (IsDisposed) throw new ObjectDisposedException(GetType().Name);
    }

  }

}