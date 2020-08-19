using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct App {

    public static App* Create(Settings* settings, Config* config)
      => AppCore.CreateApp(settings, config);

  }

  [PublicAPI]
  public static unsafe class AppExtensions {

    public static void Destroy(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyApp((App*) p);
    }

    public static void SetWindow(in this App _, Window* window) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppSetWindow((App*) p, window);
    }

    public static Window* GetWindow(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetWindow((App*) p);
    }

    public static bool IsRunning(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppIsRunning((App*) p);
    }

    public static void Quit(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppQuit((App*) p);
    }

    public static void Run(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.AppRun((App*) p);
    }

    public static Renderer* GetRenderer(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetRenderer((App*) p);
    }

    public static Monitor* GetMainMonitor(in this App _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.AppGetMainMonitor((App*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class App : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public unsafe UltralightSharp.App* Unsafe => _;

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