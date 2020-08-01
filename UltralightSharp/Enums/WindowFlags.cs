using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public enum WindowFlags {

    Borderless = 1 << 0,

    Titled = 1 << 1,

    Resizable = 1 << 2,

    Maximizable = 1 << 3,

  }

}