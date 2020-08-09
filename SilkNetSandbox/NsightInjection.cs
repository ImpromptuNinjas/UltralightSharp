using System;
using System.Runtime.InteropServices;
using ImpromptuNinjas.UltralightSharp;

namespace Nvidia.Nsight.Injection {

  public enum NsightInjectionResult {

    Ok = 0,

    Failure = -1,

    InvalidArgument = -2,

    InjectionFailed = -3,

    AlreadyInjected = -4,

    NotInjected = -5,

    DriverStillLoaded = -6,

  }

  public enum NsightSku {

    Unknown,

    Public,

    Pro,

    Internal,

  }

  public unsafe partial struct NsightInjectionInstallationInfo {

    public NsightSku Sku;

    [NativeTypeName("uint16_t")]
    public ushort VersionMajor;

    [NativeTypeName("uint16_t")]
    public ushort VersionMinor;

    [NativeTypeName("uint16_t")]
    public ushort VersionPatch;

    [NativeTypeName("const NGFX_Injection_PathChar *")]
    public ushort* InstallationPath;

  }

  public enum NsightInjectionActivityType {

    Unknown,

    FrameDebugger,

    FrameProfiler,

    GenerateCppCapture,

    GpuTrace,

  }

  public unsafe partial struct NsightInjectionActivity {

    public NsightInjectionActivityType Type;

    [NativeTypeName("const char *")]
    public sbyte* Description;

  }

  public static unsafe partial class Nsight {

    [DllImport("NGFX_Injection", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NGFX_Injection_EnumerateInstallations", ExactSpelling = true)]
    public static extern NsightInjectionResult EnumerateInstallations([NativeTypeName("uint32_t *")] uint* pCount, [NativeTypeName("NGFX_Injection_InstallationInfo *")]
      NsightInjectionInstallationInfo* pInstallations);

    [DllImport("NGFX_Injection", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NGFX_Injection_EnumerateActivities", ExactSpelling = true)]
    public static extern NsightInjectionResult EnumerateActivities([NativeTypeName("const NGFX_Injection_InstallationInfo *")]
      NsightInjectionInstallationInfo* pInstallation, [NativeTypeName("uint32_t *")] uint* pCount, [NativeTypeName("NGFX_Injection_Activity *")]
      NsightInjectionActivity* pActivities);

    [DllImport("NGFX_Injection", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NGFX_Injection_InjectToProcess", ExactSpelling = true)]
    public static extern NsightInjectionResult InjectToProcess([NativeTypeName("const NGFX_Injection_InstallationInfo *")]
      NsightInjectionInstallationInfo* pInstallation, [NativeTypeName("const NGFX_Injection_Activity *")]
      NsightInjectionActivity* pActivity);

    [DllImport("NGFX_Injection", CallingConvention = CallingConvention.Cdecl, EntryPoint = "NGFX_Injection_ExecuteActivityCommand", ExactSpelling = true)]
    public static extern NsightInjectionResult ExecuteActivityCommand();

    [NativeTypeName("#define NGFX_Injection_API_VersionMajor 0")]
    public const int VersionMajor = 0;

    [NativeTypeName("#define NGFX_Injection_API_VersionMinor 7")]
    public const int VersionMinor = 7;

    [NativeTypeName("#define NGFX_Injection_API_VersionPatch 0")]
    public const int VersionPatch = 0;

    [NativeTypeName("#define NGFX_Injection_API_Version ((NGFX_Injection_API_VersionMajor*1000) + (NGFX_Injection_API_VersionMinor*10) + NGFX_Injection_API_VersionPatch)")]
    public const int Version = ((0 * 1000) + (7 * 10) + 0);

    [NativeTypeName("#define NGFX_Injection_API_VersionString NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionMajor) \".\" NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionMinor) \".\" NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionPatch)")]
    public static ReadOnlySpan<byte> NgfxInjectionApiVersionStringUtf8
      => new byte[] {0x30, 0x2E, 0x37, 0x2E, 0x30, 0x00};

    [NativeTypeName("#define NGFX_Injection_API_VersionString NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionMajor) \".\" NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionMinor) \".\" NGFX_Injection_API_MK_STR(NGFX_Injection_API_VersionPatch)")]
    public static string NgfxInjectionApiVersionStringUtf16
      => $"{VersionMajor}.{VersionMinor}.{VersionPatch}";

  }

}