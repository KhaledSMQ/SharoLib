using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Manager
{
    public class Touch : IDisposable
    {
        MessageWindow hwnd = null;
        private int _marge = 2;
        private bool _initialized = false;
        private List<PointerTouchInfo> _touches = new List<PointerTouchInfo>();

        public Touch () {
            this.hwnd = new MessageWindow();
            this.hwnd.TouchChanged += (sender, count, inputs) => {
                this.TouchChanged?.Invoke(sender, count, inputs);
            };
            this.hwnd.CreateHandle(new CreateParams() {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
            });
        }

        public event TouchChangedHandler TouchChanged;

        public int Marge
        {
            get { return this._marge; }
            set { this._marge = value; }
        }

        public static bool RegisterTouchWindow(IntPtr hwnd) {
            return NativeMethod.RegisterTouchWindow(hwnd, 0);
        }
        public void Dispose() {
            this.hwnd.DestroyHandle();
        }

        public void InitializeTouch() {
            NativeMethod.InitializeTouchInjection(10, TouchFeedback.None);
            this._initialized = true;
        }
        public PointerTouchInfo TouchDown(Point p) {
            if (this._touches.Count == 0 && !this._initialized)
                InitializeTouch();

            PointerTouchInfo res = (new Data.PointerTouchInfo() {
                pointerInfo = new Data.POINTER_INFO() {
                    pointerType = POINTER_INPUT_TYPE.PT_TOUCH,
                    pointerFlags = PointerFlags.Down | PointerFlags.InRange | PointerFlags.InContact,
                    ptPixelLocation = new Data.POINT() {
                        X = p.X,
                        Y = p.Y
                    },
                    pointerId = 0
                },
                touchFlags = TOUCH_FLAGS.TOUCH_FLAGS_NONE,
                orientation = 90,
                pressure = 32000,
                touchMask = TouchMask.ContactArea | TouchMask.Orientation | TouchMask.Pressure,
                rcContact = new RECT() {
                   left = p.X - _marge,
                   right = p.X + _marge,
                   top = p.Y - _marge,
                   bottom = p.Y + _marge,
                }
            });
            this._touches.Add(res);

            NativeMethod.InjectTouchInput((uint)this._touches.Count, this._touches.ToArray());
            return res;
        }
        public void TouchUpdate(PointerTouchInfo touch, Point p) {
            if (!this._touches.Contains(touch))
                throw new ArgumentException("touch");

            touch.pointerInfo.pointerFlags = PointerFlags.Update | PointerFlags.InRange | PointerFlags.InContact;
            touch.pointerInfo.ptPixelLocation =
                new POINT() { X = p.X, Y = p.Y };
            NativeMethod.InjectTouchInput((uint)this._touches.Count, this._touches.ToArray());
        }
        public void TouchUp(PointerTouchInfo touch) {
            this.TouchUp(touch, Point.Empty);
        }
        public void TouchUp(PointerTouchInfo touch, Point p) {
            if (!this._touches.Contains(touch))
                throw new ArgumentException("touch");

            //touch.pointerInfo.pointerFlags = PointerFlags.Up | PointerFlags.InRange | PointerFlags.InContact;
            //touch.pointerInfo.ptPixelLocation =
            //    new POINT() { X = p.X, Y = p.Y };
            this._touches.Remove(touch);
            NativeMethod.InjectTouchInput((uint)this._touches.Count, this._touches.ToArray());

            if (this._touches.Count == 0)
                this._initialized = false;
        }

        public static TOUCHINPUT[] GetTouchInputFromParam(IntPtr wParam, IntPtr lParam) {

            int inputCount = (int)(wParam.ToInt32() & 0xFFFF);
            TOUCHINPUT[] inputs = new TOUCHINPUT[inputCount];
            bool result = NativeMethod.GetTouchInputInfo(lParam, inputCount, inputs, Marshal.SizeOf(inputs[0]));
            

            return inputs;
        }

        [System.Security.Permissions.PermissionSet(
            System.Security.Permissions.SecurityAction.Demand,
            Name = "FullTrust")]
        private class MessageWindow : NativeWindow
        {
            
            public event TouchChangedHandler TouchChanged;

            protected override void WndProc(ref Message m) {

                switch (m.Msg) {
                    case (int)WM.WM_TOUCH: {
                            int inputCount = (int)(m.WParam.ToInt32() & 0xFFFF);
                            TOUCHINPUT[] inputs = new TOUCHINPUT[inputCount];
                            bool result = NativeMethod.GetTouchInputInfo(m.LParam, inputCount, inputs, Marshal.SizeOf(inputs[0]));

                            this.TouchChanged?.Invoke(this.Handle, inputCount, inputs);
                        }
                        break;
                }

                base.WndProc(ref m);
            }
        }
        public delegate void TouchChangedHandler(IntPtr sender, int count, TOUCHINPUT[] inputs);

    }
}
