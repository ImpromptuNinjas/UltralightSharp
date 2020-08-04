using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using ImpromptuNinjas.UltralightSharp;
using UltralightSharp.Enums;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Experimental.Rendering;
using Renderer = ImpromptuNinjas.UltralightSharp.Renderer;
using String = ImpromptuNinjas.UltralightSharp.String;

[RequireComponent(typeof(UnityEngine.Renderer))]
public class UltralightBrowserDemo : MonoBehaviour {

  [NonSerialized]
  private unsafe Renderer* _ulRenderer;

  [NonSerialized]
  private unsafe View* _ulView;

  [NonSerialized]
  private Texture2D _texture;

  [SerializeField]
  private string url;

  [NonSerialized]
  private bool _isDomReady;

  [NonSerialized]
  private bool _isWindowReady;

  [NonSerialized]
  private bool _failed;

  [NonSerialized]
  private bool _isLoaded;

  private bool _willRender;

  public unsafe string Title => _ulView != null ? _ulView->GetTitle()->Read() : null;

  public unsafe bool IsLoading => _ulView != null && _ulView->IsLoading();

  public bool IsDomReady => _isDomReady;

  public bool IsWindowReady => _isWindowReady;

  public bool Failed => _failed;

  public bool IsLoaded => _isLoaded;

  public bool WillRender => _willRender;

  public unsafe string Url {
    get => url;
    set {
      if (_ulView == null) return;

      url = value;
      var s = String.Create(value);
      _ulView->LoadUrl(s);
      s->Destroy();
    }
  }

  // Start is called before the first frame update
  unsafe void OnEnable() {
    var texSize = new Vector2(640, 480);
    _texture = new Texture2D((int) texSize.x, (int) texSize.y,
      GraphicsFormat.B8G8R8A8_UNorm, TextureCreationFlags.None) {
      hideFlags = HideFlags.HideAndDontSave
    };
    var cfg = Config.Create();

    {
      var cachePath = String.Create(Path.Combine(Application.streamingAssetsPath, "ultralight", "cache"));
      cfg->SetCachePath(cachePath);
      cachePath->Destroy();
    }

    {
      // ReSharper disable once AssignNullToNotNullAttribute
      var resourcePath = String.Create(Path.Combine(Application.streamingAssetsPath, "ultralight", "resources"));
      cfg->SetResourcePath(resourcePath);
      resourcePath->Destroy();
    }

    cfg->SetUseGpuRenderer(false);
    cfg->SetEnableImages(true);
    cfg->SetEnableJavaScript(false);

    AppCore.EnablePlatformFontLoader();

    {
      var assetsPath = String.Create(Path.Combine(Application.streamingAssetsPath, "ultralight", "assets"));
      AppCore.EnablePlatformFileSystem(assetsPath);
      assetsPath->Destroy();
    }

    _ulRenderer = Renderer.Create(cfg);
    var sessionName = String.Create("Demo");
    var session = Session.Create(_ulRenderer, false, sessionName);

    _ulView = View.Create(_ulRenderer, (uint) texSize.x, (uint) texSize.y, false, session);

    GetComponent<UnityEngine.Renderer>().material.mainTexture = _texture;

    var gch = GCHandle.Alloc(this);
    var gchPtr = (void*) GCHandle.ToIntPtr(gch);

    _ulView->SetAddConsoleMessageCallback((userData, caller, source, level, msg, lineNumber, columnNumber, sourceId) => {
      switch (level) {
        default:
          Debug.Log($"[Ultralight Console] [{level}] {sourceId->Read()}:{lineNumber}:{columnNumber} {msg->Read()}");
          break;
        case MessageLevel.Error:
          Debug.LogError($"[Ultralight Console] {sourceId->Read()}:{lineNumber}:{columnNumber} {msg->Read()}");
          break;
        case MessageLevel.Warning:
          Debug.LogError($"[Ultralight Console] {sourceId->Read()}:{lineNumber}:{columnNumber} {msg->Read()}");
          break;
      }
    }, gchPtr);

    _ulView->SetDomReadyCallback((userData, caller, id, isMainFrame, readyUrl) => {
      if (!isMainFrame)
        return;

      var r = (UltralightBrowserDemo) GCHandle.FromIntPtr((IntPtr) userData).Target;

      r._isDomReady = true;
    }, gchPtr);

    _ulView->SetFailLoadingCallback((userData, caller, id, isMainFrame, failedUrl, failDesc, errorDomain, errorCode) => {
      if (!isMainFrame)
        return;

      var r = (UltralightBrowserDemo) GCHandle.FromIntPtr((IntPtr) userData).Target;

      r._failed = true;
      r._isLoaded = true;
    }, gchPtr);

    _ulView->SetFinishLoadingCallback((userData, caller, id, isMainFrame, finishedUrl) => {
      if (!isMainFrame)
        return;

      var r = (UltralightBrowserDemo) GCHandle.FromIntPtr((IntPtr) userData).Target;

      r._failed = false;
      r._isLoaded = true;
    }, gchPtr);

    _ulView->SetWindowObjectReadyCallback((userData, caller, id, isMainFrame, readyUrl) => {
      if (!isMainFrame)
        return;

      var r = (UltralightBrowserDemo) GCHandle.FromIntPtr((IntPtr) userData).Target;

      r._isWindowReady = true;
    }, gchPtr);

    _ulView->SetChangeUrlCallback((userData, caller, newUrl) => {
      if (!caller->IsLoading())
        return;

      var r = (UltralightBrowserDemo) GCHandle.FromIntPtr((IntPtr) userData).Target;

      r._isDomReady = false;
      r._failed = false;
      r._isLoaded = false;
      r._isWindowReady = false;
    }, gchPtr);

    Url = url;
  }

