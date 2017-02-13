using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI
{
    public partial class NativeMethod
    {
        // Mouse
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetCursorPos(int X, int Y);
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        // Touch
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterTouchWindow(IntPtr hWnd, TWF ulFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterTouchWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TOUCHINPUT[] pInputs, int cbSize);
        [DllImport("User32.dll")]
        public static extern bool InitializeTouchInjection(uint maxCount, TouchFeedback dwMode);
        [DllImport("User32.dll")]
        public static extern bool InjectTouchInput(uint count, [MarshalAs(UnmanagedType.LPArray), In] PointerTouchInfo[] contacts);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsTouchWindow(IntPtr hwnd, out TWF flags);

    }
}
