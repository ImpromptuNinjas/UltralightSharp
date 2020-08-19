using System;
using System.ComponentModel;
using System.Text;
using ImpromptuNinjas.UltralightSharp.Enums;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly unsafe ref struct Window {

    public static Window* Create(Monitor* monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
      => AppCore.CreateWindow(monitor, width, height, fullscreen, windowFlags);

  }

  [PublicAPI]
  public static unsafe class WindowExtensions {

    public static void Destroy(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.DestroyWindow((Window*) p);
    }

    public static void Close(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      AppCore.WindowClose((Window*) p);
    }

    public static int DeviceToPixel(this in Window _, int val) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowDeviceToPixel((Window*) p, val);
    }

    public static void* GetNativeHandle(this in Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetNativeHandle((Window*) p);
    }

    public static uint GetHeight(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetHeight((Window*) p);
    }

    public static uint GetWidth(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetWidth((Window*) p);
    }

    public static double GetScale(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowGetScale((Window*) p);
    }

    public static bool IsFullscreen(in this Window _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      return AppCore.WindowIsFullscreen((Window*) p);
    }

    public static void SetTitle(this in Window _, ReadOnlySpan<byte> title) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      fixed (byte* pTitle = title)
        AppCore.WindowSetTitle((Window*) p, (sbyte*) pTitle);
    }

    public static void SetTitle(this in Window _, string title) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      var bytes = Encoding.UTF8.GetBytes(title);
      fixed (byte* pBytes = bytes)
        AppCore.WindowSetTitle((Window*) p, (sbyte*) pBytes);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class Window : IDisposable {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.Window* Unsafe => _;

      internal readonly UltralightSharp.Window* _;

      private readonly bool _refOnly;

      public Window(UltralightSharp.Window* p, bool refOnly = true) {
        _ = p;
        _refOnly = refOnly;
      }

      public Window(UltralightSharp.Monitor* monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
        => _ = UltralightSharp.Window.Create(monitor, width, height, fullscreen, windowFlags);

      public Window(Monitor monitor, uint width, uint height, bool fullscreen, WindowFlags windowFlags)
        => _ = UltralightSharp.Window.Create(monitor._, width, height, fullscreen, windowFlags);

      public void Dispose() {
        if (!_refOnly) _->Destroy();
      }

      public void Close()
        => _->Close();

      public int DeviceToPixel(int val)
        => _->DeviceToPixel(val);

      public void* GetNativeHandleUnsafe()
        => _->GetNativeHandle();

      public IntPtr GetNativeHandle()
        => (IntPtr) _->GetNativeHandle();

      public uint GetHeight()
        => _->GetHeight();

      public uint GetWidth()
        => _->GetWidth();

      public double GetScale()
        => _->GetScale();

      public bool IsFullscreen()
        => _->IsFullscreen();

      public void SetTitle(string title)
        => _->SetTitle(title);

    }

  }

}