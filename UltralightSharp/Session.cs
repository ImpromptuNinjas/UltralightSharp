using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Session {

    public static unsafe Session* Create(Renderer* renderer, bool isPersistent, String* name)
      => Ultralight.CreateSession(renderer, isPersistent, name);

  }

  [PublicAPI]
  public static class SessionExtensions {

    public static unsafe void Destroy(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroySession((Session*) p);
    }

    public static unsafe ulong GetId(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetId((Session*) p);
    }

    public static unsafe String* GetName(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetName((Session*) p);
    }

    public static unsafe String* GetDiskPath(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetDiskPath((Session*) p);
    }

    public static unsafe bool IsPersistent(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionIsPersistent((Session*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Session : IDisposable {

      public unsafe UltralightSharp.Session* Unsafe => _;

      internal readonly unsafe UltralightSharp.Session* _;

      private readonly bool _refOnly;

      public unsafe Session(UltralightSharp.Session* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public unsafe Session(UltralightSharp.Renderer* renderer, bool isPersistent, String* name)
        => _ = UltralightSharp.Session.Create(renderer, isPersistent, name);

      public unsafe Session(UltralightSharp.Renderer* renderer, bool isPersistent, string name) {
        var str = String.Create(name);
        _ = UltralightSharp.Session.Create(renderer, isPersistent, str);
        str->Destroy();
      }

      public unsafe Session(Renderer renderer, bool isPersistent, String* name)
        => _ = UltralightSharp.Session.Create(renderer._, isPersistent, name);

      public unsafe Session(Renderer renderer, bool isPersistent, string name) {
        var str = String.Create(name);
        _ = UltralightSharp.Session.Create(renderer._, isPersistent, str);
        str->Destroy();
      }

      public unsafe void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public unsafe ulong GetId()
        => _->GetId();

      public unsafe string? GetName()
        => _->GetName()->Read();

      public unsafe string? GetDiskPath()
        => _->GetDiskPath()->Read();

      public unsafe bool IsPersistent()
        => _->IsPersistent();

    }

  }

}