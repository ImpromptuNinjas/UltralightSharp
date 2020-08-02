using System;
using System.Text;
using InlineIL;
using JetBrains.Annotations;
using UltralightSharp;
using UltralightSharp.Enums;

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

    public static unsafe void WithPixelsLocked(PixelBufferWorkerCallback callback) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      var pixels = Ultralight.BitmapLockPixels((Bitmap*) p);
      try {
        callback(pixels);
      }
      finally {
        Ultralight.BitmapUnlockPixels((Bitmap*) p);
      }
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

  namespace Safe {

    [PublicAPI]
    public sealed class Bitmap : IDisposable, ICloneable {

      internal readonly unsafe UltralightSharp.Bitmap* _;

      private readonly bool _refOnly;

      internal unsafe Bitmap(UltralightSharp.Bitmap* existingBitmap, bool refOnly = true) {
        _ = existingBitmap;
        _refOnly = refOnly;
      }

      public unsafe Bitmap()
        => _ = UltralightSharp.Bitmap.CreateEmpty();

      public unsafe Bitmap(uint width, uint height, BitmapFormat format)
        => _ = UltralightSharp.Bitmap.Create(width, height, format);

      public unsafe Bitmap(uint width, uint height, BitmapFormat format, uint rowBytes, void* pixels, UIntPtr size, bool shouldCopy)
        => _ = UltralightSharp.Bitmap.CreateFromPixels(width, height, format, rowBytes, pixels, size, shouldCopy);

      public unsafe void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public unsafe bool WritePng(string path) {
        fixed (byte* pBytes = Encoding.UTF8.GetBytes(path))
          return _->WritePng((sbyte*) pBytes);
      }

      public unsafe void Erase()
        => _->Erase();

      public unsafe uint GetBpp()
        => _->GetBpp();

      public unsafe IntPtr RawPixels()
        => (IntPtr) _->RawPixels();

      public unsafe IntPtr LockPixels()
        => (IntPtr) _->LockPixels();

      public unsafe void UnlockPixels()
        => _->UnlockPixels();

      public unsafe void WithPixelsLocked(PixelBufferWorkerCallback callback) {
        var pixels = _->LockPixels();
        try {
          callback((IntPtr) pixels);
        }
        finally {
          _->UnlockPixels();
        }
      }

      public unsafe BitmapFormat GetFormat()
        => _->GetFormat();

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe UIntPtr GetSize()
        => _->GetSize();

      public unsafe uint GetRowBytes()
        => _->GetRowBytes();

      public unsafe void SwapRedBlueChannels()
        => _->SwapRedBlueChannels();

      unsafe Bitmap Clone()
        => new Bitmap(UltralightSharp.Bitmap.Copy(_));

      object ICloneable.Clone()
        => Clone();

    }

  }

}