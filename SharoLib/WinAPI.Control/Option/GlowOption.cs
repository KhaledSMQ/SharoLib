using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Control
{
    public class GlowOption : IThemeTextOption
    {
        public const int DefaultSize = 10;
        public const int Word2007Size = 15;
        public const int PreciseGlow = 2;
        public int Size
        {
            get;
            set;
        }
        public GlowOption(int size)
        {
            this.Size = size;
        }
        internal override void Apply(ref DTTOPTS options)
        {
            options.dwFlags |= DTTOPSFlags.DTT_GLOWSIZE;
            options.iGlowSize = this.Size;
        }
    }
}
