using System;
using UnityEngine;

public static class ExecutableServices {

  public static void RestoreNuGetPackages() {
    if (Environment.ExitCode != 0)
      //Environment.Exit(Environment.ExitCode);
      Application.Quit(Environment.ExitCode);

    try {
      NugetForUnity.NugetHelper.Restore();
    }
    catch {
      Environment.ExitCode = 1;
    }

    //Environment.Exit(Environment.ExitCode);
    Application.Quit(Environment.ExitCode);
  }

}