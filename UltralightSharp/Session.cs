using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct Session {

    public static unsafe Session* Create(Renderer* renderer, bool isPersistent, String* name)
      => Ultralight.CreateSession(renderer, isPersistent, name);

  }

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

}