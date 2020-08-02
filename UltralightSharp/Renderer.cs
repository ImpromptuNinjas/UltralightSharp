using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Renderer {

    public static unsafe Renderer* Create(Config* config)
      => Ultralight.CreateRenderer(config);

  }

  [PublicAPI]
  public static class RendererExtensions {

    public static unsafe void Destroy(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyRenderer((Renderer*) p);
    }

    public static unsafe Session* GetDefaultSession(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.DefaultSession((Renderer*) p);
    }

    public static unsafe void Update(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.Update((Renderer*) p);
    }

    public static unsafe void Render(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.Render((Renderer*) p);
    }

    public static unsafe void PurgeMemory(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.PurgeMemory((Renderer*) p);
    }

    public static unsafe void LogMemoryUsage(in this Renderer _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.LogMemoryUsage((Renderer*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Renderer : IDisposable {

      internal readonly unsafe UltralightSharp.Renderer* _;

      private readonly bool _refOnly;

      public unsafe Renderer(UltralightSharp.Renderer* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public unsafe Renderer(UltralightSharp.Config* config)
        => _ = UltralightSharp.Renderer.Create(config);

      public unsafe Renderer(Config config)
        => _ = UltralightSharp.Renderer.Create(config._);

      public unsafe void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public unsafe Session GetDefaultSession()
        => new Session(_->GetDefaultSession());

      public unsafe void Update()
        => _->Update();

      public unsafe void Render()
        => _->Render();

      public unsafe void PurgeMemory()
        => _->PurgeMemory();

      public unsafe void LogMemoryUsage()
        => _->LogMemoryUsage();

    }

  }

}