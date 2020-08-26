using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using SixLabors.ImageSharp.PixelFormats;
using ImpromptuNinjas.UltralightSharp.Enums;

namespace ImpromptuNinjas.UltralightSharp.Demo {

  public static partial class DemoProgram {

    public static void Main(string[] args) {
      var isNotInteractive = !Environment.UserInteractive || Console.IsInputRedirected;

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
      do
        storagePath = Path.Combine(tempDir, Guid.NewGuid().ToString());
      while (Directory.Exists(storagePath) || File.Exists(storagePath));

      {
        using var cfg = new Safe.Config();

        var cachePath = Path.Combine(storagePath, "Cache");
        cfg.SetCachePath(cachePath);

        var resourcePath = Path.Combine(asmDir, "resources");
        cfg.SetResourcePath(resourcePath);

        cfg.SetUseGpuRenderer(false);
        cfg.SetEnableImages(true);
        cfg.SetEnableJavaScript(true);

        Safe.AppCore.EnablePlatformFontLoader();

        {
          var assetsPath = Path.Combine(asmDir, "assets");
          Safe.AppCore.EnablePlatformFileSystem(assetsPath);
        }

        using var renderer = new Safe.Renderer(cfg);
        var sessionName = "Demo";
        using var session = new Safe.Session(renderer, false, sessionName);

        using var view = new Safe.View(renderer, 640, 480, false, session);

        //var htmlString = "<i>Loading...</i>";
        //Console.WriteLine($"Loading HTML: {htmlString}");
        //view.LoadHtml(htmlString);
        view.LoadUrl("file:///index.html");

        var nextStep = false;

        view.SetFinishLoadingCallback((data, caller, frameId, isMainFrame, url) => {
          //Console.WriteLine($"Loading Finished, URL: {url}");

          nextStep = true;
        }, default);

        if (isNotInteractive)
          do {
            renderer.Update();
            renderer.Render();
          } while (nextStep == false);

        const double wm = 4;
        const double hm = wm * 2;

        GetConsoleSize(out var cw, out var ch);
        view.Resize((uint) (cw * wm), (uint) (ch * hm));

        var ditherIndex = 0;
        var customDither = Dithers[ditherIndex];

        var ansiColors = (AnsiColors) 0;

        var step = 0;
        var escInstrBytes = Encoding.UTF8.GetBytes("Press C to change color mode, D to change dither mode, ESC to exit.");
        var key = new ConsoleKeyInfo();
        //Console.TreatControlCAsInput = true;

        using var stdOut = Console.OpenStandardOutput(256);
        using var o = new BufferedStream(stdOut, 8 * 1024 * 1024);

        byte[]? urlStrBytes = null;
        // alt buffer
        if (!isNotInteractive) {
          o.WriteByte(0x1B);
          o.WriteByte((byte) '[');
          o.WriteByte((byte) '1');
          o.WriteByte((byte) '0');
          o.WriteByte((byte) '4');
          o.WriteByte((byte) '9');
          o.WriteByte((byte) 'h');
          o.Flush();
        }

        try { Console.CursorVisible = false; }
        catch {
          /* ok */
        }

        try { Console.Clear(); }
        catch {
          /* ok */
        }

        try {
          Console.SetCursorPosition(0, 0);
        }
        catch {
          o.WriteByte(0x1B);
          o.WriteByte((byte) '7');
        }

        do {
          GetConsoleSize(out var newCw, out var newCh);
          if (cw != newCw || ch != newCh) {
            cw = newCw;
            ch = newCh;

            try { Console.Clear(); }
            catch {
              /* ok */
            }

            view.Resize((uint) (cw * wm), (uint) (ch * hm));
          }

          try {
            Console.SetCursorPosition(0, 0);
          }
          catch {
            o.WriteByte(0x1B);
            o.WriteByte((byte) '8');
          }

          var urlStr = view.GetUrl();
          var urlStrSize = Encoding.UTF8.GetByteCount(urlStr ?? "");
          if (urlStrBytes == null || urlStrBytes.Length < urlStrSize)
            urlStrBytes = new byte[urlStrSize];
          var urlStrBytesLen = Encoding.UTF8.GetBytes(urlStr, urlStrBytes);
          o.Write(urlStrBytes, 0, urlStrBytesLen);
          o.WriteByte((byte) '\n');

          renderer.Update();
          renderer.Render();

          var surface = view.GetSurface();
          var bitmap = surface.GetBitmap();
          var pixels = bitmap.LockPixels();
          RenderAnsi<Bgra32>(o, pixels,
            bitmap.GetWidth(), bitmap.GetHeight(),
            2,
            borderless: true,
            colors: ansiColors, // isNotInteractive
            customDither: customDither
          );
          bitmap.UnlockPixels();

          if (isNotInteractive)
            return;

          o.Write(escInstrBytes);
          o.Flush();

          if (nextStep)
            switch (step) {
              case 0: {
                view.EvaluateScript("rotateBgColor()");
                nextStep = false;
                ++step;
                // ReSharper disable once ObjectCreationAsStatement
                new Timer(state => {
                  nextStep = true;
                }, null, 10000, Timeout.Infinite);
                break;
              }
              case 1: {
                var urlString = "https://cristurm.github.io/nyan-cat/";
                view.LoadUrl(urlString);
                nextStep = false;
                ++step;
                break;
              }
              case 2:
                view.EvaluateScript(
                  "const m = document.createElement('marquee');"
                  + "const d = document.createElement('div');"
                  + "document.body.appendChild(d);"
                  + "d.appendChild(m);"
                  + "m.setAttribute('width', '100%');"
                  + "m.setAttribute('scrollamount', 4);"
                  + "m.setAttribute('scrolldelay', 1);"
                  + "m.setAttribute('truespeed', 1);"
                  + "m.innerText = 'UltralightSharp';"
                  + "m.style.cssText = '"
                  + "position: absolute;"
                  + "bottom: 0;"
                  + "left: 0;"
                  + "z-index: 999;"
                  + "padding: 4px 8px;"
                  + "font-size: 2.5em;"
                  + "font-weight: bold;"
                  + "width: 100vw;"
                  + "color: #fff"
                  + "';");
                nextStep = false;
                ++step;
                break;
            }

          if (!Console.KeyAvailable)
            continue;

          key = Console.ReadKey(true);
          Console.WriteLine();
          if (key.Key == ConsoleKey.D) {
            ditherIndex = (ditherIndex + 1) % Dithers.Length;
            customDither = Dithers[ditherIndex];
            continue;
          }

          if (key.Key == ConsoleKey.C) {
            ansiColors = (AnsiColors) (((int) ansiColors + 1) % 3);
            continue;
          }
        } while (key.Key != ConsoleKey.Escape);
      }

      try {
        Directory.Delete(storagePath, true);
      }
      catch {
        /* ok */
      }
    }

    private static void LoggerCallback(LogLevel logLevel, string? msg) {
      Debug.WriteLine($"{logLevel.ToString()}: {msg}");
    }

  }

}