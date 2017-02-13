using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public UInt32 cbSize;
        public IntPtr hWnd;
        public UInt32 uCallbackMessage;
        public UInt32 uEdge;
        public RECT rc;
        public Int32 lParam;
    }
}
