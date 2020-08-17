using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public static class RenderBufferExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly UltralightSharp.RenderBuffer AsUnsafe(in this RenderBuffer b)
      => ref Unsafe.As<RenderBuffer, UltralightSharp.RenderBuffer>(ref Unsafe.AsRef(b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly RenderBuffer AsSafe(in this UltralightSharp.RenderBuffer b)
      => ref Unsafe.As<UltralightSharp.RenderBuffer, RenderBuffer>(ref Unsafe.AsRef(b));

  }

}