using System;
using System.Diagnostics;
using UnityEngine;

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

}