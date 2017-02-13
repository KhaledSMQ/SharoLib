using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BP_PAINTPARAMS
    {
        public int cbSize;
        public BPPF dwFlags;
        public IntPtr prcExclude;
        public IntPtr pBlendFunction;
    }
}
