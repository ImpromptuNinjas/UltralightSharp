using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace ImpromptuNinjas.UltralightSharpSharp.Demo {

  public static partial class DemoProgram {

    private static unsafe void RenderAnsi24BitColor<TColor>(void* pixels, uint w, uint h, uint bpp) where TColor : unmanaged, IPixel<TColor> {
      //Console.WriteLine($"0x{(ulong) pixels} {w}x{h}x{bpp * 8}");
      if (bpp != 4) throw new NotImplementedException();

      var aspect = w / (double) h;

      var cw = 0;

      try {
        cw = Console.WindowWidth;
      }
      catch {
        // ok
      }

      if (cw <= 0)
        cw = 72;

      var sq = (cw / aspect) - 1;

      var aw = (int) Math.Floor(sq * aspect);
      var ah = (int) Math.Floor(sq / aspect);

      var span = new ReadOnlySpan<TColor>((byte*) pixels, checked((int) (w * h)));
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

      for (var y = 0; y < ah; y += 2) {
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
            u[x].ToRgba32(ref lpx);
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
            o.WriteByte(0xDF);
          }
        }

        o.WriteByte(0x1B);
        o.WriteByte((byte) '[');
        o.WriteByte((byte) '0');
        o.WriteByte((byte) 'm');
        o.WriteByte((byte) ' ');
        o.WriteByte((byte) '\n');
      }

      o.Flush();
      stdOut.Flush();
    }

  }

}