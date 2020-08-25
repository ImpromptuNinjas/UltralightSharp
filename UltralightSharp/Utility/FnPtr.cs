using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct FnPtr<TDelegate> where TDelegate : Delegate {

    public IntPtr Pointer;

    internal static Dictionary<IntPtr, TDelegate> GcRefs
      = new Dictionary<IntPtr, TDelegate>();

    public FnPtr(TDelegate d) {
      Pointer = Marshal.GetFunctionPointerForDelegate(d);
      GcRefs.Add(Pointer, d);
    }

    public bool Free()
      => GcRefs.Remove(Pointer);

    public static implicit operator IntPtr(FnPtr<TDelegate> fp)
      => fp.Pointer;

    public static implicit operator FnPtr<TDelegate>(TDelegate d)
      => new FnPtr<TDelegate>(d);

  }

  [PublicAPI]
  public struct FnPtr<TDelegate1, TDelegate2> where TDelegate1 : Delegate where TDelegate2 : Delegate {

    public IntPtr Pointer;

    public FnPtr(TDelegate1 d) {
      Pointer = Marshal.GetFunctionPointerForDelegate(d);
      FnPtr<TDelegate1>.GcRefs.Add(Pointer, d);
    }

    public FnPtr(TDelegate2 d) {
      Pointer = Marshal.GetFunctionPointerForDelegate(d);
      FnPtr<TDelegate2>.GcRefs.Add(Pointer, d);
    }

    public bool Free()
      => FnPtr<TDelegate1>.GcRefs.Remove(Pointer)
        || FnPtr<TDelegate2>.GcRefs.Remove(Pointer);

    public Type WhichDelegateType() {
      if (FnPtr<TDelegate1>.GcRefs.ContainsKey(Pointer)) return typeof(TDelegate1);
      if (FnPtr<TDelegate2>.GcRefs.ContainsKey(Pointer)) return typeof(TDelegate2);

      throw new KeyNotFoundException("Unable to identify delegate type.");
    }

    public static implicit operator IntPtr(FnPtr<TDelegate1, TDelegate2> fp)
      => fp.Pointer;

    public static implicit operator FnPtr<TDelegate1, TDelegate2>(TDelegate1 d)
      => new FnPtr<TDelegate1, TDelegate2>(d);

    public static implicit operator FnPtr<TDelegate1, TDelegate2>(TDelegate2 d)
      => new FnPtr<TDelegate1, TDelegate2>(d);

  }

}