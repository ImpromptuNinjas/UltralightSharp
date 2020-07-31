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

  }

}