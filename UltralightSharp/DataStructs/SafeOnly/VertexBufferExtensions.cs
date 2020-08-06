using System.Runtime.CompilerServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public static class VertexBufferExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly UltralightSharp.VertexBuffer AsUnsafe(in this VertexBuffer b)
      => ref Unsafe.As<VertexBuffer, UltralightSharp.VertexBuffer>(ref Unsafe.AsRef(b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly VertexBuffer AsSafe(in this UltralightSharp.VertexBuffer b)
      => ref Unsafe.As<UltralightSharp.VertexBuffer, VertexBuffer>(ref Unsafe.AsRef(b));

  }

}