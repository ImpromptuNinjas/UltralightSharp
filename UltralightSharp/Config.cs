using System;
using System.ComponentModel;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Config {

    public static Config* Create()
      => Ultralight.CreateConfig();

  }

  [PublicAPI]
  public static unsafe class ConfigExtensions {

    public static void Destroy(in this Config _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyConfig((Config*) p);
    }

    public static void SetAnimationTimerDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetAnimationTimerDelay((Config*) p, delay);
    }

    public static void SetCachePath(in this Config _, String* cachePath) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetCachePath((Config*) p, cachePath);
    }

    public static void SetDeviceScale(in this Config _, double value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetDeviceScale((Config*) p, value);
    }

    public static void SetEnableImages(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetEnableImages((Config*) p, enabled);
    }

    public static void SetEnableJavaScript(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetEnableJavaScript((Config*) p, enabled);
    }

    public static void SetFaceWinding(in this Config _, FaceWinding winding) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFaceWinding((Config*) p, winding);
    }

    public static void SetFontFamilyFixed(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilyFixed((Config*) p, fontName);
    }

    public static void SetFontFamilySansSerif(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilySansSerif((Config*) p, fontName);
    }

    public static void SetFontFamilySerif(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilySerif((Config*) p, fontName);
    }

    public static void SetFontFamilyStandard(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilyStandard((Config*) p, fontName);
    }

    public static void SetFontGamma(in this Config _, double fontGamma) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontGamma((Config*) p, fontGamma);
    }

    public static void SetFontHinting(in this Config _, FontHinting fontHinting) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontHinting((Config*) p, fontHinting);
    }

    public static void SetForceRepaint(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetForceRepaint((Config*) p, enabled);
    }

    public static void SetMemoryCacheSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMemoryCacheSize((Config*) p, size);
    }

    public static void SetMinLargeHeapSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMinLargeHeapSize((Config*) p, size);
    }

    public static void SetMinSmallHeapSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMinSmallHeapSize((Config*) p, size);
    }

    public static void SetOverrideRamSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetOverrideRamSize((Config*) p, size);
    }

    public static void SetPageCacheSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetPageCacheSize((Config*) p, size);
    }

    public static void SetRecycleDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetRecycleDelay((Config*) p, delay);
    }

    public static void SetResourcePath(in this Config _, String* resourcePath) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetResourcePath((Config*) p, resourcePath);
    }

    public static void SetScrollTimerDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetScrollTimerDelay((Config*) p, delay);
    }

    public static void SetUseGpuRenderer(in this Config _, bool useGpu) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUseGpuRenderer((Config*) p, useGpu);
    }

    public static void SetUserAgent(in this Config _, String* agentString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUserAgent((Config*) p, agentString);
    }

    public static void SetUserStylesheet(in this Config _, String* cssString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUserStylesheet((Config*) p, cssString);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Config : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Config* Unsafe => _;

      internal readonly UltralightSharp.Config* _;

      public Config(UltralightSharp.Config* p)
        => _ = p;

      public Config()
        => _ = UltralightSharp.Config.Create();

      public void Dispose()
        => _->Destroy();

      public void SetAnimationTimerDelay(double delay) {
        _->SetAnimationTimerDelay(delay);
      }

      public void SetCachePath(string cachePath) {
        var s = String.Create(cachePath);
        _->SetCachePath(s);
        s->Destroy();
      }

      public void SetDeviceScale(double value) {
        _->SetDeviceScale(value);
      }

      public void SetEnableImages(bool enabled) {
        _->SetEnableImages(enabled);
      }

      public void SetEnableJavaScript(bool enabled) {
        _->SetEnableJavaScript(enabled);
      }

      public void SetFaceWinding(FaceWinding winding) {
        _->SetFaceWinding(winding);
      }

      public void SetFontFamilyFixed(string fontName) {
        var s = String.Create(fontName);
        _->SetFontFamilyFixed(s);
      }

      public void SetFontFamilySansSerif(string fontName) {
        var s = String.Create(fontName);
        _->SetFontFamilySansSerif(s);
        s->Destroy();
      }

      public void SetFontFamilySerif(string fontName) {
        var s = String.Create(fontName);
        _->SetFontFamilySerif(s);
        s->Destroy();
      }

      public void SetFontFamilyStandard(string fontName) {
        var s = String.Create(fontName);
        _->SetFontFamilyStandard(s);
        s->Destroy();
      }

      public void SetFontGamma(double fontGamma) {
        _->SetFontGamma(fontGamma);
      }

      public void SetFontHinting(FontHinting fontHinting) {
        _->SetFontHinting(fontHinting);
      }

      public void SetForceRepaint(bool enabled) {
        _->SetForceRepaint(enabled);
      }

      public void SetMemoryCacheSize(uint size) {
        _->SetMemoryCacheSize(size);
      }

      public void SetMinLargeHeapSize(uint size) {
        _->SetMinLargeHeapSize(size);
      }

      public void SetMinSmallHeapSize(uint size) {
        _->SetMinSmallHeapSize(size);
      }

      public void SetOverrideRamSize(uint size) {
        _->SetOverrideRamSize(size);
      }

      public void SetPageCacheSize(uint size) {
        _->SetPageCacheSize(size);
      }

      public void SetRecycleDelay(double delay) {
        _->SetRecycleDelay(delay);
      }

      public void SetResourcePath(string resourcePath) {
        var s = String.Create(resourcePath);
        _->SetResourcePath(s);
        s->Destroy();
      }

      public void SetScrollTimerDelay(double delay) {
        _->SetScrollTimerDelay(delay);
      }

      public void SetUseGpuRenderer(bool useGpu) {
        _->SetUseGpuRenderer(useGpu);
      }

      public void SetUserAgent(string agentString) {
        var s = String.Create(agentString);
        _->SetUserAgent(s);
        s->Destroy();
      }

      public void SetUserStylesheet(string cssString) {
        var s = String.Create(cssString);
        _->SetUserStylesheet(s);
        s->Destroy();
      }

    }

  }

}