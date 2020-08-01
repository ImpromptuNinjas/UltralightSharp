using System;
using System.Runtime.CompilerServices;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Overlay {

    public static unsafe Overlay* Create(Window* window, uint width, uint height, int x, int y)
      => AppCore.CreateOverlay(window, width, height, x, y);

    public static unsafe Overlay* Create(Window* window, View* view, int x, int y)
      => AppCore.CreateOverlayWithView(window, view, x, y);

  }

  public static class OverlayExtensions {

    public static unsafe void Destroy(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyOverlay((Overlay*) p);
    }

    public static unsafe void Focus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayFocus((Overlay*) p);
    }

    public static unsafe void Unfocus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayUnfocus((Overlay*) p);
    }

    public static unsafe void Show(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayShow((Overlay*) p);
    }

    public static unsafe void Hide(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.OverlayHide((Overlay*) p);
    }

    public static unsafe View* GetView(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetView((Overlay*) p);
    }

    public static unsafe uint GetHeight(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetHeight((Overlay*) p);
    }

    public static unsafe uint GetWidth(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetWidth((Overlay*) p);
    }

    public static unsafe int GetX(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetX((Overlay*) p);
    }

    public static unsafe int GetY(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayGetY((Overlay*) p);
    }

    public static unsafe bool HasFocus(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayHasFocus((Overlay*) p);
    }

    public static unsafe bool IsHidden(in this Overlay _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.OverlayIsHidden((Overlay*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Overlay : IDisposable {

      internal readonly unsafe UltralightSharp.Overlay* _;

      public unsafe Overlay(UltralightSharp.Overlay* p)
        => _ = p;

      public unsafe Overlay(UltralightSharp.Window* window, uint width, uint height, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, width, height, x, y);

      public unsafe Overlay(UltralightSharp.Window* window, UltralightSharp.View* view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, view, x, y);

      public unsafe Overlay(UltralightSharp.Window* window, View view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window, view._, x, y);

      public unsafe Overlay(Window window, UltralightSharp.View* view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window._, view, x, y);

      public unsafe Overlay(Window window, View view, int x, int y)
        => _ = UltralightSharp.Overlay.Create(window._, view._, x, y);

      public unsafe void Dispose()
        => _->Destroy();

      public unsafe void Focus()
        => _->Focus();

      public unsafe void Unfocus()
        => _->Unfocus();

      public unsafe void Show()
        => _->Show();

      public unsafe void Hide()
        => _->Hide();

      public unsafe UltralightSharp.View* GetViewUnsafe()
        => _->GetView();

      public unsafe View GetView()
        => new View(_->GetView());

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe int GetX()
        => _->GetX();

      public unsafe int GetY()
        => _->GetY();

      public unsafe bool HasFocus()
        => _->HasFocus();

      public unsafe bool IsHidden()
        => _->IsHidden();

    }

  }

}