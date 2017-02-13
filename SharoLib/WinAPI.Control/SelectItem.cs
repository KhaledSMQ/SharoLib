using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Control
{
    public class SelectItem : global::System.Windows.Forms.Control
    {
        public SelectItem()
        {
            this.MouseEnter += MouseEnterEvent;
            this.MouseLeave += MouseLeaveEvent;

            this.ControlAdded += ControlAddedEvent;
            this.ControlRemoved += ControlRemovedEvent;
        }
        

        public DrawOption None = DrawOption.None;
        public DrawOption Enter = DrawOption.Enter;
        public DrawOption Selected = DrawOption.Selected;

        int Flags { get; set; }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            DrawOption dp = null;

            if (!this.Focused && Flags == 0) // None
            {
                dp = this.None;
            }
            if (!this.Focused && Flags == 1) // Enterd
            {
                dp = this.Enter;
            }
            if (this.Focused) // Focused
            {
                dp = this.Selected;
            }

            Point[] border = new[] {
                new Point(1, 0),
                new Point(this.Width - 2, 0),
                new Point(this.Width - 1, 1),
                new Point(this.Width - 1, this.Height - 2),
                new Point(this.Width - 2, this.Height - 1),
                new Point(1, this.Height - 1),
                new Point(0, this.Height - 2),
                new Point(0, 1),
                new Point(1, 0),
            };

            using (SolidBrush sb = new SolidBrush(dp.Border))
                e.Graphics.FillRectangle(sb, 1, 1, this.Width - 2, this.Height - 2);
            using (Pen p = new Pen(dp.BackColor))
                e.Graphics.DrawLines(p, border);

            base.OnPaint(e);
        }

        private void MouseEnterEvent(object sender, EventArgs e)
        {
            Flags = 1;
            base.Refresh();
        }

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            Flags = 0;
            base.Refresh();
        }
        
        void ControlAddedEvent(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            e.Control.MouseEnter += MouseEnterEvent;
            e.Control.MouseLeave += MouseLeaveEvent;
        }

        void ControlRemovedEvent(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            e.Control.MouseEnter -= MouseEnterEvent;
            e.Control.MouseLeave -= MouseLeaveEvent;
        }

        public class DrawOption
        {
            public static readonly DrawOption None;
            public static readonly DrawOption Enter;
            public static readonly DrawOption Selected;

            static DrawOption()
            {
                None = new DrawOption()
                {
                    BackColor = Color.Transparent,
                    Border = Color.Transparent,
                };
                Enter = new DrawOption()
                {
                    BackColor = Color.FromArgb(180, 184, 214, 251),
                    Border = Color.FromArgb(184, 214, 251),
                };
                Selected = new DrawOption()
                {
                    BackColor = Color.FromArgb(180, 125, 162, 205),
                    Border = Color.FromArgb(125, 162, 205),
                };
            }

            public Color BackColor { get; set; }
            public Color Border { get; set; }
        }
    }
}
