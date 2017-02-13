using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum DTTOPSFlags
    {
        DTT_TEXTCOLOR = 1,
        DTT_BORDERCOLOR,
        DTT_SHADOWCOLOR = 4,
        DTT_SHADOWTYPE = 8,
        DTT_SHADOWOFFSET = 16,
        DTT_BORDERSIZE = 32,
        DTT_APPLYOVERLAY = 1024,
        DTT_GLOWSIZE = 2048,
        DTT_COMPOSITED = 8192
    }
}
