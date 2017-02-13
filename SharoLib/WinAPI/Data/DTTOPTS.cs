using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    public struct DTTOPTS
    {
        public int dwSize;
        public DTTOPSFlags dwFlags;
        public int crText;
        public int crBorder;
        public int crShadow;
        public int iTextShadowType;
        public POINT ptShadowOffset;
        public int iBorderSize;
        public int iFontPropId;
        public int iColorPropId;
        public int iStateId;
        public bool fApplyOverlay;
        public int iGlowSize;
        public int pfnDrawTextCallback;
        public IntPtr lParam;
    }
}
