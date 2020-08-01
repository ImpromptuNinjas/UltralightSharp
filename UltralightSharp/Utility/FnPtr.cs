using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct FnPtr<TDelegate> where TDelegate : Delegate {

    public IntPtr Pointer;

    public FnPtr(TDelegate d)
      => Pointer = Marshal.GetFunctionPointerForDelegate(d);

    public static implicit operator IntPtr(FnPtr<TDelegate> fp)
      => fp.Pointer;

    public static implicit operator FnPtr<TDelegate>(TDelegate d)
      => new FnPtr<TDelegate>(d);

  }

}