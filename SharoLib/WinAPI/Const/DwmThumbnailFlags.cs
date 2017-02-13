using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum DwmThumbnailFlags
    {
        RectDestination = 1,
        RectSource = 2,
        Opacity = 4,
        Visible = 8,
        SourceClientAreaOnly = 16
    }
}
