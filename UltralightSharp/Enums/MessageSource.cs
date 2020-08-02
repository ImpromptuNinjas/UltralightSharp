using JetBrains.Annotations;

namespace UltralightSharp.Enums {

  [PublicAPI]
  public enum MessageSource {

    Xml = 0,

    Js,

    Network,

    ConsoleApi,

    Storage,

    AppCache,

    Rendering,

    Css,

    Security,

    ContentBlocker,

    Other,

  }

}