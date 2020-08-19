using System;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp.Enums {

  [PublicAPI]
  [Flags]
  public enum JsClassAttribute : uint {

    None = 0,

    NoAutomaticPrototype = 1 << 1,

  }

}