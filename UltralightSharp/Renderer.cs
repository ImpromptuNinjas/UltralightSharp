using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Renderer {

    public static Renderer* Create(Config* config)
      => Ultralight.CreateRenderer(config);

  }

  [PublicAPI]
  public static unsafe class RendererExtensions {

    public static void Destroy(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyRenderer((Renderer*) p);
    }

    public static Session* GetDefaultSession(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.DefaultSession((Renderer*) p);
    }

    public static void Update(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.Update((Renderer*) p);
    }

    public static void Render(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.Render((Renderer*) p);
    }

    public static void PurgeMemory(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.PurgeMemory((Renderer*) p);
    }

    public static void LogMemoryUsage(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.LogMemoryUsage((Renderer*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Renderer : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Renderer* Unsafe => _;

      internal readonly UltralightSharp.Renderer* _;

      private readonly bool _refOnly;

      public Renderer(UltralightSharp.Renderer* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public Renderer(UltralightSharp.Config* config)
        => _ = UltralightSharp.Renderer.Create(config);

      public Renderer(Config config)
        => _ = UltralightSharp.Renderer.Create(config._);

      public void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public Session GetDefaultSession()
        => new Session(_->GetDefaultSession());

      public void Update()
        => _->Update();

      public void Render()
        => _->Render();

      public void PurgeMemory()
        => _->PurgeMemory();

      public void LogMemoryUsage()
        => _->LogMemoryUsage();

    }

  }

}