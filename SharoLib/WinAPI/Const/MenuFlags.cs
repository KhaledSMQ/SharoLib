using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Const
{
    [Flags]
    public enum MenuFlags : uint
    {
        MF_STRING = 0,
        MF_BYPOSITION = 0x400,
        MF_SEPARATOR = 0x800,
        MF_REMOVE = 0x1000,
    }

}
