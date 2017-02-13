using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum DWM_BB
    {
        Enable = 1,
        BlurRegion = 2,
        TransitionMaximized = 4
    }
}
