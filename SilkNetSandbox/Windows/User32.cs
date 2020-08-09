using System;
using System.Runtime.InteropServices;

namespace Windows
{

    internal static class User32
    {

        [DllImport("user32", SetLastError = true)]
        internal static extern bool SetProcessDpiAwarenessContext(DpiAwarenessContext dpiFlag);

        [DllImport("user32", SetLastError = true)]
        internal static extern bool SetProcessDPIAware();

        [Flags]
        internal enum DpiAwarenessContext
        {

            Unaware = 16,

            SystemAware = 17,

            PerMonitorAware = 18,

            PerMonitorAwareV2 = 34

        }


    }

}
