using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct FnPtr<TDelegate> where TDelegate : Delegate {

    public IntPtr Pointer;

    private static Dictionary<IntPtr, TDelegate> _gcRefs
      = new Dictionary<IntPtr, TDelegate>();

    public FnPtr(TDelegate d) {
      Pointer = Marshal.GetFunctionPointerForDelegate(d);
      _gcRefs.Add(Pointer, d);
    }

    public void Free()
      => _gcRefs.Remove(Pointer);

    public static implicit operator IntPtr(FnPtr<TDelegate> fp)
      => fp.Pointer;

    public static implicit operator FnPtr<TDelegate>(TDelegate d)
      => new FnPtr<TDelegate>(d);

  }

}