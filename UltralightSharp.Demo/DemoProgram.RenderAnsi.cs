using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace ImpromptuNinjas.UltralightSharpSharp.Demo {

  public static partial class DemoProgram {


    public static unsafe void RenderAnsi<TColor>(void* pixels,
      uint w, uint h,
      uint reduceLineCount = 0, int maxLineCount = -1, int maxWidth = -1,
      bool borderless = false
    ) where TColor : unmanaged, IPixel<TColor> {
      var aspect = w / (double) h;

      // get the console size
      // @formatter:off
      var cw = 0;
      if (maxWidth < 0)
        try { cw = Console.WindowWidth; } catch { cw = 72; }
      else
        cw = maxWidth;

      var ch = 0;
      if (maxLineCount < 0)
        try { ch = Console.WindowHeight; } catch { ch = 25; }
      else
        ch = maxLineCount;
      // @formatter:on

      cw -= 1;
      ch -= (int) reduceLineCount;

      if (cw == 0 || ch == 0) return;

      // come up with an aperture that fits the console window (minus drawn borders, accounting for 2v/c)
      var borderCost = borderless ? 0 : -2;
      var wsq = borderCost + cw / aspect;
      var hsq = borderCost + (ch * 2) * aspect;
      var sq = Math.Min(wsq, hsq);

      var aw = (int) Math.Floor(sq * aspect);
      var ah = (int) Math.Floor(sq / aspect);

      var pPixels = (byte*) pixels;
      var span = new ReadOnlySpan<TColor>(pPixels, checked((int) (w * h)));
      using var img = Image.LoadPixelData(span, (int) w, (int) h);
      img.Mutate(x => x
        .Resize(aw, ah, LanczosResampler.Lanczos3)
        .Crop(aw, ah));
      using var stdOut = Console.OpenStandardOutput(256);
      using var o = new BufferedStream(stdOut, (42 * aw * (ah / 2)) + 1);

      void WriteTriplet(byte b) {
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
        var u = img.GetPixelRowSpan(y);
        var haveL = y + 1 < ah;
        var l = haveL
          ? img.GetPixelRowSpan(y + 1)
          : new Span<TColor>(default, 0);

        for (var x = 0; x < aw; ++x) {
          // upper color
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
          WriteTriplet((byte) MathF.Round(upx.R * ua, MidpointRounding.AwayFromZero));
          o.WriteByte((byte) ';');
          WriteTriplet((byte) MathF.Round(upx.G * ua, MidpointRounding.AwayFromZero));
          o.WriteByte((byte) ';');
          WriteTriplet((byte) MathF.Round(upx.B * ua, MidpointRounding.AwayFromZero));
          o.WriteByte((byte) 'm');

          if (!haveL) // full block
            o.WriteByte(0xDB);

          else {
            // lower color
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
            WriteTriplet((byte) Math.Round(lpx.R * la, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) ';');
            WriteTriplet((byte) Math.Round(lpx.G * la, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) ';');
            WriteTriplet((byte) Math.Round(lpx.B * la, MidpointRounding.AwayFromZero));
            o.WriteByte((byte) 'm');

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

      o.Flush();
      stdOut.Flush();
    }

 
  }

}