using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum PointerFlags : uint
    {
        None = 0x00000000,
        New = 0x00000001,
        InRange = 0x00000002,
        InContact = 0x00000004,
        FirstButton = 0x00000010,
        SecondButton = 0x00000020,
        ThirdButton = 0x00000040,
        OtherButton = 0x00000080,
        Primary = 0x00000100,
        Confidence = 0x00000200,
        Cancelled = 0x00000400,
        Down = 0x00010000,
        Update = 0x00020000,
        Up = 0x00040000,
        Wheel = 0x00080000,
        HWheel = 0x00100000
    }
}
