using System;
using System.IO;
using Ultralight;
using String = System.String;
using static Ultralight.Ultralight;

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

      var cfg = Config.Create();

      {
        var cachePath = Ultralight.String.Create(Path.Combine(storagePath, "Cache"));
        cfg->SetCachePath(cachePath);
        cachePath->Destroy();
      }

      {
        var resourcePath = Ultralight.String.Create(Path.Combine(asmDir, "resources"));
        cfg->SetResourcePath(resourcePath);
        resourcePath->Destroy();
      }

      cfg->SetUseGpuRenderer(false);
      cfg->SetEnableImages(true);
      cfg->SetEnableJavaScript(false);

      AppCore.EnablePlatformFontLoader();

      {
        var assetsPath = Ultralight.String.Create(Path.Combine(asmDir, "assets"));
        AppCore.EnablePlatformFileSystem(assetsPath);
        assetsPath->Destroy();
      }

      var renderer = Renderer.Create(cfg);
      var sessionName = Ultralight.String.Create("Demo");
      var session = Session.Create(renderer, false, sessionName);

      var view = View.Create(renderer, 640, 480, false, session);

      {
        var htmlString = Ultralight.String.Create("<i>Loading...</i>");
        view->LoadHtml(htmlString);
        htmlString->Destroy();
      }

      var loaded = false;

      view->SetFinishLoadingCallback((data, caller, id, frame, url) => {
        Console.WriteLine("Our page has loaded!");
        loaded = true;
      }, null);

      while (!loaded) {
        Update(renderer);
        Render(renderer);
      }

      view->Destroy();

      session->Destroy();
      renderer->Destroy();
      cfg->Destroy();
    }

  }

}