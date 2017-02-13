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
    public class ShadowOption : IThemeTextOption
    {
        public enum ShadowType
        {
            None,
            Single,
            Continuous
        }
        public ShadowOption.ShadowType Type
        {
            get;
            set;
        }
        public Point Offset
        {
            get;
            set;
        }
        public Color Color
        {
            get;
            set;
        }
        public ShadowOption(Color c, Point offset, ShadowOption.ShadowType type)
        {
            this.Color = c;
            this.Offset = offset;
            this.Type = type;
        }
        internal override void Apply(ref DTTOPTS options)
        {
            options.dwFlags |= (DTTOPSFlags)28;
            options.crShadow = ColorTranslator.ToWin32(this.Color);
            options.ptShadowOffset = new POINT(this.Offset);
            switch (this.Type)
            {
                case ShadowOption.ShadowType.None:
                    {
                        options.iTextShadowType = 0;
                        return;
                    }
                case ShadowOption.ShadowType.Single:
                    {
                        options.iTextShadowType = 1;
                        return;
                    }
                case ShadowOption.ShadowType.Continuous:
                    {
                        options.iTextShadowType = 2;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }
    }
}
