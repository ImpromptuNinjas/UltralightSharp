using System;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp.Enums {

  [PublicAPI]
  [Flags]
  public enum JsPropertyAttribute : uint {

    None = 0,

    ReadOnly = 1 << 1,

    DontEnum = 1 << 2,

    DontDelete = 1 << 3,

  }

}