using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Surface {

  }

  [PublicAPI]
  public static class SurfaceExtensions {

    public static unsafe void ClearDirtyBounds(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceClearDirtyBounds((Surface*) p);
    }

    public static unsafe IntRect GetDirtyBounds(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetDirtyBounds((Surface*) p);
    }

    public static unsafe uint GetHeight(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetHeight((Surface*) p);
    }

    public static unsafe uint GetWidth(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetWidth((Surface*) p);
    }

    public static unsafe uint GetRowBytes(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetRowBytes((Surface*) p);
    }

    public static unsafe UIntPtr GetSize(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetSize((Surface*) p);
    }

    public static unsafe void* GetUserData(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetUserData((Surface*) p);
    }

    public static unsafe void* LockPixels(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceLockPixels((Surface*) p);
    }

    public static unsafe void UnlockPixels(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceUnlockPixels((Surface*) p);
    }

    public static unsafe void SetDirtyBounds(in this Surface _, IntRect bounds) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceSetDirtyBounds((Surface*) p, bounds);
    }

    public static unsafe Bitmap* GetBitmap(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapSurfaceGetBitmap((Surface*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Surface {

      internal readonly unsafe UltralightSharp.Surface* _;

      public unsafe Surface(UltralightSharp.Surface* p)
        => _ = p;

      public unsafe void ClearDirtyBounds()
        => _->ClearDirtyBounds();

      public unsafe IntRect GetDirtyBounds()
        => _->GetDirtyBounds();

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe uint GetRowBytes()
        => _->GetRowBytes();

      public unsafe UIntPtr GetSize()
        => _->GetSize();

      public unsafe IntPtr GetUserData()
        => (IntPtr) _->GetUserData();

      public unsafe void UnlockPixels()
        => _->UnlockPixels();

      public unsafe void SetDirtyBounds(IntRect bounds)
        => _->SetDirtyBounds(bounds);

      public unsafe Bitmap GetBitmap()
        => new Bitmap(_->GetBitmap());

    }

  }

}