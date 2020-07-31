using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct View {

    public static unsafe View* Create(Renderer* renderer, uint width, uint height, bool transparent, Session* session)
      => Ultralight.CreateView(renderer, width, height, transparent, session);

  }

  [PublicAPI]
  public static class ViewExtensions {

    public static unsafe void Destroy(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyView((View*) p);
    }

    public static unsafe void SetFinishLoadingCallback(in this View _, FinishLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetFinishLoadingCallback((View*) p, callback, userData);
    }

    public static unsafe void SetAddConsoleMessageCallback(in this View _, AddConsoleMessageCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetAddConsoleMessageCallback((View*) p, callback, userData);
    }

    public static unsafe void SetAddConsoleMessageCallback(in this View _, BeginLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetBeginLoadingCallback((View*) p, callback, userData);
    }

    public static unsafe void SetChangeCursorCallback(in this View _, ChangeTooltipCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeCursorCallback((View*) p, callback, userData);
    }

    public static unsafe void SetChangeTitleCallback(in this View _, ChangeTitleCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeTitleCallback((View*) p, callback, userData);
    }

    public static unsafe void SetChangeTooltipCallback(in this View _, ChangeTooltipCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeTooltipCallback((View*) p, callback, userData);
    }

    public static unsafe void SetChangeUrlCallback(in this View _, ChangeUrlCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeUrlCallback((View*) p, callback, userData);
    }

    public static unsafe void SetCreateChildViewCallback(in this View _, CreateChildViewCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetCreateChildViewCallback((View*) p, callback, userData);
    }

    public static unsafe void SetDomReadyCallback(in this View _, DomReadyCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetDomReadyCallback((View*) p, callback, userData);
    }

    public static unsafe void SetFailLoadingCallback(in this View _, FailLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetFailLoadingCallback((View*) p, callback, userData);
    }

    public static unsafe void SetUpdateHistoryCallback(in this View _, UpdateHistoryCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetUpdateHistoryCallback((View*) p, callback, userData);
    }

    public static unsafe void SetWindowObjectReadyCallback(in this View _, WindowObjectReadyCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetWindowObjectReadyCallback((View*) p, callback, userData);
    }

    public static unsafe void LoadHtml(in this View _, String* htmlString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadHtml((View*) p, htmlString);
    }

    public static unsafe void LoadUrl(in this View _, String* url) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadUrl((View*) p, url);
    }

    public static unsafe String* GetUrl(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetUrl((View*) p);
    }

    public static unsafe String* GetTitle(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetTitle((View*) p);
    }

    public static unsafe uint GetHeight(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetHeight((View*) p);
    }

    public static unsafe uint GetWidth(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetWidth((View*) p);
    }

    public static unsafe Surface* GetSurface(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetSurface((View*) p);
    }

    public static unsafe RenderTarget GetRenderTarget(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetRenderTarget((View*) p);
    }

    public static unsafe JsContext* LockJsContext(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewLockJsContext((View*) p);
    }

    public static unsafe void UnlockJsContext(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewUnlockJsContext((View*) p);
    }

    public static unsafe void Focus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFocus((View*) p);
    }

    public static unsafe void Unfocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewUnfocus((View*) p);
    }

    public static unsafe bool HasFocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewHasFocus((View*) p);
    }

    public static unsafe bool HasInputFocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewHasInputFocus((View*) p);
    }

    public static unsafe bool IsLoading(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewIsLoading((View*) p);
    }

  }

}