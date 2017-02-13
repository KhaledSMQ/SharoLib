using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum MouseEventFlags : uint
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010,
        WHEEL = 0x00000800,
        XDOWN = 0x00000080,
        XUP = 0x00000100
    }

    //Use the values of this enum for the 'dwData' parameter
    //to specify an X button when using MouseEventFlags.XDOWN or
    //MouseEventFlags.XUP for the dwFlags parameter.
    public enum MouseEventDataXButtons : uint
    {
        XBUTTON1 = 0x00000001,
        XBUTTON2 = 0x00000002
    }
}
