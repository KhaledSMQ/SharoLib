using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public MARGINS(int all) { this.left = this.right = this.top = this.bottom = all; }
        public MARGINS(int left, int right, int top, int bottom) { this.left = left; this.right = right; this.top = top; this.bottom = bottom; }

        public int left;
        public int right;
        public int top;
        public int bottom;

        public int Width
        {
            get { return Math.Abs(this.left - this.right); }
        }
        public int Height
        {
            get { return Math.Abs(this.top - this.bottom); }
        }

        public static implicit operator Margins(MARGINS mrgn)
        {
            return new Margins(mrgn.left, mrgn.top, mrgn.Width, mrgn.Height);
        }
        public static implicit operator MARGINS(Margins mrgn)
        {
            return new MARGINS() { left = mrgn.Left, right = mrgn.Right, top = mrgn.Top, bottom = mrgn.Bottom };
        }
    }

}
