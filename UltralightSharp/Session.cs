using System;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Session {

    public static Session* Create(Renderer* renderer, bool isPersistent, String* name)
      => Ultralight.CreateSession(renderer, isPersistent, name);

  }

  [PublicAPI]
  public static unsafe class SessionExtensions {

    public static void Destroy(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroySession((Session*) p);
    }

    public static ulong GetId(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetId((Session*) p);
    }

    public static String* GetName(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetName((Session*) p);
    }

    public static String* GetDiskPath(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionGetDiskPath((Session*) p);
    }

    public static bool IsPersistent(in this Session _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return Ultralight.SessionIsPersistent((Session*) p);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Session : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Session* Unsafe => _;

      internal readonly UltralightSharp.Session* _;

      private readonly bool _refOnly;

      public Session(UltralightSharp.Session* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public Session(UltralightSharp.Renderer* renderer, bool isPersistent, String* name)
        => _ = UltralightSharp.Session.Create(renderer, isPersistent, name);

      public Session(UltralightSharp.Renderer* renderer, bool isPersistent, string name) {
        var str = String.Create(name);
        _ = UltralightSharp.Session.Create(renderer, isPersistent, str);
        str->Destroy();
      }

      public Session(Renderer renderer, bool isPersistent, String* name)
        => _ = UltralightSharp.Session.Create(renderer._, isPersistent, name);

      public Session(Renderer renderer, bool isPersistent, string name) {
        var str = String.Create(name);
        _ = UltralightSharp.Session.Create(renderer._, isPersistent, str);
        str->Destroy();
      }

      public void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public ulong GetId()
        => _->GetId();

      public string? GetName()
        => _->GetName()->Read();

      public string? GetDiskPath()
        => _->GetDiskPath()->Read();

      public bool IsPersistent()
        => _->IsPersistent();

    }

  }

}