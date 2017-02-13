using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAPI.Control
{
    [ToolboxBitmap(typeof(Panel))]
    public class HorizontalPanel : Panel
    {
        private bool _renderOnGlass;
        [Category("Appearance"), DefaultValue(false)]
        public bool RenderOnGlass
        {
            get
            {
                return this._renderOnGlass;
            }
            set
            {
                this._renderOnGlass = value;
            }
        }
        public HorizontalPanel()
        {
            this.BackColor = Color.Transparent;
            this.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is LinkLabel)
            {
                e.Control.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
                ((LinkLabel)e.Control).LinkBehavior = LinkBehavior.HoverUnderline;
                ((LinkLabel)e.Control).LinkColor = Color.FromArgb(64, 64, 64);
                ((LinkLabel)e.Control).ActiveLinkColor = Color.Blue;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color aeroColor = Color.FromArgb(204, 217, 234);
            Color aeroColor2 = Color.FromArgb(217, 227, 240);
            Color aeroColor3 = Color.FromArgb(232, 238, 247);
            Color aeroColor4 = Color.FromArgb(237, 242, 249);
            Color aeroColor5 = Color.FromArgb(240, 244, 250);
            Color aeroColor6 = Color.FromArgb(241, 245, 251);
            Rectangle rect = new Rectangle(0, 0, base.Width, 1);
            SolidBrush sb = new SolidBrush(aeroColor);
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 1, base.Width, 1);
            sb.Color = aeroColor2;
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 2, base.Width, 1);
            sb.Color = aeroColor3;
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 3, base.Width, 1);
            sb.Color = aeroColor3;
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 4, base.Width, 1);
            sb.Color = aeroColor4;
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 5, base.Width, 1);
            sb.Color = aeroColor5;
            e.Graphics.FillRectangle(sb, rect);
            rect = new Rectangle(0, 6, base.Width, base.Height - 5);
            sb.Color = aeroColor6;
            e.Graphics.FillRectangle(sb, rect);
            sb.Dispose();
        }
        public void RedrawControlAsBitmap(IntPtr hwnd)
        {
            global::System.Windows.Forms.Control c = global::System.Windows.Forms.Control.FromHandle(hwnd);
            using (Bitmap bm = new Bitmap(c.Width, c.Height))
            {
                c.DrawToBitmap(bm, c.ClientRectangle);
                using (Graphics g = c.CreateGraphics())
                {
                    Point p = new Point(-1, -1);
                    g.DrawImage(bm, p);
                }
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            int WM_PAINT = 15;
            if (m.Msg == WM_PAINT && this.RenderOnGlass)
            {
                this.RedrawControlAsBitmap(base.Handle);
            }
        }
    }
}
