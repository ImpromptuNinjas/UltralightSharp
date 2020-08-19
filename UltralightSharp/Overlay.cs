using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Overlay {

    public static Overlay* Create(Window* window, uint width, uint height, int x, int y)
      => AppCore.CreateOverlay(window, width, height, x, y);

    public static Overlay* Create(Window* window, View* view, int x, int y)
      => AppCore.CreateOverlayWithView(window, view, x, y);

  }

  public static unsafe class OverlayExtensions {

    public static void Destroy(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyOverlay((Overlay*) p);
    }

    public static void Focus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayFocus((Overlay*) p);
    }

    public static void Unfocus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayUnfocus((Overlay*) p);
    }

    public static void Show(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayShow((Overlay*) p);
    }

    public static void Hide(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayHide((Overlay*) p);
    }

    public static View* GetView(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetView((Overlay*) p);
    }

    public static uint GetHeight(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetHeight((Overlay*) p);
    }

    public static uint GetWidth(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetWidth((Overlay*) p);
    }

    public static int GetX(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetX((Overlay*) p);
    }

    public static int GetY(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetY((Overlay*) p);
    }

    public static bool HasFocus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayHasFocus((Overlay*) p);
    }

    public static bool IsHidden(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayIsHidden((Overlay*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Overlay : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Overlay* Unsafe => _;

      internal readonly UltralightSharp.Overlay* _;

      public Overlay(UltralightSharp.Overlay* p)
        => _ = p;

      public Overlay(UltralightSharp.Window* window, uint width, uint height, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, width, height, x, y);

      public Overlay(UltralightSharp.Window* window, UltralightSharp.View* view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, view, x, y);

      public Overlay(UltralightSharp.Window* window, View view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, view._, x, y);

      public Overlay(Window window, UltralightSharp.View* view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window._, view, x, y);

      public Overlay(Window window, View view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window._, view._, x, y);

      public void Dispose()
        => _->Destroy();

      public void Focus()
        => _->Focus();

      public void Unfocus()
        => _->Unfocus();

      public void Show()
        => _->Show();

      public void Hide()
        => _->Hide();

      public View GetView()
        => new View(_->GetView());

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public int GetX()
        => _->GetX();

      public int GetY()
        => _->GetY();

      public bool HasFocus()
        => _->HasFocus();

      public bool IsHidden()
        => _->IsHidden();

    }

  }

}