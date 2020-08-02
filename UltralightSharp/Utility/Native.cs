// ReSharper disable RedundantUsingDirective

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public static partial class Native {

    private static readonly Lazy<IntPtr> LazyLoadedLibUltralight
      = new Lazy<IntPtr>(
        () => LoadLib("Ultralight"),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<IntPtr> LazyLoadedLibAppCore
      = new Lazy<IntPtr>(
        () => LoadLib("AppCore"),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<IntPtr> LazyLoadedLibWebCore
      = new Lazy<IntPtr>(
        () => LoadLib("WebCore"),
        LazyThreadSafetyMode.ExecutionAndPublication);

    private static unsafe IntPtr LoadLib(string libName) {
      var asm = typeof(Native).GetAssembly();
      var baseDir = asm.GetLocalCodeBaseDirectory();

      var ptrBits = sizeof(void*) * 8;

      // ReSharper disable once RedundantAssignment
      IntPtr lib = default;

#if NETFRAMEWORK
      string libPath = Path.Combine(baseDir, $"{libName}.dll");
#else
      string libPath;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
        libPath = Path.Combine(baseDir, $"{libName}.dll");
        if (!TryLoad(libPath, out lib))
          libPath = Path.Combine(baseDir, "runtimes", ptrBits == 32 ? "win-x86" : "win-x64", "native", $"{libName}.dll");
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
        libPath = Path.Combine(baseDir, $"lib{libName}.dylib");
        if (!TryLoad(libPath, out lib))
          libPath = Path.Combine(baseDir, "runtimes", "osx-x64", "native", $"lib{libName}.dylib");
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
        libPath = Path.Combine(baseDir, $"lib{libName}.so");
        if (!TryLoad(libPath, out lib))
          libPath = Path.Combine(baseDir, "runtimes", $"{(IsMusl() ? "linux-musl-" : "linux-")}{GetProcArchString()}", "native", $"lib{libName}.so");
      }
      else throw new PlatformNotSupportedException();
#endif

      // ReSharper disable once InvertIf
      if (lib == default) {
        lib = NativeLibrary.Load(libPath);
        if (lib == default)
#if !NETFRAMEWORK
          throw new DllNotFoundException(libPath);
#else
          throw new FileNotFoundException(libPath + "\n" +
            $"You may need to specify <RuntimeIdentifier>{(ptrBits == 32 ? "win-x86" : "win-x64")}<RuntimeIdentifier> or <RuntimeIdentifier>win<RuntimeIdentifier> in your project file.",
            libPath);
#endif
      }

      return lib;
    }

    private static bool TryLoad(string libPath, out IntPtr lib) {
      try {
        lib = NativeLibrary.Load(libPath);
      }
#if !NETSTANDARD1_1 && !NETSTANDARD1_4
      catch (Exception ex) {
        Console.Error.WriteLine($"Library loading error: {libPath}\n{ex}");
#else
      catch {
#endif
        lib = default;
        return false;
      }

      return true;
    }

    public static IntPtr LibUltralight => LazyLoadedLibUltralight.Value;

    public static IntPtr LibAppCore => LazyLoadedLibAppCore.Value;

    public static IntPtr LibWebCore => LazyLoadedLibWebCore.Value;

    static Native()
      => NativeLibrary.SetDllImportResolver(typeof(Native).GetAssembly(),
        (name, assembly, path)
          => {
          switch (name) {
            case "Ultralight":
              Debug.Assert(LibUltralight != default);
              return LibUltralight;
            case "AppCore":
              Debug.Assert(LibAppCore != default);
              return LibAppCore;
            case "WebCoreCore":
              Debug.Assert(LibWebCore != default);
              return LibWebCore;
            default:
              return default;
          }
        });

    internal static void Init() {
      Debug.Assert(LibUltralight != default);
      Debug.Assert(LibAppCore != default);
      Debug.Assert(LibWebCore != default);
    }

  }

}