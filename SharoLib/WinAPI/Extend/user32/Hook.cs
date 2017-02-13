using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinAPI.Const;
using WinAPI.Data;
using WinAPI.Delegate;

namespace WinAPI
{
    public partial class NativeMethod
    {
        // マウス
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(GlobalHookTypes hookType, MouseHookDelegate hookDelegate, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(GlobalHookTypes hookType, GCHandle hookDelegate, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int CallNextHookEx(IntPtr hook, int code, MouseMessage message, ref MOUSESTATE state);
        [DllImport("user32", EntryPoint = "CallNextHookEx")]
        public static extern int CallNextHook(IntPtr hHook, int ncode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32", EntryPoint = "CallNextHookEx")]
        public static extern int CallNextHook(IntPtr hHook, int ncode, IntPtr wParam, ref CWP lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hook);

        // キーボード
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(GlobalHookTypes hookType, KeyboardHookDelegate hookDelegate, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int CallNextHookEx(IntPtr hook, int code, KeyboardMessage message, ref KEYBOARDSTATE state);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int CallNextHookEx(IntPtr hook, int code, IntPtr message, IntPtr state);

        // グローバルフック
        [DllImport("user32", EntryPoint = "SetWindowsHookExA")]
        public static extern IntPtr SetWindowsHookEx(GlobalHookTypes idHook, MessageDelegate lpfn, IntPtr hmod, IntPtr dwThreadId);

        // ホットキー
        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);
        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr HWnd, int ID, MOD MOD_KEY, Keys KEY);
        [DllImport("user32.dll")]
        public static extern int UnregisterHotKey(IntPtr HWnd, int ID);

        // クリップボード
        [DllImport("user32")]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32")]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    }
}
