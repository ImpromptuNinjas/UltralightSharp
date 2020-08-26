using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Dithering;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace ImpromptuNinjas.UltralightSharp.Demo {

  public static partial class DemoProgram {

    public static void GetConsoleSize(out int width, out int height) {
      width = 0;
      try { width = Console.WindowWidth; }
      catch { width = 72; }

      height = 0;
      try { height = Console.WindowHeight; }
      catch { height = 25; }
    }

    public static unsafe int RenderAnsi<TColor>(Stream o, IntPtr pixels,
      uint w, uint h,
      uint reduceLineCount = 0, int maxLineCount = -1, int maxWidth = -1,
      bool borderless = false, bool palette256 = false
    ) where TColor : unmanaged, IPixel<TColor> {
      var aspect = w / (double) h;

      GetConsoleSize(out var cw, out var ch);

      if (maxWidth >= 0)
        cw = maxWidth;

      if (maxLineCount >= 0)
        ch = maxLineCount;

      cw -= 1;
      ch -= (int) reduceLineCount;

      if (cw == 0 || ch == 0) return 0;

      var aw = cw;
      var ah = ch * 2;

      var pPixels = (byte*) pixels;
      var span = new ReadOnlySpan<TColor>(pPixels, checked((int) (w * h)));
      using var img = Image.LoadPixelData(span, (int) w, (int) h);
      img.Mutate(x => x
        .Resize(aw, ah, LanczosResampler.Lanczos3)
        .Crop(aw, ah)
      );

      IndexedImageFrame<TColor>? img256 = null;
      if (palette256) {
        img256 = AnsiPalette256
          .CreatePixelSpecificQuantizer<TColor>(Configuration.Default)
          .QuantizeFrame(img.Frames[0], new Rectangle(0, 0, img.Width, img.Height));
      }

      void WriteNumberTriplet(byte b) {
        var ones = b % 10;
        var tens = b / 10 % 10;
        var hundreds = b / 100;
        var anyHundreds = hundreds > 0;
        if (anyHundreds)
          o!.WriteByte((byte) ('0' + hundreds));
        if (anyHundreds || tens > 0)
          o!.WriteByte((byte) ('0' + tens));
        o!.WriteByte((byte) ('0' + ones));
      }

      // ╭
      void DrawTopLeftCorner() {
        o.WriteByte(0xE2);
        o.WriteByte(0x95);
        o.WriteByte(0xAD);
      }

      // ╮
      void DrawTopRightCorner() {
        o.WriteByte(0xE2);
        o.WriteByte(0x95);
        o.WriteByte(0xAE);
      }

      // ╰
      void DrawBottomLeftCorner() {
        o.WriteByte(0xE2);
        o.WriteByte(0x95);
        o.WriteByte(0xB0);
      }

      // ╯
      void DrawBottomRightCorner() {
        o.WriteByte(0xE2);
        o.WriteByte(0x95);
        o.WriteByte(0xAF);
      }

      // ─ x width
      void DrawHorizontalFrame(int width) {
        for (var i = 0; i < width; ++i) {
          o.WriteByte(0xE2);
          o.WriteByte(0x94);
          o.WriteByte(0x80);
        }
      }

      // │
      void DrawVerticalFrame() {
        o.WriteByte(0xE2);
        o.WriteByte(0x94);
        o.WriteByte(0x82);
      }

      if (!borderless) {
        DrawTopLeftCorner();
        DrawHorizontalFrame(aw + 1);
        DrawTopRightCorner();
        o.WriteByte((byte) '\n');
      }

      var lastY = ah & ~ 1;
      for (var y = 0; y < lastY; y += 2) {
        if (!borderless)
          DrawVerticalFrame();
        // write 2 lines at a time
        var haveL = y + 1 < ah;
        
        var u = !palette256
          ? img.GetPixelRowSpan(y)
          : default;
        var l = haveL && !palette256
          ? img.GetPixelRowSpan(y + 1)
          : default;
        
        var up = palette256
          ? img256!.GetPixelRowSpan(y)
          : default;
        var lp = haveL && palette256
          ? img256!.GetPixelRowSpan(y + 1)
          : default;

        for (var x = 0; x < aw; ++x) {
          // upper color
          if (palette256) {
            var upx = up[x];
            o.WriteByte(0x1B);
            o.WriteByte((byte) '[');
            o.WriteByte((byte) '3');
            o.WriteByte((byte) '8');
            o.WriteByte((byte) ';');
            o.WriteByte((byte) '5');
            o.WriteByte((byte) ';');
            WriteNumberTriplet(upx);
            o.WriteByte((byte) 'm');
          }
          else {
            Rgba32 upx = default;
            u[x].ToRgba32(ref upx);
            var ua = 255.0f / upx.A;
            o.WriteByte(0x1B);
            o.WriteByte((byte) '[');
            o.WriteByte((byte) '3');
            o.WriteByte((byte) '8');
            o.WriteByte((byte) ';');
            o.WriteByte((byte) '2');
            o.WriteByte((byte) ';');
            WriteNumberTriplet((byte) MathF.Round(upx.R * ua, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) ';');
            WriteNumberTriplet((byte) MathF.Round(upx.G * ua, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) ';');
            WriteNumberTriplet((byte) MathF.Round(upx.B * ua, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) 'm');
          }

          if (!haveL) // full block
          {
            o.WriteByte(0xE2);
            o.WriteByte(0x96);
            o.WriteByte(0x88);
          }

          else {
            // lower color
            if (palette256) {
              var lpx = lp[x];
              o.WriteByte(0x1B);
              o.WriteByte((byte) '[');
              o.WriteByte((byte) '4');
              o.WriteByte((byte) '8');
              o.WriteByte((byte) ';');
              o.WriteByte((byte) '5');
              o.WriteByte((byte) ';');
              WriteNumberTriplet(lpx);
              o.WriteByte((byte) 'm');
            }
            else {
              Rgba32 lpx = default;
              l[x].ToRgba32(ref lpx);
              var la = 255.0f / lpx.A;
              o.WriteByte(0x1B);
              o.WriteByte((byte) '[');
              o.WriteByte((byte) '4');
              o.WriteByte((byte) '8');
              o.WriteByte((byte) ';');
              o.WriteByte((byte) '2');
              o.WriteByte((byte) ';');
              WriteNumberTriplet((byte) Math.Round(lpx.R * la, MidpointRounding.AwayFromZero));
              o.WriteByte((byte) ';');
              WriteNumberTriplet((byte) Math.Round(lpx.G * la, MidpointRounding.AwayFromZero));
              o.WriteByte((byte) ';');
              WriteNumberTriplet((byte) Math.Round(lpx.B * la, MidpointRounding.AwayFromZero));
              o.WriteByte((byte) 'm');
            }

            // half block
            o.WriteByte(0xE2);
            o.WriteByte(0x96);
            o.WriteByte(0x80);
          }
        }

        o.WriteByte(0x1B);
        o.WriteByte((byte) '[');
        o.WriteByte((byte) '0');
        o.WriteByte((byte) 'm');
        o.WriteByte((byte) ' ');
        if (!borderless)
          DrawVerticalFrame();
        o.WriteByte((byte) '\n');
      }

      if (!borderless) {
        DrawBottomLeftCorner();
        DrawHorizontalFrame(aw + 1);
        DrawBottomRightCorner();
        o.WriteByte((byte) '\n');
      }

      //o.Flush();

      return lastY;
    }

  }

}