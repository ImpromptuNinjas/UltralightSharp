using System;
using Ultralight;
using String = System.String;

namespace UltralightSharp.Demo {

  public static class DemoProgram {

    public static unsafe void Main(string[] args) {
      var cfg = Config.Create();
      var renderer = Renderer.Create(cfg);
      var sessionName = Ultralight.String.Create("Demo");
      var session = Session.Create(renderer, false, sessionName);
      var view = View.Create(renderer, 640, 480, false, session);

      view->Destroy();
      session->Destroy();
      renderer->Destroy();
      cfg->Destroy();
    }

  }

}