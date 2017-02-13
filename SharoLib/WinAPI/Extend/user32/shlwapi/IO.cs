using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern bool PathCompactPath(IntPtr hDC, [In, Out] StringBuilder pszPath, int dx);

    }
}
