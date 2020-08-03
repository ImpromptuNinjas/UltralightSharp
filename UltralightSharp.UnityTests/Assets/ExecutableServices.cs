using System;
using System.Diagnostics;
using ImpromptuNinjas.UltralightSharp;
using JetBrains.Annotations;
using UnityEngine;

[PublicAPI]
public static class ExecutableServices {

  public static void RestoreNuGetPackages() {
    if (Environment.ExitCode != 0)
      //Environment.Exit(Environment.ExitCode);
      Application.Quit(Environment.ExitCode);

    UnityEngine.Debug.LogFormat("Entering RestoreNuGetPackages");

    try {
      NugetForUnity.NugetHelper.Restore();
    }
    catch (Exception ex) {
      UnityEngine.Debug.LogFormat($"Error in RestoreNuGetPackages: {ex}");
      Environment.ExitCode = 1;
    }

    UnityEngine.Debug.LogFormat("Exiting RestoreNuGetPackages");

    //Environment.Exit(Environment.ExitCode);
    Application.Quit(Environment.ExitCode);
  }

  public static void UltralightLibraryProbe() {
    if (Environment.ExitCode != 0)
      //Environment.Exit(Environment.ExitCode);
      Application.Quit(Environment.ExitCode);

    UnityEngine.Debug.LogFormat("Entering UltralightLibraryProbe");

    try {
      UnityEngine.Debug.LogFormat($"Ultralight v{Ultralight.VersionMajor()}.{Ultralight.VersionMinor()}.{Ultralight.VersionPatch()}");
    }
    catch (Exception ex) {
      UnityEngine.Debug.LogFormat($"Error in UltralightLibraryProbe: {ex}");
      Environment.ExitCode = 1;
    }

    UnityEngine.Debug.LogFormat("Exiting UltralightLibraryProbe");
    //Environment.Exit(Environment.ExitCode);
    Application.Quit(Environment.ExitCode);
  }

}