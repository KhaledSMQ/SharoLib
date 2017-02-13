using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_BLURBEHIND
    {
        public DWM_BB dwFlags;
        public bool fEnable;
        public IntPtr hRgnBlur;
        public bool fTransitionOnMaximized;

        public DWM_BLURBEHIND(bool enabled)
        {
            fEnable = enabled;
            hRgnBlur = IntPtr.Zero;
            fTransitionOnMaximized = false;
            dwFlags = DWM_BB.Enable;
        }

        public System.Drawing.Region Region
        {
            get { return System.Drawing.Region.FromHrgn(hRgnBlur); }
        }

        public bool TransitionOnMaximized
        {
            get { return fTransitionOnMaximized; }
            set
            {
                fTransitionOnMaximized = value;
                dwFlags |= DWM_BB.TransitionMaximized;
            }
        }

        public void SetRegion(System.Drawing.Graphics graphics, System.Drawing.Region region)
        {
            hRgnBlur = region.GetHrgn(graphics);
            dwFlags |= DWM_BB.BlurRegion;
        }
    }


}
