using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public abstract class SafeVertexBuffer : IDisposable {

    internal abstract bool Owned { get; }

    public abstract unsafe void* Pointer { get; }

    internal static Dictionary<IntPtr, GCHandle> _pins
      = new Dictionary<IntPtr, GCHandle>();

    protected SafeVertexBuffer() {
    }

    internal abstract void Free();

    public void Dispose() {
      Free();
    }

  }

}