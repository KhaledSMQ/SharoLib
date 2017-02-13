using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum BPPF : uint
    {
        Erase = 1,
        NoClip = 2,
        NonClient = 4
    }
}
