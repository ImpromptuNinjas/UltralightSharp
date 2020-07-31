using JetBrains.Annotations;

namespace Ultralight {

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