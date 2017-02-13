using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI;
using WinAPI.Const;
using WinAPI.Data;
using WinAPI.Delegate;

namespace SharoLib.Manager
{ 
    /// <summary>
    /// 低レベルなマウスのグローバルフックです。使用する場合はコンパイルオプションのVisualStudioホストデバッグを無効化してください。
    /// </summary>
    public class MouseHook : IDisposable
    {
        private const int MouseLowLevelHook = 14;
        private MouseHookDelegate hookDelegate;
        private IntPtr hook;
        private bool disposed = false;
        private static readonly object EventMouseHooked = new object();
        
        public event MouseHookedEventHandler MouseStatusChanged;

        public MouseHook()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");
            this.hookDelegate = new MouseHookDelegate(CallNextHook);
            IntPtr module = Marshal.GetHINSTANCE(typeof(MouseHook).Assembly.GetModules()[0]);
            hook = NativeMethod.SetWindowsHookEx(GlobalHookTypes.Mouse_Global, hookDelegate, module, 0);
            // hook = NativeMethod.SetWindowsHookEx(GlobalHookTypes.Mouse_Global, hookDelegate, IntPtr.Zero, (uint)NativeMethod.GetCurrentThreadId());


            //NativeMethod.GetCurrentThreadId() 
        }
        public MouseHook(MouseHookedEventHandler handler)
            : this()
        {
            this.MouseStatusChanged += handler;
        }

        private void OnMouseHooked(MouseHookedEventArgs e)
        {
            if (this.MouseStatusChanged != null)
                this.MouseStatusChanged(this, e);
        }
        private int CallNextHook(int code, MouseMessage message, ref MOUSESTATE state)
        {
            if (code >= 0)
            {
                OnMouseHooked(new MouseHookedEventArgs(message, ref state));
            }
            return NativeMethod.CallNextHookEx(hook, code, message, ref state);
        }
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                NativeMethod.UnhookWindowsHookEx(hook);
                hook = IntPtr.Zero;
            }
        }
    }


    public delegate void MouseHookedEventHandler(object sender, MouseHookedEventArgs e);
    public class MouseHookedEventArgs : EventArgs
    {
        internal MouseHookedEventArgs(MouseMessage message, ref MOUSESTATE state) {
            this.message = message;
            this.state = state;
        }
        private MouseMessage message;
        private MOUSESTATE state;
        ///<summary>マウス操作の種類を表すMouseMessage値。</summary>
        public MouseMessage Message { get { return message; } }
        ///<summary>スクリーン座標における現在のマウスカーソルの位置。</summary>
        public Point Point { get { return state.Point; } }
        ///<summary>ホイールの情報を表すWheelData構造体。</summary>
        public WHEELDATA WheelData { get { return state.WheelData; } }
        ///<summary>XButtonの情報を表すXButtonData構造体。</summary>
        public XBUTTONDATA XButtonData { get { return state.XButtonData; } }
    }
}
