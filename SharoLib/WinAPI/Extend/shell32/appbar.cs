using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Data;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("shell32.dll")]
        public static extern UInt32 SHAppBarMessage(UInt32 dwMessage, ref APPBARDATA pData);

        [DllImport("user32.dll")]
        public static extern UInt32 RegisterWindowMessage([MarshalAs(UnmanagedType.LPTStr)]			String lpString);
	


    }
}
