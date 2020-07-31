using InlineIL;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  public readonly ref struct View {

    public static unsafe View* Create(Renderer* renderer, uint width, uint height, bool transparent, Session* session)
      => Ultralight.CreateView(renderer, width, height, transparent, session);

  }

  public static class ViewExtensions {

    public static unsafe void Destroy(in this View _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyView((View*) p);
    }

    public static unsafe void SetFinishLoadingCallback(in this View _, FinishLoadingCallback callback, void* userData) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewSetFinishLoadingCallback((View*) p, callback, userData);
    }

    public static unsafe void LoadHtml(in this View _, String* htmlString) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadHtml((View*) p, htmlString);
    }

    public static unsafe void LoadUrl(in this View _, String* url) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.ViewLoadUrl((View*) p, url);
    }

  }

}