#if NETFRAMEWORK || NETSTANDARD

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ImpromptuNinjas.UltralightSharp {

  internal delegate IntPtr DllImportResolver(
    string libraryName,
    Assembly assembly,
    DllImportSearchPath? searchPath
  );

  internal abstract class NativeLibrary {

#if !NETFRAMEWORK
    private static readonly INativeLibraryLoader Loader
      = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? Kernel32.Instance
        : LibDl.Instance;
#else
    private static INativeLibraryLoader Loader
      => Kernel32.Instance;
#endif

#if !NETSTANDARD1_1
    private static readonly ConditionalWeakTable<Assembly, LinkedList<DllImportResolver>> Resolvers
      = new ConditionalWeakTable<Assembly, LinkedList<DllImportResolver>>();
#else
    private static readonly Dictionary<Assembly, LinkedList<DllImportResolver>> Resolvers
      = new Dictionary<Assembly, LinkedList<DllImportResolver>>();
#endif

    public static void SetDllImportResolver(Assembly assembly, DllImportResolver resolver) {
      lock (Resolvers)
        Resolvers.GetOrCreateValue(assembly).AddLast(resolver);
    }

    public static IntPtr GetExport(IntPtr handle, string name) {
      if (handle == default)
        throw new ArgumentNullException(nameof(handle));
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name));

      var export = Loader.GetExport(handle, name);
      if (export == default)
#if !NETSTANDARD1_4 && !NETSTANDARD1_1
        throw new EntryPointNotFoundException(name);
#else
        throw new TypeLoadException($"Entry point not found: {name}");
#endif

      return export;
    }

    public static IntPtr Load(string libraryName, Assembly assembly, DllImportSearchPath? searchPath = null) {
      if (string.IsNullOrEmpty(libraryName))
        throw new ArgumentNullException(nameof(libraryName));
      if (assembly == null)
        throw new ArgumentNullException(nameof(assembly));

      lock (Resolvers) {
        if (!Resolvers.TryGetValue(assembly, out var resolvers))
          return Load(libraryName);

        foreach (var resolver in resolvers) {
          var result = resolver(libraryName, assembly, searchPath);
          if (result != default)
            return result;
        }

        var loaded = Load(libraryName);
        if (loaded == default)
#if NETSTANDARD1_1
          throw new Exception(libraryName);
#else
          throw new InvalidProgramException(libraryName);
#endif

        return loaded;
      }
    }

    public static IntPtr Load(string libraryPath) {
      if (string.IsNullOrEmpty(libraryPath))
        throw new ArgumentNullException(nameof(libraryPath));

      var loaded = Loader.Load(libraryPath);
      if (loaded == default)
#if NETSTANDARD1_1
        throw new InvalidOperationException(libraryPath);
#else
        throw new InvalidProgramException(libraryPath);
#endif

      return loaded;
    }

    private interface INativeLibraryLoader {

      void Init();

      IntPtr Load(string libraryPath);

      IntPtr GetExport(IntPtr handle, string name);

    }

#if !NETFRAMEWORK

    private static class LibDl {

      static LibDl() {
        //Trace.TraceInformation($"LibDl initializing.");
        INativeLibraryLoader loader;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
          // delayed linkage means we can't check for dl2 safely
          loader = new LibDl1();
          loader.Init();
        }
        else {
          // not delayed linkage
          try {
            loader = new LibDl2();
            loader.Init();
          }
          catch {
            loader = new LibDl1();
            loader.Init();
          }
        }

        Instance = loader;
      }

      internal static readonly INativeLibraryLoader Instance;

    }

    private static unsafe string GetString(sbyte* err) {
      var strLen = new ReadOnlySpan<sbyte>(err, 32768).IndexOf((sbyte) 0);
      if (strLen == -1)
        throw new InvalidOperationException();

#if NETSTANDARD1_1
      var errStrBytes = new byte[strLen];
      fixed (byte* pErrStrBytes = errStrBytes)
        Unsafe.CopyBlockUnaligned(pErrStrBytes, err, (uint) strLen);
      var errStr = Encoding.UTF8.GetString(errStrBytes, 0, strLen);
#else
      var errStr = Encoding.UTF8.GetString((byte*) err, strLen);
#endif
      return errStr;
    }

    private sealed class LibDl1 : INativeLibraryLoader {

      // ReSharper disable once MemberHidesStaticFromOuterClass
      private const string LibName = "dl"; // can be libdl.so or libdl.dylib

      [DllImport(LibName, EntryPoint = "dlopen")]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      internal static extern IntPtr Load(string fileName, int flags);

      [DllImport(LibName, EntryPoint = "dlsym")]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      private static extern IntPtr GetExport(IntPtr handle, string symbol);

      [DllImport(LibName, EntryPoint = "dlerror")]
      internal static extern unsafe sbyte* GetLastError();

      unsafe IntPtr INativeLibraryLoader.Load(string libraryPath) {
        var lib = Load(libraryPath, 0x0002 /*RTLD_NOW*/);
        if (lib != default)
          return lib;

        var err = GetLastError();
        if (err == default)
          return default;

#if NETSTANDARD1_4 || NETSTANDARD1_1
        var errStr = GetString(err);
#else
        var errStr = new string(err);
#endif
        throw new InvalidOperationException(errStr);
      }

      IntPtr INativeLibraryLoader.GetExport(IntPtr handle, string name)
        => GetExport(handle, name);

      public void Init() {
      }

    }

    private sealed class LibDl2 : INativeLibraryLoader {

      // ReSharper disable once MemberHidesStaticFromOuterClass
      private const string LibName = "dl.so.2";

      [DllImport(LibName, EntryPoint = "dlopen")]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      internal static extern IntPtr Load(string fileName, int flags);

      [DllImport(LibName, EntryPoint = "dlsym")]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      private static extern IntPtr GetExport(IntPtr handle, string symbol);

      [DllImport(LibName, EntryPoint = "dlerror")]
      internal static extern unsafe sbyte* GetLastError();

      unsafe IntPtr INativeLibraryLoader.Load(string libraryPath) {
        var lib = Load(libraryPath, 0x0002 /*RTLD_NOW*/);
        if (lib != default)
          return lib;

        var err = GetLastError();
        if (err == default)
          return default;

#if NETSTANDARD1_4 || NETSTANDARD1_1
        var errStr = GetString(err);
#else
        var errStr = new string(err);
#endif
        throw new InvalidOperationException(errStr);
      }

      IntPtr INativeLibraryLoader.GetExport(IntPtr handle, string name)
        => GetExport(handle, name);

      public void Init() {
      }

    }

#endif

    private sealed class Kernel32 : INativeLibraryLoader {

      // ReSharper disable once MemberHidesStaticFromOuterClass
      private const string LibName = "kernel32";

      private Kernel32() {
      }

      internal static readonly INativeLibraryLoader Instance = new Kernel32();

      [DllImport(LibName, EntryPoint = "LoadLibrary", SetLastError = true)]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      private static extern IntPtr Load(string lpFileName);

      [DllImport(LibName, EntryPoint = "GetProcAddress")]
      // ReSharper disable once MemberHidesStaticFromOuterClass
      private static extern IntPtr GetExport(IntPtr handle, string procedureName);

      IntPtr INativeLibraryLoader.Load(string libraryPath) {
        var lib = Load(libraryPath);
        if (lib != default)
          return lib;

        var err = Marshal.GetLastWin32Error();
        if (err == default)
          return default;

        throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
      }

      IntPtr INativeLibraryLoader.GetExport(IntPtr handle, string name)
        => GetExport(handle, name);

      public void Init() {
      }

    }

  }

}

#endif