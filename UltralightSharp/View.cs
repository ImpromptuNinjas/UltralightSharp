using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct View {

    public static View* Create(Renderer* renderer, uint width, uint height, bool transparent, Session* session, bool force_cpu_renderer)
      => Ultralight.CreateView(renderer, width, height, transparent, session, force_cpu_renderer);

  }

  [PublicAPI]
  public static unsafe class ViewExtensions {

    public static void Destroy(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyView((View*) p);
    }

    public static void SetFinishLoadingCallback(in this View _, FinishLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetFinishLoadingCallback((View*) p, callback, userData);
    }

    public static void SetAddConsoleMessageCallback(in this View _, AddConsoleMessageCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetAddConsoleMessageCallback((View*) p, callback, userData);
    }

    public static void SetBeginLoadingCallback(in this View _, BeginLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetBeginLoadingCallback((View*) p, callback, userData);
    }

    public static void SetChangeCursorCallback(in this View _, ChangeCursorCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeCursorCallback((View*) p, callback, userData);
    }

    public static void SetChangeTitleCallback(in this View _, ChangeTitleCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeTitleCallback((View*) p, callback, userData);
    }

    public static void SetChangeTooltipCallback(in this View _, ChangeTooltipCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeTooltipCallback((View*) p, callback, userData);
    }

    public static void SetChangeUrlCallback(in this View _, ChangeUrlCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetChangeUrlCallback((View*) p, callback, userData);
    }

    public static void SetCreateChildViewCallback(in this View _, CreateChildViewCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetCreateChildViewCallback((View*) p, callback, userData);
    }

    public static void SetDomReadyCallback(in this View _, DomReadyCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetDomReadyCallback((View*) p, callback, userData);
    }

    public static void SetFailLoadingCallback(in this View _, FailLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetFailLoadingCallback((View*) p, callback, userData);
    }

    public static void SetUpdateHistoryCallback(in this View _, UpdateHistoryCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetUpdateHistoryCallback((View*) p, callback, userData);
    }

    public static void SetWindowObjectReadyCallback(in this View _, WindowObjectReadyCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetWindowObjectReadyCallback((View*) p, callback, userData);
    }

    public static void LoadHtml(in this View _, String* htmlString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadHtml((View*) p, htmlString);
    }

    public static void LoadUrl(in this View _, String* url) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadUrl((View*) p, url);
    }

    public static String* GetUrl(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetUrl((View*) p);
    }

    public static String* GetTitle(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetTitle((View*) p);
    }

    public static uint GetHeight(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetHeight((View*) p);
    }

    public static uint GetWidth(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetWidth((View*) p);
    }

    public static Surface* GetSurface(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetSurface((View*) p);
    }

    public static RenderTarget GetRenderTarget(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetRenderTarget((View*) p);
    }

    public static JsContext* LockJsContext(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewLockJsContext((View*) p);
    }

    public static void UnlockJsContext(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewUnlockJsContext((View*) p);
    }

    public static void Focus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFocus((View*) p);
    }

    public static void Unfocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewUnfocus((View*) p);
    }

    public static bool HasFocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewHasFocus((View*) p);
    }

    public static bool HasInputFocus(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewHasInputFocus((View*) p);
    }

    public static bool IsLoading(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewIsLoading((View*) p);
    }

    public static String* EvaluateScript(this in View _, String* jsString, String** exception) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewEvaluateScript((View*) p, jsString, exception);
    }

    public static View* CreateInspector(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCreateInspectorView((View*) p);
    }

    public static void FireKeyEvent(this in View _, KeyEvent* keyEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireKeyEvent((View*) p, keyEvent);
    }

    public static void FireMouseEvent(this in View _, MouseEvent* mouseEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireMouseEvent((View*) p, mouseEvent);
    }

    public static void FireScrollEvent(this in View _, ScrollEvent* scrollEvent) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewFireScrollEvent((View*) p, scrollEvent);
    }

    public static bool GetNeedsPaint(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewGetNeedsPaint((View*) p);
    }

    public static bool CanGoBack(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCanGoBack((View*) p);
    }

    public static bool CanGoForward(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.ViewCanGoForward((View*) p);
    }

    public static void GoToHistoryOffset(this in View _, int offset) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewGoToHistoryOffset((View*) p, offset);
    }

    public static void Stop(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewStop((View*) p);
    }

    public static void Reload(this in View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewReload((View*) p);
    }

    public static void Resize(this in View _, uint width, uint height) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewResize((View*) p, width, height);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class View : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.View* Unsafe => _;

      internal readonly UltralightSharp.View* _;

      private readonly bool _refOnly;

      public View(UltralightSharp.View* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public View(UltralightSharp.Renderer* renderer, uint width, uint height, bool transparent, UltralightSharp.Session* session, bool force_cpu_renderer)
        => _ = UltralightSharp.View.Create(renderer, width, height, transparent, session, force_cpu_renderer);

      public View(UltralightSharp.Renderer* renderer, uint width, uint height, bool transparent, Session session, bool force_cpu_renderer)
        => _ = UltralightSharp.View.Create(renderer, width, height, transparent, session._, force_cpu_renderer);

      public View(Renderer renderer, uint width, uint height, bool transparent, UltralightSharp.Session* session, bool force_cpu_renderer)
        => _ = UltralightSharp.View.Create(renderer._, width, height, transparent, session, force_cpu_renderer);

      public View(Renderer renderer, uint width, uint height, bool transparent, Session session, bool force_cpu_renderer)
        => _ = UltralightSharp.View.Create(renderer._, width, height, transparent, session._, force_cpu_renderer);

      public void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public void SetFinishLoadingCallback(FinishLoadingCallback callback, IntPtr userData)
        => _->SetFinishLoadingCallback((ud, caller, frameId, isMainFrame, url) => {
          callback((IntPtr) ud, new View(caller), frameId, isMainFrame, url->Read());
        }, (void*) userData);

      public void SetAddConsoleMessageCallback(AddConsoleMessageCallback callback, IntPtr userData)
        => _->SetAddConsoleMessageCallback((ud, caller, source, level, message, lineNumber, columnNumber, sourceId)
            => callback((IntPtr) ud, new View(caller), source, level, message->Read(), lineNumber, columnNumber, sourceId->Read()),
          (void*) userData);

      public void SetBeginLoadingCallback(BeginLoadingCallback callback, IntPtr userData)
        => _->SetBeginLoadingCallback((ud, caller, frameId, isMainFrame, url)
            => callback((IntPtr) ud, new View(caller), frameId, isMainFrame, url->Read()),
          (void*) userData);

      public void SetChangeCursorCallback(ChangeCursorCallback callback, IntPtr userData)
        => _->SetChangeCursorCallback((ud, caller, cursor)
            => callback((IntPtr) ud, new View(caller), cursor),
          (void*) userData);

      public void SetChangeTitleCallback(ChangeTitleCallback callback, IntPtr userData)
        => _->SetChangeTitleCallback((ud, caller, title)
            => callback((IntPtr) ud, new View(caller), title->Read()),
          (void*) userData);

      public void SetChangeTooltipCallback(ChangeTooltipCallback callback, IntPtr userData)
        => _->SetChangeTooltipCallback((ud, caller, tooltip)
            => callback((IntPtr) ud, new View(caller), tooltip->Read()),
          (void*) userData);

      public void SetChangeUrlCallback(ChangeUrlCallback callback, IntPtr userData)
        => _->SetChangeUrlCallback((ud, caller, url)
            => callback((IntPtr) ud, new View(caller), url->Read()),
          (void*) userData);

      public void SetCreateChildViewCallback(CreateChildViewCallback callback, IntPtr userData)
        => _->SetCreateChildViewCallback((ud, caller, url, targetUrl, popup, rect)
            => callback((IntPtr) ud, new View(caller), url->Read(), targetUrl->Read(), popup, rect)._,
          (void*) userData);

      public void SetDomReadyCallback(DomReadyCallback callback, IntPtr userData)
        => _->SetDomReadyCallback((ud, caller, id, frame, url)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read()),
          (void*) userData);

      public void SetFailLoadingCallback(FailLoadingCallback callback, IntPtr userData)
        => _->SetFailLoadingCallback((ud, caller, id, frame, url, description, domain, code)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read(), description->Read(), domain->Read(), code),
          (void*) userData);

      public void SetUpdateHistoryCallback(UpdateHistoryCallback callback, IntPtr userData)
        => _->SetUpdateHistoryCallback((ud, caller)
            => callback((IntPtr) ud, new View(caller)),
          (void*) userData);

      public void SetWindowObjectReadyCallback(WindowObjectReadyCallback callback, IntPtr userData)
        => _->SetWindowObjectReadyCallback((ud, caller, id, frame, url)
            => callback((IntPtr) ud, new View(caller), id, frame, url->Read()),
          (void*) userData);

      public void LoadHtml(string htmlString) {
        var s = String.Create(htmlString);
        _->LoadHtml(s);
        s->Destroy();
      }

      public void LoadUrl(string url) {
        var s = String.Create(url);
        _->LoadUrl(s);
        s->Destroy();
      }

      public string? GetUrl()
        => _->GetUrl()->Read();

      public string? GetTitle()
        => _->GetTitle()->Read();

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public Surface GetSurface()
        => new Surface(_->GetSurface());

      public RenderTarget GetRenderTarget()
        => _->GetRenderTarget().AsSafe();

      public JsGlobalContext LockJsContext()
        => new JsGlobalContext(_->LockJsContext());

      public void UnlockJsContext()
        => _->UnlockJsContext();

      public void Focus()
        => _->Focus();

      public void Unfocus()
        => _->Unfocus();

      public bool HasFocus()
        => _->HasFocus();

      public bool HasInputFocus()
        => _->HasInputFocus();

      public bool IsLoading()
        => _->IsLoading();

      public string? EvaluateScript(string jsString) {
        var s = String.Create(jsString);
        var r = _->EvaluateScript(s, null);
        return r->Read();
      }

      public string? EvaluateScript(string jsString, out string? exception) {
        var s = String.Create(jsString);
        String* pExc;
        var r = _->EvaluateScript(s, &pExc);
        exception = pExc == default ? null : pExc->Read();
        return r->Read();
      }

      public View CreateInspector()
        => new View(_->CreateInspector());

      public void FireKeyEvent(KeyEvent keyEvent)
        => _->FireKeyEvent(keyEvent._);

      public void FireMouseEvent(MouseEvent mouseEvent)
        => _->FireMouseEvent(mouseEvent._);

      public void FireScrollEvent(ScrollEvent scrollEvent)
        => _->FireScrollEvent(scrollEvent._);

      public bool GetNeedsPaint()
        => _->GetNeedsPaint();

      public bool CanGoBack()
        => _->CanGoBack();

      public bool CanGoForward()
        => _->CanGoForward();

      public void GoToHistoryOffset(int offset)
        => _->GoToHistoryOffset(offset);

      public void Stop()
        => _->Stop();

      public void Reload()
        => _->Reload();

      public void Resize(uint width, uint height)
        => _->Resize(width, height);

    }

  }

}