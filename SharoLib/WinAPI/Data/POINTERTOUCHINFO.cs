using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PointerTouchInfo
    {
        public POINTER_INFO pointerInfo;
        public TOUCH_FLAGS touchFlags;
        public TouchMask touchMask;
        public RECT rcContact;
        public RECT rcContactRaw;
        public uint orientation;
        public uint pressure;
    }
}
