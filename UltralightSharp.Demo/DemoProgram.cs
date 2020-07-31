using System;
using System.IO;
using Ultralight;
using String = System.String;

namespace UltralightSharp.Demo {

  public static class DemoProgram {

    public static unsafe void Main(string[] args) {
      var asmPath = new Uri(typeof(DemoProgram).Assembly.CodeBase!).LocalPath;
      var asmDir = Path.GetDirectoryName(asmPath)!;
      var tempDir = Path.GetTempPath();
      string storagePath;
      do {
        storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      } while (Directory.Exists(storagePath) || File.Exists(storagePath));
      var cachePath = Ultralight.String.Create(Path.Combine(storagePath, "Cache"));
      var resourcePath = Ultralight.String.Create(Path.Combine(asmDir, "resources"));

      var cfg = Config.Create();
      cfg->SetCachePath(cachePath);
      cfg->SetResourcePath(resourcePath);
      cfg->SetUseGpuRenderer(false);
      cfg->SetEnableImages(true);
      cfg->SetEnableJavaScript(false);
      
      var renderer = Renderer.Create(cfg);
      var sessionName = Ultralight.String.Create("Demo");
      var session = Session.Create(renderer, false, sessionName);
      /*
      var view = View.Create(renderer, 640, 480, false, session);

      view->Destroy();
      */
      session->Destroy();
      renderer->Destroy();
      cfg->Destroy();
    }

  }

}