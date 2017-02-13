using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public FlashWindow dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;

        public void SetSize()
        {
            this.cbSize = Convert.ToUInt32(Marshal.SizeOf(this));
        }
    }

}
