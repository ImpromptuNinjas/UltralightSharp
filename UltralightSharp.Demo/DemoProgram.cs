using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using ImpromptuNinjas.UltralightSharp;
using String = ImpromptuNinjas.UltralightSharp.String;

namespace ImpromptuNinjas.UltralightSharpSharp.Demo {

  public static partial class DemoProgram {

    public static unsafe void Main(string[] args) {
      // setup logging
      LoggerLogMessageCallback cb = LoggerCallback;
      Ultralight.PlatformSetLogger(new Logger {LogMessage = cb});

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        Ansi.WindowsConsole.TryEnableVirtualTerminalProcessing();

      var asmPath = new Uri(typeof(DemoProgram).Assembly.CodeBase!).LocalPath;
      var asmDir = Path.GetDirectoryName(asmPath)!;
      var tempDir = Path.GetTempPath();
      // find a place to stash instance storage
      string storagePath;
      do {
        storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      } while (Directory.Exists(storagePath) || File.Exists(storagePath));

      var cfg = Config.Create();

      {
        var cachePath = String.Create(Path.Combine(storagePath, "Cache"));
        cfg->SetCachePath(cachePath);
        cachePath->Destroy();
      }

      {
        var resourcePath = String.Create(Path.Combine(asmDir, "resources"));
        cfg->SetResourcePath(resourcePath);
        resourcePath->Destroy();
      }

      cfg->SetUseGpuRenderer(false);
      cfg->SetEnableImages(true);
      cfg->SetEnableJavaScript(false);

      AppCore.EnablePlatformFontLoader();

      {
        var assetsPath = String.Create(Path.Combine(asmDir, "assets"));
        AppCore.EnablePlatformFileSystem(assetsPath);
        assetsPath->Destroy();
      }

      var renderer = Renderer.Create(cfg);
      var sessionName = String.Create("Demo");
      var session = Session.Create(renderer, false, sessionName);

      var view = View.Create(renderer, 640, 480, false, session);

      {
        var htmlString = String.Create("<i>Loading...</i>");
        Console.WriteLine($"Loading HTML: {htmlString->Read()}");
        view->LoadHtml(htmlString);
        htmlString->Destroy();
      }

      var loaded = false;

      view->SetFinishLoadingCallback((data, caller, id, frame, url) => {
        Console.WriteLine($"Loading Finished, URL: 0x{(ulong) url:X8}  {url->Read()}");

        loaded = true;
      }, null);

      while (!loaded) {
        Ultralight.Update(renderer);
        Ultralight.Render(renderer);
      }

      /*
      {
        var surface = view->GetSurface();
        var bitmap = surface->GetBitmap();
        var pixels = bitmap->LockPixels();
        RenderAnsi<Bgra32>(pixels, bitmap->GetWidth(), bitmap->GetHeight(), bitmap->GetBpp());
        Console.WriteLine();
        bitmap->UnlockPixels();
        bitmap->SwapRedBlueChannels();
        //bitmap->WritePng("Loading.png");
      }
      */

      loaded = false;

      {
        var urlString = String.Create("file:///index.html");
        Console.WriteLine($"Loading URL: {urlString->Read()}");
        view->LoadUrl(urlString);
        urlString->Destroy();
      }

      while (!loaded) {
        Ultralight.Update(renderer);
        Ultralight.Render(renderer);
      }

      {
        var urlStrPtr = view->GetUrl();
        Console.WriteLine($"After Loaded View GetURL: 0x{(ulong) urlStrPtr:X8} {urlStrPtr->Read()}");
      }

      {
        var surface = view->GetSurface();
        var bitmap = surface->GetBitmap();
        var pixels = bitmap->LockPixels();
        RenderAnsi24BitColor<Bgra32>(pixels, bitmap->GetWidth(), bitmap->GetHeight(), bitmap->GetBpp());
        bitmap->UnlockPixels();
        bitmap->SwapRedBlueChannels();
        //bitmap->WritePng("Loaded.png");
      }

      view->Destroy();

      session->Destroy();
      renderer->Destroy();
      cfg->Destroy();

      try {
        Directory.Delete(storagePath, true);
      }
      catch {
        /* ok */
      }

      //Console.WriteLine("Press any key to exit.");
      //Console.ReadKey(true);
    }

    private static unsafe void LoggerCallback(LogLevel logLevel, String* msg)
      => Console.WriteLine($"{logLevel.ToString()}: {msg->Read()}");

  }

}