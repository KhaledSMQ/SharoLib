using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
        public RECT(Rectangle rect)
        {
            this.left = rect.X;
            this.top = rect.Y;
            this.right = rect.Right;
            this.bottom = rect.Bottom;
        }


        public int left;
        public int top;
        public int right;
        public int bottom;

        public int Width
        {
            get
            {
                return Math.Abs(this.right - this.left);
            }
        }
        public int Height
        {
            get
            {
                return Math.Abs(this.bottom - this.top);
            }
        }

        public static implicit operator Rectangle(RECT rect)
        {
            return new Rectangle(rect.left, rect.top, rect.Width, rect.Height);
        }
        public static implicit operator RECT(Rectangle rect)
        {
            return new RECT() { left = rect.Left, right = rect.Right, top = rect.Top, bottom = rect.Bottom };
        }
    }
}
