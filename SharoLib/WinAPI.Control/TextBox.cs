using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAPI.Const;

namespace WinAPI.Control
{
    [ToolboxBitmap(typeof(TextBox))]
    public class TextBox : System.Windows.Forms.TextBox
    {
        private string _cueBannerText = string.Empty;
        private bool _showCueFocused;
        private bool _aeroFix;


        public TextBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        }

        [Category("Appearance"), DefaultValue("")]
        public string CueBannerText
        {
            get
            {
                return this._cueBannerText;
            }
            set
            {
                this._cueBannerText = value;
                this.SetCueText(this.ShowCueFocused);
            }
        }
        [Browsable(false)]
        public new bool Multiline
        {
            get
            {
                return base.Multiline;
            }
            set
            {
                base.Multiline = false;
            }
        }
        [DefaultValue(false), Category("Appearance")]
        public bool ShowCueFocused
        {
            get
            {
                return this._showCueFocused;
            }
            set
            {
                this._showCueFocused = value;
                this.SetCueText(value);
            }
        }
        [DefaultValue(false), Category("Appearance")]
        public bool AeroFix
        {
            get { return _aeroFix; }
            set
            {
                if (_aeroFix != value)
                {
                    Invalidate();
                }

                _aeroFix = value;
            }
        }


        private void SetCueText(bool showFocus)
        {
            NativeMethod.SendMessage(base.Handle, 5377, new IntPtr(showFocus ? 1 : 0), this._cueBannerText);
        }
        private void RedrawAsBitmap()
        {
            using (Bitmap bm = new Bitmap(this.Width, this.Height))
            using (Graphics g = this.CreateGraphics())
            {
                
                this.DrawToBitmap(bm, this.ClientRectangle);
                bm.MakeTransparent();
                g.DrawImageUnscaled(bm, -1, -1);
            }
        }


        protected override void WndProc(ref Message m)
        {
            if (_aeroFix)
            {
                switch (m.Msg)
                {
                    case (int)WM.WM_PAINT:
                        RedrawAsBitmap();
                        m.Result = new IntPtr(1);
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            else
            {
                base.WndProc(ref m);
            }

        }
    }
}
