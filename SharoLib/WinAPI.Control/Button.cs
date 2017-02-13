using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Control
{
    public class Button : global::System.Windows.Forms.Button
    {
        private IntPtr hDC = IntPtr.Zero;
        private IntPtr pb = IntPtr.Zero;
        private Rectangle rect = Rectangle.Empty;
        private IntPtr phdc = IntPtr.Zero;

        public Button()
        {
            base.FlatStyle = FlatStyle.System;

            hDC = Graphics.FromHwnd(this.Handle).GetHdc();
        }

        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
        
        private bool useicon = true;
        private Bitmap image_;
        private Icon icon_;
        private bool showshield_;
        [DefaultValue(null), Category("Appearance")]
        public new Bitmap Image
        {
            get
            {
                return this.image_;
            }
            set
            {
                this.image_ = value;
                if (value != null)
                {
                    this.useicon = false;
                    this.Icon = null;
                }
                this.SetShield(false);
                this.SetImage();
            }
        }
        [Category("Appearance"), DefaultValue(null)]
        public Icon Icon
        {
            get
            {
                return this.icon_;
            }
            set
            {
                this.icon_ = value;
                if (this.icon_ != null)
                {
                    this.useicon = true;
                }
                this.SetShield(false);
                this.SetImage();
            }
        }
        [Category("Appearance"), DefaultValue(false)]
        public bool ShowShield
        {
            get
            {
                return this.showshield_;
            }
            set
            {
                this.showshield_ = value;
                this.SetShield(value);
                if (!value)
                {
                    this.SetImage();
                }
            }
        }
        public void SetImage()
        {
            IntPtr iconhandle = IntPtr.Zero;
            if (!this.useicon)
            {
                if (this.image_ != null)
                {
                    iconhandle = this.image_.GetHicon();
                }
            }
            else
            {
                if (this.icon_ != null)
                {
                    iconhandle = this.Icon.Handle;
                }
            }
            NativeMethod.SendMessage(base.Handle, 247, 1, (int)iconhandle);
        }
        public void SetShield(bool Value)
        {
            NativeMethod.SendMessage(base.Handle, 5644, IntPtr.Zero, new IntPtr(this.showshield_ ? 1 : 0));
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0f)
            {
                BP_PAINTPARAMS d = new BP_PAINTPARAMS();
                pb = NativeMethod.BeginBufferedPaint(hDC, ref rect, BP_BUFFERFORMAT.TopDownDIB, ref d, out phdc);

                int res = NativeMethod.BufferedPaintSetAlpha(hDC, ref rect, 255);
                IntPtr r = NativeMethod.SendMessage(hDC, 0x318, hDC.ToInt32(), 4);
                res = NativeMethod.BufferedPaintSetAlpha(hDC, ref rect, 255);
                res = NativeMethod.EndBufferedPaint(hDC, true);

            }
        }
        protected override void Dispose(bool disposing)
        {
            //ReleaseDC(IntPtr.Zero, hDC);
            base.Dispose(disposing);
        }
    }
}
