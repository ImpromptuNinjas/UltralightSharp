using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct App {

    public static unsafe App* Create(Settings* settings, Config* config)
      => AppCore.CreateApp(settings, config);

  }

  [PublicAPI]
  public static class AppExtensions {

    public static unsafe void Destroy(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyApp((App*) p);
    }

    public static unsafe void SetWindow(in this App _, Window* window) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppSetWindow((App*) p, window);
    }

    public static unsafe Window* GetWindow(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetWindow((App*) p);
    }

    public static unsafe bool IsRunning(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppIsRunning((App*) p);
    }

    public static unsafe void Quit(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppQuit((App*) p);
    }

    public static unsafe void Run(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppRun((App*) p);
    }

    public static unsafe Renderer* GetRenderer(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetRenderer((App*) p);
    }

    public static unsafe Monitor* GetMainMonitor(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetMainMonitor((App*) p);
    }
  }

}