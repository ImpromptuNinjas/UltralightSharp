using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

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