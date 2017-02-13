using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Control
{
    public class OverlayOption : IThemeTextOption
    {
        public bool Enabled
        {
            get;
            set;
        }
        public OverlayOption(bool enabled)
        {
            this.Enabled = enabled;
        }
        internal override void Apply(ref DTTOPTS options)
        {
            options.dwFlags |= DTTOPSFlags.DTT_APPLYOVERLAY;
            options.fApplyOverlay = this.Enabled;
        }
    }
}
