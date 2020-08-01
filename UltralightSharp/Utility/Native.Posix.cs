// ReSharper disable RedundantUsingDirective
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ImpromptuNinjas.UltralightSharp {

  public static partial class Native {

#if !NETFRAMEWORK
#if NETSTANDARD1_1
    internal static class Posix {

      [DllImport("c", EntryPoint = "access")]
      private static extern unsafe int AccessCheck(sbyte* path, int mode);

      public static unsafe int AccessCheck(string path, int mode) {
        var pathBytes = Encoding.UTF8.GetBytes(path);
        fixed (byte* pPathBytes = &pathBytes[0])
          return AccessCheck((sbyte*)pPathBytes, mode);
      }

    }
#endif

    private static bool IsMusl() {
#if NETSTANDARD1_1
      var cpu = RuntimeInformation.ProcessArchitecture;
      switch (cpu) {
        case Architecture.X86:
          return Posix.AccessCheck("/lib/libc.musl-x86.so.1", 0) == 0;
        case Architecture.X64:
          return Posix.AccessCheck("/lib/libc.musl-x86_64.so.1", 0) == 0;
        case Architecture.Arm:
          return Posix.AccessCheck("/lib/libc.musl-armv7.so.1", 0) == 0;
        case Architecture.Arm64:
          return Posix.AccessCheck("/lib/libc.musl-aarch64.so.1", 0) == 0;
        default: throw new PlatformNotSupportedException(cpu.ToString());
      }
#else
      using (var proc = Process.GetCurrentProcess()) {
        foreach (ProcessModule mod in proc.Modules) {
          var fileName = mod.FileName;

          if (!fileName.Contains("libc"))
            continue;

          if (fileName.Contains("musl"))
            return true;

          break;
        }
      }

      return false;
#endif
    }

    private static string GetProcArchString() {
      var cpu = RuntimeInformation.ProcessArchitecture;
      switch (cpu) {
        case Architecture.X86:
          return "x86";
        case Architecture.X64:
          return "x64";
        case Architecture.Arm:
          return "arm";
        case Architecture.Arm64:
          return "arm64";
        default: throw new PlatformNotSupportedException(cpu.ToString());
      }
    }

#endif

  }

}