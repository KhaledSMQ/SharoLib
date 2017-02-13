using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Control.Option
{
    [Flags]
    public enum AppBarStates
    {
        AutoHide = 0x00000001,
        AlwaysOnTop = 0x00000002
    }
}
