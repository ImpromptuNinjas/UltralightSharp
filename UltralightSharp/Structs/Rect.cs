using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct Rect {

    public float Left;

    public float Top;

    public float Right;

    public float Bottom;

    public bool IsEmpty()
      => Ultralight.RectIsEmpty(this);

    public static Rect Create()
      => Ultralight.RectMakeEmpty();

  }

}