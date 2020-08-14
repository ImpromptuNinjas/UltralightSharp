using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

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

    public static unsafe void SetBeginLoadingCallback(in this View _, BeginLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetBeginLoadingCallback((View*) p, callback, userData);
    }

    public static unsafe void SetChangeCursorCallback(in this View _, ChangeCursorCallback callback, void* userData) {
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

    public static unsafe String* EvaluateScript(this in View _, String* jsString, String** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewEvaluateScript((View*) p, jsString, exception);
    }

    public static unsafe View* CreateInspector(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCreateInspectorView((View*) p);
    }

    public static unsafe void FireKeyEvent(this in View _, KeyEvent* keyEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireKeyEvent((View*) p, keyEvent);
    }

    public static unsafe void FireMouseEvent(this in View _, MouseEvent* mouseEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireMouseEvent((View*) p, mouseEvent);
    }

    public static unsafe void FireScrollEvent(this in View _, ScrollEvent* scrollEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireScrollEvent((View*) p, scrollEvent);
    }

    public static unsafe bool GetNeedsPaint(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetNeedsPaint((View*) p);
    }

    public static unsafe bool CanGoBack(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCanGoBack((View*) p);
    }

    public static unsafe bool CanGoForward(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCanGoForward((View*) p);
    }

    public static unsafe void GoToHistoryOffset(this in View _, int offset) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewGoToHistoryOffset((View*) p, offset);
    }

    public static unsafe void Stop(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewStop((View*) p);
    }

    public static unsafe void Reload(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewReload((View*) p);
    }

    public static unsafe void Resize(this in View _, uint width, uint height) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewResize((View*) p, width, height);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class View : IDisposable {

      internal readonly unsafe UltralightSharp.View* _;

      private readonly bool _refOnly;

      public unsafe View(UltralightSharp.View* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public unsafe View(UltralightSharp.Renderer* renderer, uint width, uint height, bool transparent, UltralightSharp.Session* session)
        => _ = UltralightSharp.View.Create(renderer, width, height, transparent, session);

      public unsafe View(UltralightSharp.Renderer* renderer, uint width, uint height, bool transparent, Session session)
        => _ = UltralightSharp.View.Create(renderer, width, height, transparent, session._);

      public unsafe View(Renderer renderer, uint width, uint height, bool transparent, UltralightSharp.Session* session)
        => _ = UltralightSharp.View.Create(renderer._, width, height, transparent, session);

      public unsafe View(Renderer renderer, uint width, uint height, bool transparent, Session session)
        => _ = UltralightSharp.View.Create(renderer._, width, height, transparent, session._);

      public unsafe void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public unsafe void SetFinishLoadingCallback(FinishLoadingCallback callback, IntPtr userData)
        => _->SetFinishLoadingCallback((ud, caller, frameId, isMainFrame, url) => {
          callback((IntPtr) ud, new View(caller), frameId, isMainFrame, url->Read());
        }, (void*) userData);

      public unsafe void SetAddConsoleMessageCallback(AddConsoleMessageCallback callback, IntPtr userData)
        => _->SetAddConsoleMessageCallback((ud, caller, source, level, message, lineNumber, columnNumber, sourceId)
            => callback((IntPtr) ud, new View(caller), source, level, message->Read(), lineNumber, columnNumber, sourceId->Read()),
          (void*) userData);

      public unsafe void SetBeginLoadingCallback(BeginLoadingCallback callback, IntPtr userData)
        => _->SetBeginLoadingCallback((ud, caller, frameId, isMainFrame, url)
            => callback((IntPtr) ud, new View(caller), frameId, isMainFrame, url->Read()),
          (void*) userData);

      public unsafe void SetChangeCursorCallback(ChangeCursorCallback callback, IntPtr userData)
        => _->SetChangeCursorCallback((ud, caller, cursor)
            => callback((IntPtr) ud, new View(caller), cursor),
          (void*) userData);

      public unsafe void SetChangeTitleCallback(ChangeTitleCallback callback, IntPtr userData)
        => _->SetChangeTitleCallback((ud, caller, title)
            => callback((IntPtr) ud, new View(caller), title->Read()),
          (void*) userData);

      public unsafe void SetChangeTooltipCallback(ChangeTooltipCallback callback, IntPtr userData)
        => _->SetChangeTooltipCallback((ud, caller, tooltip)
            => callback((IntPtr) ud, new View(caller), tooltip->Read()),
          (void*) userData);

      public unsafe void SetChangeUrlCallback(ChangeUrlCallback callback, IntPtr userData)
        => _->SetChangeUrlCallback((ud, caller, url)
            => callback((IntPtr) ud, new View(caller), url->Read()),
          (void*) userData);

      public unsafe void SetCreateChildViewCallback(CreateChildViewCallback callback, IntPtr userData)
        => _->SetCreateChildViewCallback((ud, caller, url, targetUrl, popup, rect)
            => callback((IntPtr) ud, new View(caller), url->Read(), targetUrl->Read(), popup, rect)._,
          (void*) userData);

      public unsafe void SetDomReadyCallback(DomReadyCallback callback, IntPtr userData)
        => _->SetDomReadyCallback((ud, caller, id, frame, url)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read()),
          (void*) userData);

      public unsafe void SetFailLoadingCallback(FailLoadingCallback callback, IntPtr userData)
        => _->SetFailLoadingCallback((ud, caller, id, frame, url, description, domain, code)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read(), description->Read(), domain->Read(), code),
          (void*) userData);

      public unsafe void SetUpdateHistoryCallback(UpdateHistoryCallback callback, IntPtr userData)
        => _->SetUpdateHistoryCallback((ud, caller)
            => callback((IntPtr) ud, new View(caller)),
          (void*) userData);

      public unsafe void SetWindowObjectReadyCallback(WindowObjectReadyCallback callback, IntPtr userData)
        => _->SetWindowObjectReadyCallback((ud, caller, id, frame, url)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read()),
          (void*) userData);

      public unsafe void LoadHtml(string htmlString) {
        var s = String.Create(htmlString);
        _->LoadHtml(s);
        s->Destroy();
      }

      public unsafe void LoadUrl(string url) {
        var s = String.Create(url);
        _->LoadUrl(s);
        s->Destroy();
      }


      public unsafe string? GetUrl()
        => _->GetUrl()->Read();


      public unsafe string? GetTitle()
        => _->GetTitle()->Read();

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe Surface GetSurface()
        => new Surface(_->GetSurface());

      public unsafe RenderTarget GetRenderTarget()
        => _->GetRenderTarget();


      public unsafe JsContext LockJsContext()
        => new JsContext(_->LockJsContext());

      public unsafe void UnlockJsContext()
        => _->UnlockJsContext();

      public unsafe void Focus()
        => _->Focus();

      public unsafe void Unfocus()
        => _->Unfocus();

      public unsafe bool HasFocus()
        => _->HasFocus();

      public unsafe bool HasInputFocus()
        => _->HasInputFocus();

      public unsafe bool IsLoading()
        => _->IsLoading();

      public unsafe string? EvaluateScript(string jsString, out string? exception) {
        var s = String.Create(jsString);
        String* pExc;
        var r = _->EvaluateScript(s, &pExc);
        exception = pExc == default ? null : pExc->Read();
        return r->Read();
      }

      public unsafe View CreateInspector()
        => new View(_->CreateInspector());


      public unsafe void FireKeyEvent(KeyEvent keyEvent)
        => _->FireKeyEvent(keyEvent._);

      public unsafe void FireMouseEvent(MouseEvent mouseEvent)
        => _->FireMouseEvent(mouseEvent._);

      public unsafe void FireScrollEvent(ScrollEvent scrollEvent)
        => _->FireScrollEvent(scrollEvent._);

      public unsafe bool GetNeedsPaint()
        => _->GetNeedsPaint();

      public unsafe bool CanGoBack()
        => _->CanGoBack();

      public unsafe bool CanGoForward()
        => _->CanGoForward();

      public unsafe void GoToHistoryOffset(int offset)
        => _->GoToHistoryOffset(offset);

      public unsafe void Stop()
        => _->Stop();

      public unsafe void Reload()
        => _->Reload();

      public unsafe void Resize(uint width, uint height)
        => _->Resize(width, height);

    }

  }

}