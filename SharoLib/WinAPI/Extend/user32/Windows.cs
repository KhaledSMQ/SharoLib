using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ExitWindowsEx(EWX uFlags, int dwReason);

    }
}
