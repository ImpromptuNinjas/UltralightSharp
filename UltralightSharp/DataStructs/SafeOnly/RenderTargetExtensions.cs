using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public static class RenderTargetExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly UltralightSharp.RenderTarget AsUnsafe(in this RenderTarget b)
      => ref Unsafe.As<RenderTarget, UltralightSharp.RenderTarget>(ref Unsafe.AsRef(b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly RenderTarget AsSafe(in this UltralightSharp.RenderTarget b)
      => ref Unsafe.As<UltralightSharp.RenderTarget, RenderTarget>(ref Unsafe.AsRef(b));

  }

}