using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);
        [DllImport("uxtheme.dll")]
        public static extern int BufferedPaintSetAlpha(IntPtr hBufferedPaint, [In] ref Rectangle prc, byte alpha);
        [DllImport("uxtheme.dll")]
        public static extern int EndBufferedPaint(IntPtr hBufferedPaint, bool fUpdateTarget);
        [DllImport("uxtheme.dll", SetLastError = true)]
        public static extern IntPtr BeginBufferedPaint(IntPtr hdcTarget, [In] ref Rectangle prcTarget, BP_BUFFERFORMAT dwFormat, [In] ref BP_PAINTPARAMS pPaintParams, out IntPtr phdc);

    }
}
