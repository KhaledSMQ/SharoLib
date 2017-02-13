using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinAPI;
using WinAPI.Const;
using WinAPI.Data;
using WinAPI.Delegate;

namespace WinAPI.Manager
{
    /// <summary>
    /// 低レベルなキーボードのグローバルフックです。使用する場合はコンパイルオプションのVisualStudioホストデバッグを無効化してください。
    /// </summary>
    public class KeyboardHook : IDisposable
    {
        private const int KeyboardHookType = 13;
        private GCHandle hookDelegate;
        private IntPtr hook;
        private static readonly object EventKeyboardHooked = new object();
        public event KeyboardHookedEventHandler KeyStatusChanged;
        public bool Enabled { get; private set; }
        
        private void OnKeyboardHooked(KeyboardHookedEventArgs e)
        {
            if (this.KeyStatusChanged != null)
                this.KeyStatusChanged(this, e);
        }

        public KeyboardHook()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");
            this.Start();
        }
        public KeyboardHook(KeyboardHookedEventHandler handler)
            : this()
        {
            this.KeyStatusChanged += handler;
        }

        public void Start()
        {
            if (!Enabled)
            {
                KeyboardHookDelegate callback = new KeyboardHookDelegate(CallNextHook);
                this.hookDelegate = GCHandle.Alloc(callback);
                IntPtr module = Marshal.GetHINSTANCE(typeof(KeyboardHook).Assembly.GetModules()[0]);
                this.hook = NativeMethod.SetWindowsHookEx(GlobalHookTypes.KeyBoard_Global, callback, module, 0);

                if (this.hook != IntPtr.Zero)
                    Enabled = true;
            }
        }
        public void Stop()
        {
            if (this.Enabled)
            {
                NativeMethod.UnhookWindowsHookEx(hook);
                this.hook = IntPtr.Zero;
                this.hookDelegate.Free();
            }
        }

        private int CallNextHook(int code, KeyboardMessage message, ref KEYBOARDSTATE state)
        {
            if (code >= 0)
            {
                KeyboardHookedEventArgs e = new KeyboardHookedEventArgs(message, ref state);
                OnKeyboardHooked(e);
                if (e.Cancel)
                {
                    return -1;
                }
            }
            return NativeMethod.CallNextHookEx(IntPtr.Zero, code, message, ref state);
        }

        public void Dispose()
        {
            this.Stop();
        }
    }


    public delegate void KeyboardHookedEventHandler(object sender, KeyboardHookedEventArgs e);
    public class KeyboardHookedEventArgs : CancelEventArgs
    {
        private KeyboardHookedEventArgs() { }
        internal KeyboardHookedEventArgs(KeyboardMessage message, ref KEYBOARDSTATE state) {
            this.message = message;
            this.state = state;
        }

        private KeyboardMessage message;
        private KEYBOARDSTATE state;
        public KeyboardUpDown Status
        {
            get
            {
                return (message == KeyboardMessage.KeyDown || message == KeyboardMessage.SysKeyDown) ?
                    KeyboardUpDown.Down : KeyboardUpDown.Up;
            }
        }
        public Keys KeyCode { get { return state.KeyCode; } }
        public int ScanCode { get { return state.ScanCode; } }
        ///<summary>操作されたキーがテンキーなどの拡張キーかどうかを表す値を取得する。</summary>
        public bool IsExtendedKey { get { return state.Flag.IsExtended; } }
        ///<summary>ALTキーが押されているかどうかを表す値を取得する。</summary>
        public bool AltDown { get { return state.Flag.AltDown; } }
    }
}
