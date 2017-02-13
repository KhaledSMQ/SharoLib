using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum MK : int
    {
        Control = 0x0008,
        LeftButton = 0x0001,
        MiddleButton = 0x0010,
        RightButton = 0x0002,
        Shift = 0x0004,
        XButton1 = 0x0020,
        XButton2 = 0x0040,
    }
}
