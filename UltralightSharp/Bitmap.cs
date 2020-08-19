using System;
using System.ComponentModel;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Bitmap {

    public static Bitmap* CreateEmpty()
      => Ultralight.CreateEmptyBitmap();

    public static Bitmap* Create(uint width, uint height, BitmapFormat format)
      => Ultralight.CreateBitmap(width, height, format);

    public static Bitmap* Copy(Bitmap* existingBitmap)
      => Ultralight.CreateBitmapFromCopy(existingBitmap);

    public static Bitmap* CreateFromPixels(uint width, uint height, BitmapFormat format, uint rowBytes, void* pixels, UIntPtr size, bool shouldCopy)
      => Ultralight.CreateBitmapFromPixels(width, height, format, rowBytes, pixels, size, shouldCopy);

  }

  [PublicAPI]
  public static unsafe class BitmapExtensions {

    public static void Destroy(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyBitmap((Bitmap*) p);
    }

    public static bool WritePng(in this Bitmap _, sbyte* path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapWritePng((Bitmap*) p, path);
    }

    public static bool WritePng(in this Bitmap _, string path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      var bytes = Encoding.UTF8.GetBytes(path);
      fixed (byte* pBytes = bytes)
        return Ultralight.BitmapWritePng((Bitmap*) p, (sbyte*) pBytes);
    }

    public static bool IsEmpty(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapIsEmpty((Bitmap*) p);
    }

    public static void Erase(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapErase((Bitmap*) p);
    }

    public static uint GetBpp(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetBpp((Bitmap*) p);
    }

    public static void* RawPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapRawPixels((Bitmap*) p);
    }

    public static void* LockPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapLockPixels((Bitmap*) p);
    }

    public static void UnlockPixels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapUnlockPixels((Bitmap*) p);
    }

    public static void WithPixelsLocked(PixelBufferWorkerCallback callback) {
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

    public static BitmapFormat GetFormat(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetFormat((Bitmap*) p);
    }

    public static uint GetHeight(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetHeight((Bitmap*) p);
    }

    public static uint GetWidth(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetWidth((Bitmap*) p);
    }

    public static UIntPtr GetSize(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetSize((Bitmap*) p);
    }

    public static uint GetRowBytes(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapGetRowBytes((Bitmap*) p);
    }

    public static void SwapRedBlueChannels(in this Bitmap _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.BitmapSwapRedBlueChannels((Bitmap*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Bitmap : IDisposable, ICloneable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Bitmap* Unsafe => _;

      internal readonly UltralightSharp.Bitmap* _;

      private readonly bool _refOnly;

      internal Bitmap(UltralightSharp.Bitmap* existingBitmap, bool refOnly = true) {
        _ = existingBitmap;
        _refOnly = refOnly;
      }

      public Bitmap()
        => _ = UltralightSharp.Bitmap.CreateEmpty();

      public Bitmap(uint width, uint height, BitmapFormat format)
        => _ = UltralightSharp.Bitmap.Create(width, height, format);

      public Bitmap(uint width, uint height, BitmapFormat format, uint rowBytes, void* pixels, UIntPtr size, bool shouldCopy)
        => _ = UltralightSharp.Bitmap.CreateFromPixels(width, height, format, rowBytes, pixels, size, shouldCopy);

      public void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public bool WritePng(string path) {
        fixed (byte* pBytes = Encoding.UTF8.GetBytes(path))
          return _->WritePng((sbyte*) pBytes);
      }

      public bool IsEmpty()
        => _->IsEmpty();

      public void Erase()
        => _->Erase();

      public uint GetBpp()
        => _->GetBpp();

      public IntPtr RawPixels()
        => (IntPtr) _->RawPixels();

      public IntPtr LockPixels()
        => (IntPtr) _->LockPixels();

      public void UnlockPixels()
        => _->UnlockPixels();

      public void WithPixelsLocked(PixelBufferWorkerCallback callback) {
        var pixels = _->LockPixels();
        try {
          callback((IntPtr) pixels);
        }
        finally {
          _->UnlockPixels();
        }
      }

      public BitmapFormat GetFormat()
        => _->GetFormat();

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public UIntPtr GetSize()
        => _->GetSize();

      public uint GetRowBytes()
        => _->GetRowBytes();

      public void SwapRedBlueChannels()
        => _->SwapRedBlueChannels();

      Bitmap Clone()
        => new Bitmap(UltralightSharp.Bitmap.Copy(_));

      object ICloneable.Clone()
        => Clone();

    }

  }

}