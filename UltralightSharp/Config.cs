using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct Config {

    public static unsafe Config* Create()
      => Ultralight.CreateConfig();

  }

  [PublicAPI]
  public static class ConfigExtensions {

    public static unsafe void Destroy(in this Config _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyConfig((Config*) p);
    }

    public static unsafe void SetAnimationTimerDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetAnimationTimerDelay((Config*) p, delay);
    }

    public static unsafe void SetCachePath(in this Config _, String* cachePath) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetCachePath((Config*) p, cachePath);
    }

    public static unsafe void SetDeviceScale(in this Config _, double value) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetDeviceScale((Config*) p, value);
    }

    public static unsafe void SetEnableImages(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetEnableImages((Config*) p, enabled);
    }

    public static unsafe void SetEnableJavaScript(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetEnableJavaScript((Config*) p, enabled);
    }

    public static unsafe void SetFaceWinding(in this Config _, FaceWinding winding) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFaceWinding((Config*) p, winding);
    }

    public static unsafe void SetFontFamilyFixed(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilyFixed((Config*) p, fontName);
    }

    public static unsafe void SetFontFamilySansSerif(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilySansSerif((Config*) p, fontName);
    }

    public static unsafe void SetFontFamilySerif(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilySerif((Config*) p, fontName);
    }

    public static unsafe void SetFontFamilyStandard(in this Config _, String* fontName) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontFamilyStandard((Config*) p, fontName);
    }

    public static unsafe void SetFontGamma(in this Config _, double fontGamma) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontGamma((Config*) p, fontGamma);
    }

    public static unsafe void SetFontHinting(in this Config _, FontHinting fontHinting) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetFontHinting((Config*) p, fontHinting);
    }

    public static unsafe void SetForceRepaint(in this Config _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetForceRepaint((Config*) p, enabled);
    }

    public static unsafe void SetMemoryCacheSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMemoryCacheSize((Config*) p, size);
    }

    public static unsafe void SetMinLargeHeapSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMinLargeHeapSize((Config*) p, size);
    }

    public static unsafe void SetMinSmallHeapSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetMinSmallHeapSize((Config*) p, size);
    }

    public static unsafe void SetOverrideRAMSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetOverrideRAMSize((Config*) p, size);
    }

    public static unsafe void SetPageCacheSize(in this Config _, uint size) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetPageCacheSize((Config*) p, size);
    }

    public static unsafe void SetRecycleDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetRecycleDelay((Config*) p, delay);
    }

    public static unsafe void SetResourcePath(in this Config _, String* resourcePath) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetResourcePath((Config*) p, resourcePath);
    }

    public static unsafe void SetScrollTimerDelay(in this Config _, double delay) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetScrollTimerDelay((Config*) p, delay);
    }

    public static unsafe void SetUseGpuRenderer(in this Config _, bool useGpu) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUseGpuRenderer((Config*) p, useGpu);
    }

    public static unsafe void SetUserAgent(in this Config _, String* agentString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUserAgent((Config*) p, agentString);
    }

    public static unsafe void SetUserStylesheet(in this Config _, String* cssString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ConfigSetUserStylesheet((Config*) p, cssString);
    }

  }

}