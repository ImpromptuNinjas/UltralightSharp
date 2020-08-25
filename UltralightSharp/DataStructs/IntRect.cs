using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public struct IntRect {

    public int Left;

    public int Top;

    public int Right;

    public int Bottom;

    public bool IsEmpty()
      => Ultralight.IntRectIsEmpty(this);

    public static IntRect Create()
      => Ultralight.IntRectMakeEmpty();

  }

}