  public unsafe void LoadHtml(string htmlString) {
    if (_ulView == null)
      return;

    var s = String.Create(htmlString);
    _ulView->LoadHtml(s);
    s->Destroy();
  }

  private unsafe void OnDisable() {
    _ulView->Destroy();
    _ulView = null;
    _ulRenderer->Destroy();
    _ulRenderer = null;
    Destroy(_texture);
    _texture = null;

    _isDomReady = false;
    _failed = false;
    _isLoaded = false;
    _isWindowReady = false;
    url = null;
  }

  public unsafe void OnWillRenderObject() {
    _willRender = true;
    if (_ulRenderer == null || _ulView == null || _texture == null)
      return;

    Ultralight.Update(_ulRenderer);
    Ultralight.Render(_ulRenderer);

    var surface = _ulView->GetSurface();
    var bitmap = surface->GetBitmap();
    var pixels = bitmap->LockPixels();
    var rawData = _texture.GetRawTextureData<byte>();
    var size = Math.Min((uint) rawData.Length, bitmap->GetSize().ToUInt32());
    Unsafe.CopyBlockUnaligned(rawData.GetUnsafePtr(), pixels, size);
    _texture.Apply();
    bitmap->UnlockPixels();
  }

  private void OnPostRender()
    => _willRender = false;

  static unsafe UltralightBrowserDemo() {
    // setup logging
    LoggerLogMessageCallback cb = LoggerCallback;
    Ultralight.PlatformSetLogger(new ImpromptuNinjas.UltralightSharp.Logger {LogMessage = cb});
  }

  private static unsafe void LoggerCallback(LogLevel logLevel, String* msg) {
    switch (logLevel) {
      default:
        Debug.Log($"[Ultralight] [{logLevel}] {msg->Read()}");
        break;
      case LogLevel.Error:
        Debug.LogError($"[Ultralight] {msg->Read()}");
        break;
      case LogLevel.Warning:
        Debug.LogError($"[Ultralight] {msg->Read()}");
        break;
    }
  }

}