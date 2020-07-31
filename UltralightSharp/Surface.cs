using System;
using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

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

}