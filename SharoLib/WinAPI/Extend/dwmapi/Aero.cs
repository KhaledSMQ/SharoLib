using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr hwndDestination, IntPtr hwndSource, out Thumbnail phThumbnailId);
        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr hThumbnailId);
        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(Thumbnail hThumbnailId, ref DwmThumbnailProperties ptnProperties);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);
        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(Thumbnail hThumbnail, out DwmSize pSize);
        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWA dwAttribute, ref int pvAttribute, int cbAttribute);
    }
}
