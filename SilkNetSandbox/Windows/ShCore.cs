using System;
using System.Runtime.InteropServices;

namespace Windows
{

    internal static class ShCore
    {

        [DllImport("ShCore", SetLastError = true)]
        internal static extern bool SetProcessDpiAwareness(ProcessDpiAwareness awareness);

        [Flags]
        internal enum ProcessDpiAwareness
        {

            DpiUnaware = 0,

            SystemDpiAware = 1,

            PerMonitorDpiAware = 2

        }

    }

}
