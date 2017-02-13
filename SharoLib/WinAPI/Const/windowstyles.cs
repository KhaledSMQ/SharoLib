using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Const
{
    [Flags()]
    public enum WindowStyles : uint
    {
        WS_BORDER = 0x800000u,
        WS_CAPTION = 0xc00000u,
        WS_CHILD = 0x40000000u,
        WS_CLIPCHILDREN = 0x2000000u,
        WS_CLIPSIBLINGS = 0x4000000u,
        WS_DISABLED = 0x8000000u,
        WS_DLGFRAME = 0x400000u,
        WS_GROUP = 0x20000u,
        WS_HSCROLL = 0x100000u,
        WS_MAXIMIZE = 0x1000000u,
        WS_MAXIMIZEBOX = 0x10000u,
        WS_MINIMIZE = 0x20000000u,
        WS_MINIMIZEBOX = 0x20000u,
        WS_OVERLAPPED = 0x0u,
        WS_OVERLAPPEDWINDOW = 0x0u | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = 0x80000000u,
        WS_POPUPWINDOW = 0x80000000u | WS_BORDER | WS_SYSMENU,
        WS_SIZEFRAME = 0x40000u,
        WS_SYSMENU = 0x80000u,
        WS_TABSTOP = 0x10000u,
        WS_VISIBLE = 0x10000000u,
        WS_VSCROLL = 0x200000u,
        NOACTIVATE = 0x8000000u,
    }
}
