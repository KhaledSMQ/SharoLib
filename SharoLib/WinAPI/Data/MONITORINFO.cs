using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MONITORINFO
    {
        public int Size;
        public RECT Monitor;
        public RECT WorkArea;
        public MonitorInfoFlag Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)StructSize.CCHDEVICENAME)]
        public string DeviceName;
        public void Init()
        {
            this.Size = 40 + 2 * (int)StructSize.CCHDEVICENAME;
            this.DeviceName = string.Empty;
        }
    }
}
