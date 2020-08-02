using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

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

  namespace Safe {

    [PublicAPI]
    public sealed class App : IDisposable {

      internal readonly unsafe UltralightSharp.App* _;

      public unsafe App(UltralightSharp.App* p)
        => _ = p;

      public unsafe App(UltralightSharp.Settings* settings, UltralightSharp.Config* config)
        => _ = UltralightSharp.App.Create(settings, config);

      public unsafe App(Settings settings, UltralightSharp.Config* config)
        => _ = UltralightSharp.App.Create(settings._, config);

      public unsafe App(UltralightSharp.Settings* settings, Config config)
        => _ = UltralightSharp.App.Create(settings, config._);

      public unsafe App(Settings settings, Config config)
        => _ = UltralightSharp.App.Create(settings._, config._);

      public unsafe void Dispose()
        => _->Destroy();

      public unsafe void SetWindow(Window window)
        => _->SetWindow(window._);

      public unsafe Window GetWindow()
        => new Window(_->GetWindow());

      public unsafe bool IsRunning()
        => _->IsRunning();

      public unsafe void Quit()
        => _->Quit();

      public unsafe void Run()
        => _->Run();

      public unsafe Renderer GetRenderer()
        => new Renderer(_->GetRenderer());

      public unsafe Monitor GetMainMonitor()
        => new Monitor(_->GetMainMonitor());

    }

  }

}