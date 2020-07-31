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
        Console.WriteLine($"Loading HTML: {htmlString->Read()}");
        view->LoadHtml(htmlString);
        htmlString->Destroy();
      }

      var loaded = false;

      view->SetFinishLoadingCallback((data, caller, id, frame, url) => {
        {
          Console.WriteLine($"Callback URL Parameter: 0x{(ulong) url:X8}  {url->Read()}");
        }

        {
          var urlStrPtr = caller->GetUrl();
          Console.WriteLine($"Callback View Parameter GetURL: 0x{(ulong) urlStrPtr:X8} {urlStrPtr->Read()}");
        }
        {
          var urlStrPtr = view->GetUrl();
          Console.WriteLine($"View GetURL from Callback: 0x{(ulong) urlStrPtr:X8} {urlStrPtr->Read()}");
        }
        loaded = true;
      }, null);

      while (!loaded) {
        Update(renderer);
        Render(renderer);
      }

      {
        var urlStrPtr = view->GetUrl();
        Console.WriteLine($"After Loaded View GetURL: 0x{(ulong) urlStrPtr:X8} {urlStrPtr->Read()}");
      }

      {
        var surface = view->GetSurface();
        var bitmap = surface->GetBitmap();
        bitmap->SwapRedBlueChannels();
        bitmap->WritePng("1.png");
      }

      loaded = false;

      {
        var htmlString = Ultralight.String.Create("file:///index.html");
        Console.WriteLine($"Loading URL: {htmlString->Read()}");
        view->LoadUrl(htmlString);
        htmlString->Destroy();
      }

      while (!loaded) {
        Update(renderer);
        Render(renderer);
      }

      {
        var urlStrPtr = view->GetUrl();
        Console.WriteLine($"After Loaded View GetURL: 0x{(ulong) urlStrPtr:X8} {urlStrPtr->Read()}");
      }

      {
        var surface = view->GetSurface();
        var bitmap = surface->GetBitmap();
        bitmap->SwapRedBlueChannels();
        bitmap->WritePng("2.png");
      }

      view->Destroy();

      session->Destroy();
      renderer->Destroy();
      cfg->Destroy();
    }

  }

}