using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Settings {

    public static Settings* Create()
      => AppCore.CreateSettings();

  }

  [PublicAPI]
  public static unsafe class SettingsExtensions {

    public static void Destroy(in this Settings _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroySettings((Settings*) p);
    }

    public static void SetAppName(this in Settings _, String* name) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetAppName((Settings*) p, name);
    }

    public static void SetDeveloperName(this in Settings _, String* name) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetDeveloperName((Settings*) p, name);
    }

    public static void SetFileSystemPath(this in Settings _, String* path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetFileSystemPath((Settings*) p, path);
    }

    public static void SetForceCpuRenderer(this in Settings _, bool forceCpu) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetForceCpuRenderer((Settings*) p, forceCpu);
    }

    public static void SetLoadShadersFromFileSystem(this in Settings _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetLoadShadersFromFileSystem((Settings*) p, enabled);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Settings : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Settings* Unsafe => _;

      internal readonly UltralightSharp.Settings* _;

      public Settings(UltralightSharp.Settings* p)
        => _ = p;

      public Settings()
        => _ = UltralightSharp.Settings.Create();

      public void Dispose()
        => _->Destroy();

      public void SetAppNameUnsafe(String* name)
        => _->SetAppName(name);

      public void SetAppName(string name) {
        var s = String.Create(name);
        _->SetAppName(s);
        s->Destroy();
      }

      public void SetDeveloperNamehUnsafe(String* name)
        => _->SetDeveloperName(name);

      public void SetDeveloperName(string name) {
        var s = String.Create(name);
        _->SetDeveloperName(s);
        s->Destroy();
      }

      public void SetFileSystemPathUnsafe(String* path)
        => _->SetFileSystemPath(path);

      public void SetFileSystemPath(string path) {
        var s = String.Create(path);
        _->SetFileSystemPath(s);
        s->Destroy();
      }

      public void SetForceCpuRenderer(bool forceCpu)
        => _->SetForceCpuRenderer(forceCpu);

      public void SetLoadShadersFromFileSystem(bool enabled)
        => _->SetLoadShadersFromFileSystem(enabled);

    }

  }

}