using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinAPI.Manager
{
    public static class Cursor
    {
        
        public static Point Position
        {
            get { return global::System.Windows.Forms.Cursor.Position; }
            set { global::System.Windows.Forms.Cursor.Position = value; }
        }

        public static void Click(Point p, MouseButtons button) {
            Point previous = Cursor.Position;
            Cursor.Position = p;

            Cursor.MouseDown(button);
            Cursor.MouseUp(button);

            Cursor.Position = previous;
        }
        public static void MouseDown(MouseButtons button) {
            if (button == MouseButtons.Left)
                NativeMethod.mouse_event(Const.MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            if (button == MouseButtons.Right)
                NativeMethod.mouse_event(Const.MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
            if (button == MouseButtons.Middle)
                NativeMethod.mouse_event(Const.MouseEventFlags.MIDDLEDOWN, 0, 0, 0, 0);
        }
        public static void MouseUp(MouseButtons button) {
            if (button == MouseButtons.Left)
                NativeMethod.mouse_event(Const.MouseEventFlags.LEFTUP, 0, 0, 0, 0);
            if (button == MouseButtons.Right)
                NativeMethod.mouse_event(Const.MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
            if (button == MouseButtons.Middle)
                NativeMethod.mouse_event(Const.MouseEventFlags.MIDDLEUP, 0, 0, 0, 0);
        }
    }
}
