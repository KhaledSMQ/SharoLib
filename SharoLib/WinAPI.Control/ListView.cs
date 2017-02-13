using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinAPI.Control
{
    /// <summary>
    /// Vista以上で追加された機能を含むListView
    /// </summary>
    [ToolboxBitmap(typeof(ListView))]
    public class ListView : System.Windows.Forms.ListView
    {
        private bool elv;
        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg == 15 && !this.elv)
            {
                NativeMethod.SetWindowTheme(base.Handle, "explorer", null);
                NativeMethod.SendMessage(base.Handle, 4150, 65536, 65536);
                this.elv = true;
            }
            base.WndProc(ref m);
        }
    }
}