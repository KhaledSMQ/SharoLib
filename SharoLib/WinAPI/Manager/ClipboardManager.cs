using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAPI.Const;

namespace WinAPI.Manager
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ClipboardManager : NativeWindow, IDisposable
    {
        public ClipboardManager()
        {
            this.CreateHandle(new CreateParams());
            this.OnHandleCreated(null, null);
        }

        private IntPtr nextHandle;
        private bool _enabled = false;

        internal void OnHandleCreated(object sender, EventArgs e)
        {
            if (!_enabled)
            {
                // AssignHandle(((Form)sender).Handle);
                // ビューアを登録
                nextHandle = NativeMethod.SetClipboardViewer(this.Handle);
            }
        }
        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            if (_enabled)
            {
                // ビューアを解除
                bool sts = NativeMethod.ChangeClipboardChain(this.Handle, nextHandle);
            }
            ReleaseHandle();
        }

        public bool Enabled { get { return _enabled; } }
        public event EventHandler Change;

        protected override void WndProc(ref Message msg)
        {
            switch ((WM)msg.Msg)
            {
                case WM.WM_DRAWCLIPBOARD:
                    if (Change != null)
                    {
                        Change(this, EventArgs.Empty);
                    }

                    if ((int)nextHandle != 0)
                        NativeMethod.SendMessage(nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;

                // クリップボード・ビューア・チェーンが更新された
                case WM.WM_CHANGECBCHAIN:
                    if (msg.WParam == nextHandle)
                        nextHandle = (IntPtr)msg.LParam;
                    else if ((int)nextHandle != 0)
                        NativeMethod.SendMessage(nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;
            }
            base.WndProc(ref msg);
        }

        public void Dispose()
        {
            this.DestroyHandle();
        }
    }   
}
