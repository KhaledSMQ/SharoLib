using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Manager
{
    public static class SystemSupports
    {
        public static bool IsHigherVista
        {
            get
            {
                return Environment.OSVersion.Version.Major >= 6;
            }
        }
        public static bool IsAeroSupport
        {
            get
            {
                if (!SystemSupports.IsHigherVista)
                    return false;

                bool isGlassSupported = false;
                NativeMethod.DwmIsCompositionEnabled(out isGlassSupported);
                return isGlassSupported;
            }
        }
    }
}
