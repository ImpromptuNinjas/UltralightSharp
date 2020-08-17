using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public static class IndexBufferExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly UltralightSharp.IndexBuffer AsUnsafe(in this IndexBuffer b)
      => ref Unsafe.As<IndexBuffer, UltralightSharp.IndexBuffer>(ref Unsafe.AsRef(b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly IndexBuffer AsSafe(in this UltralightSharp.IndexBuffer b)
      => ref Unsafe.As<UltralightSharp.IndexBuffer, IndexBuffer>(ref Unsafe.AsRef(b));

  }

}