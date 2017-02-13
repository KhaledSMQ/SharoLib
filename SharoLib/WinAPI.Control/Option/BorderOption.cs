using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Control
{
    public class BorderOption : IThemeTextOption
    {
        public Color BorderColor
        {
            get;
            set;
        }
        public int BorderSize
        {
            get;
            set;
        }
        public BorderOption(Color c, int size)
        {
            this.BorderColor = c;
            this.BorderSize = size;
        }
        internal override void Apply(ref DTTOPTS options)
        {
            options.dwFlags |= (DTTOPSFlags)34;
            options.crBorder = ColorTranslator.ToWin32(this.BorderColor);
            options.iBorderSize = this.BorderSize;
        }
    }
}
