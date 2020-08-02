using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Safe;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharpSharp.Demo {

  public static partial class DemoProgram {

    public static void Main(string[] args) {
      // setup logging
      LoggerLogMessageCallback cb = LoggerCallback;
      Ultralight.PlatformSetLogger(new Logger {LogMessage = cb});

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
        using var cfg = new Config();

        var cachePath = Path.Combine(storagePath, "Cache");
        cfg.SetCachePath(cachePath);

        var resourcePath = Path.Combine(asmDir, "resources");
        cfg.SetResourcePath(resourcePath);

        cfg.SetUseGpuRenderer(false);
        cfg.SetEnableImages(true);
        cfg.SetEnableJavaScript(false);

        AppCore.EnablePlatformFontLoader();

        {
          var assetsPath = Path.Combine(asmDir, "assets");
          AppCore.EnablePlatformFileSystem(assetsPath);
        }

        using var renderer = new Renderer(cfg);
        var sessionName = "Demo";
        using var session = new Session(renderer, false, sessionName);

        using var view = new View(renderer, 640, 480, false, session);

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
          RenderAnsi24BitColor<Bgra32>(pixels, bitmap.GetWidth(), bitmap.GetHeight(), bitmap.GetBpp());
          bitmap.UnlockPixels();
          bitmap.SwapRedBlueChannels();
          //bitmap.WritePng("Loaded.png");
        }
      }

      try {
        Directory.Delete(storagePath, true);
      }
      catch {
        /* ok */
      }

      //Console.WriteLine("Press any key to exit.");
      //Console.ReadKey(true);
    }

    private static void LoggerCallback(LogLevel logLevel, string? msg)
      => Console.WriteLine($"{logLevel.ToString()}: {msg}");

  }

}