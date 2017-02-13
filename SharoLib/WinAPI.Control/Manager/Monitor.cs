/***********************************
 * 
 * WindowsAPI Monitor
 * * Create: 2014/05/30
 * * Creator: sharoron (Twitter: @sharo0331)
 * 
 * Require:
 *  gui32.cs
 *  gui32/monitor.cs
 * 
 **********************************/


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Data;

namespace WinAPI.Manager
{
    public class DisplayInfo
    {
        private DisplayInfo() { }

        public string Availability { get; set; }
        public string ScreenHeight { get; set; }
        public string ScreenWidth { get; set; }
        public RECT MonitorArea { get; set; }
        public RECT WorkArea { get; set; }

        public static DisplayInfoCollection GetDisplays()
        {
            DisplayInfoCollection col = new DisplayInfoCollection();

            NativeMethod.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                delegate
                    (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
                {
                    col.Add(GetInfo(hMonitor));

                    return true;
                }, IntPtr.Zero);
            return col;
        }
        public static DisplayInfo GetInfo(IntPtr mhnd)
        {
            MONITORINFO mi = new MONITORINFO();
            mi.Size = (int)Marshal.SizeOf(mi);
            bool success = NativeMethod.GetMonitorInfo(mhnd, ref mi);
            if (success)
            {
                DisplayInfo di = new DisplayInfo();
                di.ScreenWidth = (mi.Monitor.right - mi.Monitor.left).ToString();
                di.ScreenHeight = (mi.Monitor.bottom - mi.Monitor.top).ToString();
                di.MonitorArea = mi.Monitor;
                di.WorkArea = mi.WorkArea;
                di.Availability = mi.Flags.ToString();
                return di;
            }

            throw new Exception("Error");
        }

        public class DisplayInfoCollection : List<DisplayInfo>
        {
        }
    }
}
