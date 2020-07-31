using System;
using System.Text;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Bitmap {

    public static unsafe Bitmap* CreateEmpty()
      => Ultralight.CreateEmptyBitmap();

    public static unsafe Bitmap* Create(uint width, uint height, BitmapFormat format)
      => Ultralight.CreateBitmap(width, height, format);

    public static unsafe Bitmap* Copy(Bitmap* existingBitmap)
      => Ultralight.CreateBitmapFromCopy(existingBitmap);

    public static unsafe Bitmap* CreateFromPixels(uint width, uint height, BitmapFormat format, uint rowBytes, void* pixels, UIntPtr size, bool shouldCopy)
      => Ultralight.CreateBitmapFromPixels(width, height, format, rowBytes, pixels, size, shouldCopy);

  }

  [PublicAPI]
  public static class BitmapExtensions {

    public static unsafe void Destroy(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyBitmap((Bitmap*) p);
    }

    public static unsafe bool WritePng(in this Bitmap _, sbyte* path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapWritePng((Bitmap*) p, path);
    }

    public static unsafe bool WritePng(in this Bitmap _, string path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      var bytes = Encoding.UTF8.GetBytes(path);
      fixed (byte* pBytes = bytes)
        return Ultralight.BitmapWritePng((Bitmap*) p, (sbyte*) pBytes);
    }

    public static unsafe void Erase(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapErase((Bitmap*) p);
    }

    public static unsafe uint GetBpp(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetBpp((Bitmap*) p);
    }

    public static unsafe void* RawPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapRawPixels((Bitmap*) p);
    }

    public static unsafe void* LockPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapLockPixels((Bitmap*) p);
    }

    public static unsafe void UnlockPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapUnlockPixels((Bitmap*) p);
    }

    public static unsafe BitmapFormat GetFormat(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetFormat((Bitmap*) p);
    }

    public static unsafe uint GetHeight(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetHeight((Bitmap*) p);
    }

    public static unsafe uint GetWidth(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetWidth((Bitmap*) p);
    }

    public static unsafe UIntPtr GetSize(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetSize((Bitmap*) p);
    }

    public static unsafe uint GetRowBytes(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetRowBytes((Bitmap*) p);
    }

    public static unsafe void SwapRedBlueChannels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapSwapRedBlueChannels((Bitmap*) p);
    }

  }

}