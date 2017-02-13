using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAPI.Data
{
    public class HDC: IDisposable
    {
        // Initialize
        public HDC() { }
        private HDC(IntPtr ptr)
        { this.Handle = ptr; }

        // Variable
        private bool _autoDispose = true;

        // Base Property
        public IntPtr Handle
        { get; internal set; }

        // Cast
        public static implicit operator System.IntPtr(HDC p)
        {
            return p.Handle;
        }
        public static implicit operator HDC(System.IntPtr hWnd)
        {
            return new HDC(hWnd);
        }

        // Property
        public bool AutoDispose
        {
            get { return this._autoDispose; }
            set { this._autoDispose = value; }
        }

        // Dynamic Method
        public Graphics GetGraphics()
        {
            return Graphics.FromHdc(this);
        }
        public void Dispose()
        {
            if (this.AutoDispose)
            {
                NativeMethod.ReleaseDC(IntPtr.Zero, this);
            }
        }

        // Static Method
        public static HDC GetPrimaryMonitorHDC()
        {
            return NativeMethod.GetDC(IntPtr.Zero);
        }
        public static HDC GetScreenHDC(Screen scr)
        {
            return NativeMethod.CreateDC(scr.DeviceName, null, null, IntPtr.Zero);
        }
    }
}
