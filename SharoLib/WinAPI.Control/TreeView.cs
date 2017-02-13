using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinAPI.Control
{
    [ToolboxBitmap(typeof(TreeView))]
    public class TreeView : System.Windows.Forms.TreeView
    {
        public TreeView()
        {
            base.HotTracking = true;
            base.ShowLines = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 32768;
                return cp;
            }
        }
        [Browsable(false)]
        private new bool HotTracking
        {
            get
            {
                return base.HotTracking;
            }
            set
            {
                base.HotTracking = true;
            }
        }
        [Browsable(false)]
        private new bool ShowLines
        {
            get
            {
                return base.ShowLines;
            }
            set
            {
                base.ShowLines = false;
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e); 

            NativeMethod.SetWindowTheme(base.Handle, "explorer", null);
            int style = NativeMethod.SendMessage(base.Handle, 4397, 0, 0).ToInt32();
            style |= 100;
            NativeMethod.SendMessage(base.Handle, 4396, 0, style);
        }
    }
}
