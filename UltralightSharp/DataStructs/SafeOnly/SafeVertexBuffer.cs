using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  [PublicAPI]
  public abstract class SafeVertexBuffer : SafeBuffer {

    private static ReaderWriterLockSlim _lock
      = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

    private static Dictionary<IntPtr, SafeVertexBuffer> _index
      = new Dictionary<IntPtr, SafeVertexBuffer>();

    protected SafeVertexBuffer() {
      _lock.EnterWriteLock();
      try {
        // ReSharper disable once VirtualMemberCallInConstructor
        _index.Add(Pointer, this);
      }
      finally {
        _lock.ExitWriteLock();
      }
    }

    public override void Dispose() {
      _lock.EnterWriteLock();
      try {
        _index.Remove(Pointer);
      }
      finally {
        _lock.ExitWriteLock();
      }

      base.Dispose();
    }

    public static SafeVertexBuffer? Lookup(IntPtr p) {
      _lock.EnterReadLock();
      try {
        return _index.TryGetValue(p, out var buf) ? buf : null;
      }
      finally {
        _lock.ExitReadLock();
      }
    }

  }

}