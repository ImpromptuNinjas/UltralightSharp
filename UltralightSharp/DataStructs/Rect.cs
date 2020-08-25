using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public struct Rect {

    public float Left;

    public float Top;

    public float Right;

    public float Bottom;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
      => Ultralight.RectIsEmpty(this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rect Create()
      => Ultralight.RectMakeEmpty();

  }

}