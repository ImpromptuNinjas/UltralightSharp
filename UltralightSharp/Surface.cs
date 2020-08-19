using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Surface {

  }

  [PublicAPI]
  public static unsafe class SurfaceExtensions {

    public static void ClearDirtyBounds(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceClearDirtyBounds((Surface*) p);
    }

    public static IntRect GetDirtyBounds(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetDirtyBounds((Surface*) p);
    }

    public static uint GetHeight(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetHeight((Surface*) p);
    }

    public static uint GetWidth(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetWidth((Surface*) p);
    }

    public static uint GetRowBytes(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetRowBytes((Surface*) p);
    }

    public static UIntPtr GetSize(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetSize((Surface*) p);
    }

    public static void* GetUserData(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceGetUserData((Surface*) p);
    }

    public static void* LockPixels(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SurfaceLockPixels((Surface*) p);
    }

    public static void UnlockPixels(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceUnlockPixels((Surface*) p);
    }

    public static void SetDirtyBounds(in this Surface _, IntRect bounds) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.SurfaceSetDirtyBounds((Surface*) p, bounds);
    }

    public static Bitmap* GetBitmap(in this Surface _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.BitmapSurfaceGetBitmap((Surface*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Surface {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Surface* Unsafe => _;

      internal readonly UltralightSharp.Surface* _;

      public Surface(UltralightSharp.Surface* p)
        => _ = p;

      public void ClearDirtyBounds()
        => _->ClearDirtyBounds();

      public IntRect GetDirtyBounds()
        => _->GetDirtyBounds();

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public uint GetRowBytes()
        => _->GetRowBytes();

      public UIntPtr GetSize()
        => _->GetSize();

      public IntPtr GetUserData()
        => (IntPtr) _->GetUserData();

      public void UnlockPixels()
        => _->UnlockPixels();

      public void SetDirtyBounds(IntRect bounds)
        => _->SetDirtyBounds(bounds);

      public Bitmap GetBitmap()
        => new Bitmap(_->GetBitmap());

    }

  }

}