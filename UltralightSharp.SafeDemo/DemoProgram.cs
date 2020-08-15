using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Safe;
using SixLabors.ImageSharp.PixelFormats;
using ImpromptuNinjas.UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharp.Demo {

  public static partial class DemoProgram {

    public static void Main(string[] args) {
      // setup logging
      Safe.LoggerLogMessageCallback cb = LoggerCallback;
      Safe.Ultralight.SetLogger(new Safe.Logger {LogMessage = cb});

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
        Console.OutputEncoding = Encoding.UTF8;
        Ansi.WindowsConsole.TryEnableVirtualTerminalProcessing();
      }

      var asmPath = new Uri(typeof(DemoProgram).Assembly.CodeBase!).LocalPath;
      var asmDir = Path.GetDirectoryName(asmPath)!;
      var tempDir = Path.GetTempPath();
      // find a place to stash instance storage
      string storagePath;
      do {
        storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      } while (Directory.Exists(storagePath) || File.Exists(storagePath));

      {
        using var cfg = new Safe.Config();

        var cachePath = Path.Combine(storagePath, "Cache");
        cfg.SetCachePath(cachePath);

        var resourcePath = Path.Combine(asmDir, "resources");
        cfg.SetResourcePath(resourcePath);

        cfg.SetUseGpuRenderer(false);
        cfg.SetEnableImages(true);
        cfg.SetEnableJavaScript(false);

        Safe.AppCore.EnablePlatformFontLoader();

        {
          var assetsPath = Path.Combine(asmDir, "assets");
          Safe.AppCore.EnablePlatformFileSystem(assetsPath);
        }

        using var renderer = new Safe.Renderer(cfg);
        var sessionName = "Demo";
        using var session = new Safe.Session(renderer, false, sessionName);

        using var view = new Safe.View(renderer, 640, 480, false, session);

        {
          var htmlString = "<i>Loading...</i>";
          Console.WriteLine($"Loading HTML: {htmlString}");
          view.LoadHtml(htmlString);
        }

        var loaded = false;

        view.SetFinishLoadingCallback((data, caller, frameId, isMainFrame, url) => {
          Console.WriteLine($"Loading Finished, URL: {url}");

          loaded = true;
        }, default);

        while (!loaded) {
          renderer.Update();
          renderer.Render();
        }

        loaded = false;

        {
          var urlString = "file:///index.html";
          Console.WriteLine($"Loading URL: {urlString}");
          view.LoadUrl(urlString);
        }

        while (!loaded) {
          renderer.Update();
          renderer.Render();
        }

        {
          var urlStr = view.GetUrl();
          Console.WriteLine($"After Loaded View GetURL: {urlStr}");
        }

        {
          var surface = view.GetSurface();
          var bitmap = surface.GetBitmap();
          var pixels = bitmap.LockPixels();
          RenderAnsi<Bgra32>(pixels, bitmap.GetWidth(), bitmap.GetHeight(), 2, borderless: true);
          bitmap.UnlockPixels();
          //bitmap.SwapRedBlueChannels();
          //bitmap.WritePng("Loaded.png");
        }
      }

      try {
        Directory.Delete(storagePath, true);
      }
      catch {
        /* ok */
      }

      if (!Environment.UserInteractive || Console.IsInputRedirected)
        return;

      Console.Write("Press any key to exit.");
      Console.ReadKey(true);
      Console.WriteLine();
    }

    private static void LoggerCallback(LogLevel logLevel, string? msg)
      => Console.WriteLine($"{logLevel.ToString()}: {msg}");

  }

}