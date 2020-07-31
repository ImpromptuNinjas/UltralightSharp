using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Settings {

    public static unsafe Settings* Create()
      => AppCore.CreateSettings();

  }

  [PublicAPI]
  public static class SettingsExtensions {

    public static unsafe void Destroy(in this Settings _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroySettings((Settings*) p);
    }

    public static unsafe void SetAppName(this in Settings _, String* name) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetAppName((Settings*) p, name);
    }

    public static unsafe void SetDeveloperName(this in Settings _, String* name) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetDeveloperName((Settings*) p, name);
    }

    public static unsafe void SetFileSystemPath(this in Settings _, String* path) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetFileSystemPath((Settings*) p, path);
    }

    public static unsafe void SetForceCpuRenderer(this in Settings _, bool forceCpu) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetForceCpuRenderer((Settings*) p, forceCpu);
    }

    public static unsafe void SetLoadShadersFromFileSystem(this in Settings _, bool enabled) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.SettingsSetLoadShadersFromFileSystem((Settings*) p, enabled);
    }

  }

}