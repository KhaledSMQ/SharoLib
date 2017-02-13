using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Delegate
{
    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    /// <summary>マウスのグローバルフック用デリゲート。MessageDelegateでも可。</summary>
    public delegate int MouseHookDelegate(int code, MouseMessage message, ref MOUSESTATE state);
    /// <summary>キーボードのグローバルフック用デリゲート。MessageDelegateでも可</summary>
    public delegate int KeyboardHookDelegate(int code, KeyboardMessage message, ref KEYBOARDSTATE state);
    /// <summary>グローバルフック用デリゲート。なんでも使える汎用</summary>
    public delegate int MessageDelegate(int code, IntPtr wparam, IntPtr lparam);
}