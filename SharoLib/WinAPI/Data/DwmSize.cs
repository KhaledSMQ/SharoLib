using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public struct DwmSize
    {
        public int Width;
        public int Height;
        public Size ToSize()
        {
            return new Size(this.Width, this.Height);
        }
    }
}
