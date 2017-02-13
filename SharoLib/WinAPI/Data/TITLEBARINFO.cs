using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TITLEBARINFO
    {
        public const int CCHILDREN_TITLEBAR = 5;
        public uint cbSize;
        public RECT rcTitleBar;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1)]
        public uint[] rgstate;
    }
}
