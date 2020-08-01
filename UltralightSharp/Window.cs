using System;
using System.Text;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct Window {

    public static unsafe Window* Create(Monitor* monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
      => AppCore.CreateWindow(monitor, width, height, fullscreen, windowFlags);

  }

  [PublicAPI]
  public static class WindowExtensions {

    public static unsafe void Destroy(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyWindow((Window*) p);
    }

    public static unsafe void Close(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.WindowClose((Window*) p);
    }

    public static unsafe int DeviceToPixel(this in Window _, int val) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowDeviceToPixel((Window*) p, val);
    }

    public static unsafe void* GetNativeHandle(this in Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetNativeHandle((Window*) p);
    }

    public static unsafe uint GetHeight(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetHeight((Window*) p);
    }

    public static unsafe uint GetWidth(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetWidth((Window*) p);
    }

    public static unsafe double GetScale(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetScale((Window*) p);
    }

    public static unsafe bool IsFullscreen(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowIsFullscreen((Window*) p);
    }

    public static unsafe void SetTitle(this in Window _, ReadOnlySpan<byte> title) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      fixed (byte* pTitle = title)
        AppCore.WindowSetTitle((Window*) p, (sbyte*) pTitle);
    }

    public static unsafe void SetTitle(this in Window _, string title) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      var bytes = Encoding.UTF8.GetBytes(title);
      fixed (byte* pBytes = bytes)
        AppCore.WindowSetTitle((Window*) p, (sbyte*) pBytes);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed class Window : IDisposable {

      internal readonly unsafe UltralightSharp.Window* _;

      public unsafe Window(UltralightSharp.Window* p)
        => _ = p;

      public unsafe Window(UltralightSharp.Monitor* monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
        => _ = UltralightSharp.Window.Create(monitor, width, height, fullscreen, windowFlags);

      public unsafe Window(Monitor monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
        => _ = UltralightSharp.Window.Create(monitor._, width, height, fullscreen, windowFlags);

      public unsafe void Dispose()
        => _->Destroy();

      public unsafe void Close()
        => _->Close();

      public unsafe int DeviceToPixel(int val)
        => _->DeviceToPixel(val);

      public unsafe void* GetNativeHandleUnsafe()
        => _->GetNativeHandle();

      public unsafe IntPtr GetNativeHandle()
        => (IntPtr) _->GetNativeHandle();

      public unsafe uint GetHeight()
        => _->GetHeight();

      public unsafe uint GetWidth()
        => _->GetWidth();

      public unsafe double GetScale()
        => _->GetScale();

      public unsafe bool IsFullscreen()
        => _->IsFullscreen();

      public unsafe void SetTitle(string title)
        => _->SetTitle(title);

    }

  }

}