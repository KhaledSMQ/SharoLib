using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CWP
    {
        public IntPtr lparam;
        public IntPtr wparam;
        public WM message;
        public IntPtr hwnd;
    }
}
