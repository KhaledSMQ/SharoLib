using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WinAPI.Const;
using WinAPI.Delegate;
using WinAPI.Manager;

namespace WinAPI.Data
{
    public class HWND
    {
        public HWND(IntPtr hWnd)
        {
            this.Handle = hWnd;
        }
        public IntPtr Handle { get; private set; }

        public static implicit operator System.IntPtr(HWND p)
        {
            return p.Handle;
        }
        public static implicit operator HWND(System.IntPtr hWnd)
        {
            return new HWND(hWnd);
        }

        public Rectangle Rectangle
        {
            get
            {
                RECT rect = new RECT();
                NativeMethod.GetWindowRect(this.Handle, ref rect).CheckError();
                return rect;
            }
        }
        public Rectangle ClientRect
        {
            get
            {
                RECT rect = new RECT();
                NativeMethod.GetClientRect(this.Handle, out rect);
                return rect;
            }
        }
        public string Text
        {
            get
            {
                int length = NativeMethod.GetWindowTextLength(this.Handle);
                StringBuilder sb = new StringBuilder(length + 1);
                NativeMethod.GetWindowText(this.Handle, sb, sb.Capacity);
                return sb.ToString();
            }
            set
            {
                NativeMethod.SetWindowText(this.Handle, value);
            }
        }
        public bool Enabled
        {
            get
            {
                return NativeMethod.IsWindowEnabled(this.Handle);
            }
            set
            {
                NativeMethod.EnableWindow(this.Handle, value).CheckError();
            }
        }
        public bool Visibled
        {
            get
            {
                return NativeMethod.IsWindowVisible(this.Handle);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public bool Minimized
        {
            get
            {
                return NativeMethod.IsIconic(this.Handle);
            }
            set
            {
                if (value)
                    NativeMethod.OpenIcon(this.Handle);
                else
                    throw new NotImplementedException();
            }
        }
        public bool Maximized
        {
            get
            {
                return NativeMethod.IsZoomed(this.Handle);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public POINT Location
        {
            get
            {
                return new POINT(this.Rectangle.Top, this.Rectangle.Left);
            }
            set
            {
                NativeMethod.MoveWindow(this.Handle, value.X, value.Y, this.Size.Width, this.Size.Height, true).CheckError();
            }
        }
        public Size Size
        {
            get
            {
                return new Size(this.Rectangle.Width, this.Rectangle.Height);
            }
            set
            {
                Point p = this.Location;
                NativeMethod.MoveWindow(this.Handle, p.X, p.Y, value.Width, value.Height, true).CheckError();
            }
        }
        public bool IsDisposed
        {
            get
            {
                return !NativeMethod.IsWindow(this.Handle);
            }
        }
        public HWND[] Child
        {
            get
            {
                List<HWND> res = new List<HWND>();
                NativeMethod.EnumChildWindows(
                    this.Handle,
                    (hwnd, lParam) =>
                    {
                        res.Add(new HWND(hwnd));
                        return true;
                    },
                    IntPtr.Zero);
                return res.ToArray();
            }
        }
        public string ClassName
        {
            get
            {
                StringBuilder sb = new StringBuilder(256);
                if (NativeMethod.GetClassName(this.Handle, sb, sb.Capacity) == 0)
                    return "";
                else
                    return sb.ToString();
            }
        }
        public string Type
        {
            get
            {
                return Regex.Match(this.ClassName, "([a-zA-Z]+)").Value;
            }
        }
        public WNDCLASS ClassProperty
        {
            get
            {
                WNDCLASS res= new WNDCLASS();
                var result = NativeMethod.GetClassInfo(this.Handle, this.ClassName, out res);
                return res;
            }
        }
        public HWND NextWindow
        {
            get
            {
                return NativeMethod.GetWindow(this.Handle, GetWindow_Cmd.GW_HWNDNEXT);
            }
        }
        public HWND PrevWindow
        {
            get
            {
                return NativeMethod.GetWindow(this.Handle, GetWindow_Cmd.GW_HWNDPREV);
            }
        }
        public HWND Owner
        {
            get
            {
                return NativeMethod.GetWindow(this.Handle, GetWindow_Cmd.GW_OWNER);
            }
        }
        public HWND Parent
        {
            get
            {
                return NativeMethod.GetParent(this.Handle);
            }
            set
            {
                NativeMethod.SetParent(this, value);
            }
        }
        public TITLEBARINFO TitleBar
        {
            get
            {
                TITLEBARINFO res = new TITLEBARINFO();
                NativeMethod.GetTitleBarInfo(this.Handle, ref res);
                return res;
            }
        }
        public WINDOWINFO WindowInfo
        {
            get
            {
                WINDOWINFO info = new WINDOWINFO();
                info.cbSize = (uint)Marshal.SizeOf(info);
                NativeMethod.GetWindowInfo(Handle, ref info).CheckError();
                return info;
            }
        }
        public WINDOWPLACEMENT Placement
        {
            get
            {
                WINDOWPLACEMENT res = new WINDOWPLACEMENT();
                NativeMethod.GetWindowPlacement(this.Handle, ref res).CheckError();
                return res;
            }
        }
        public int Width
        {
            get
            {
                return this.Rectangle.Width;
            }
        }
        public int Heigth
        {
            get
            {
                return this.Rectangle.Height;
            }
        }
        public double Opacity
        {
            set
            {
                NativeMethod.SetLayeredWindowAttributes(this.Handle, (uint)Color.Transparent.ConvertColourToWindowsRGB(), (byte)(255 - value / (1 / 255)), LWA.LWA_ALPHA);
            }
        }
        public Color LayerColor
        {
            set
            {
                NativeMethod.SetLayeredWindowAttributes(this.Handle, (uint)value.ConvertColourToWindowsRGB(), 0, LWA.LWA_COLORKEY);
            }
        }
        public WindowStyles WindowLong
        {
            get
            {
                return (WindowStyles)NativeMethod.GetWindowLong(this.Handle, GWL.GWL_STYLE);
            }
            set
            {
                NativeMethod.SetWindowLong(this.Handle, GWL.GWL_STYLE, value);
            }
        }
        public bool IsTouchWindow
        {
            get {
                TWF twf = TWF.FineTouch;
                return NativeMethod.IsTouchWindow(this.Handle, out twf);
            }
            set
            {
                if (this.IsTouchWindow)
                    NativeMethod.UnregisterTouchWindow(this.Handle);
                else
                    NativeMethod.RegisterTouchWindow(this.Handle, TWF.FineTouch);
            }
        }

        /// <summary>アニメーション。時間は200msです</summary>
        public void Animate(AnimateWindowFlags type)
        {
            NativeMethod.AnimateWindow(this.Handle, 200, type).CheckError();
        }
        /// <summary>アニメーション</summary>
        public void Animate(AnimateWindowFlags type, int time)
        {
            NativeMethod.AnimateWindow(this.Handle, time, type).CheckError();
        }
        /// <summary>最前面に出す</summary>
        public void BringWindowToTop()
        {
            NativeMethod.BringWindowToTop(this.Handle).CheckError();
        }
        /// <summary>ウィンドウを最大化します</summary>
        public void Minimize()
        {
            NativeMethod.CloseWindow(this.Handle).CheckError();
        }
        /// <summary>ウィンドウを最小化します</summary>
        public void Maximize()
        {
            throw new NotImplementedException();
            NativeMethod.CloseWindow(this.Handle).CheckError();
        }
        /// <summary>ウィンドウを破棄/削除します</summary>
        public void Close()
        {
            NativeMethod.DestroyWindow(this.Handle).CheckError();
        }
        /// <summary>標準のウィンドウメッセージを処理させます</summary>
        public IntPtr DefaultMessageProc(WM uMsg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethod.DefWindowProc(this.Handle, uMsg, wParam, lParam);
        }
        /// <summary>Aero効果を無効化します</summary>
        public void DisabledAero()
        {
            DWM_BLURBEHIND bb = new DWM_BLURBEHIND();
            bb.dwFlags = DWM_BB.BlurRegion;
            bb.fEnable = false;
            
            NativeMethod.DwmEnableBlurBehindWindow(this, ref bb);
        }

        public HWND FindChildByCaption(string caption)
        {
            return NativeMethod.FindWindowEx(this.Handle, IntPtr.Zero, null, caption);
        }
        public HWND FindChildByClassName(string className)
        {
            return NativeMethod.FindWindowEx(this.Handle, IntPtr.Zero, className, null);
        }
        /// <summary>ウィンドウを1回だけ点滅させ、点灯状態にします。</summary>
        public void Flash()
        {
            NativeMethod.FlashWindow(this.Handle, true).CheckError();
        }
        /// <summary>ウィンドウを1回だけ点滅させます</summary>
        public void Flash(bool bInvert)
        {
            NativeMethod.FlashWindow(this.Handle, bInvert).CheckError();
        }
        /// <summary>ウィンドウを指定した回数点滅させます</summary>
        public void Flash(int count)
        {
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = this.Handle;
            fInfo.dwFlags = FlashWindow.FLASHW_ALL;
            fInfo.uCount = (uint)count;
            fInfo.dwTimeout = 0;

            NativeMethod.FlashWindowEx(ref fInfo).CheckError();

        }
        /// <summary>ウィンドウをアクティブにします</summary>
        public void Active()
        {
            NativeMethod.SetActiveWindow(this.Handle);
        }
        /// <summary>ウィンドウにフォーカスを当てます</summary>
        public void Focus()
        {
            NativeMethod.SetFocus(this.Handle);
        }
        /// <summary>ウィンドウを最前面にします</summary>
        public void Foreground()
        {
            NativeMethod.SetForegroundWindow(this.Handle);
        }
        /// <summary>閉じるボタンを無効化する</summary>
        public void DisableCloseButton()
        {
            IntPtr menu = NativeMethod.GetSystemMenu(this.Handle, false);
            int menuCount = NativeMethod.GetMenuItemCount(menu);
            if (menuCount > 1)
            {
                //メニューの「閉じる」とセパレータを削除
                NativeMethod.RemoveMenu(menu, (uint)(menuCount - 1), MenuFlags.MF_BYPOSITION | MenuFlags.MF_REMOVE);
                NativeMethod.RemoveMenu(menu, (uint)(menuCount - 2), MenuFlags.MF_BYPOSITION | MenuFlags.MF_REMOVE);
                NativeMethod.DrawMenuBar(this.Handle);
            }

        }
        /// <summary>メッセージを送信する</summary>
        public void SetAeroMargin(MARGINS marg)
        {
            if (SystemSupports.IsAeroSupport)
            {
                NativeMethod.DwmExtendFrameIntoClientArea(this, ref marg);
            }
        }
        public HWND GetChildWindow(Point p) {
            return NativeMethod.ChildWindowFromPoint(this, p);
        }
        public HWND GetChildWindowEx(Point p) {
            return NativeMethod.ChildWindowFromPointEx(this, p, WindowFromPointFlags.CWP_ALL);
        }
        public HWND GetTopChildWindow(Point p) {
            HWND res = this;
            Point pos = p;
            for (;;) {
                HWND next = res.GetChildWindow(pos);
                if (next.Handle == IntPtr.Zero || res.Handle == next.Handle)
                    break;
                pos = next.GetClientPositionFromScreenPos(res.GetScreenPosFromClientPos(p));
                res = next;                
            }
            return res;
        }
        public HWND GetTopChildWindowEx(Point p) {
            HWND res = this;
            Point pos = p;
            for (;;) {
                HWND next = res.GetChildWindowEx(pos);
                if (next.Handle == IntPtr.Zero || res.Handle == next.Handle)
                    break;
                pos = next.GetClientPositionFromScreenPos(res.GetScreenPosFromClientPos(p));
                res = next;
                Console.WriteLine("ChildWindow : {0}", next.Text);
            }
            return res;
        }
        public Point GetClientPositionFromScreenPos(Point p) {
            Point res = p;
            NativeMethod.ScreenToClient(this, ref res);
            return res;
        }
        public Point GetScreenPosFromClientPos(Point p) {
            Point res = p;
            NativeMethod.ClientToScreen(this, ref res);
            return res;
        }
        public int SendMessage(uint msg, IntPtr wParam, IntPtr lParam) {
            return NativeMethod.SendMessage(this.Handle, new IntPtr(msg), wParam, lParam).ToInt32();
        }
        public int SendMessage(uint msg, uint wParam, uint lParam) {
            return NativeMethod.SendMessage(this.Handle, (int)msg, wParam, lParam).ToInt32();
        }
        public int PostMessage(uint msg, IntPtr wParam, IntPtr lParam) {
            return NativeMethod.PostMessage(this.Handle, msg, wParam, lParam);

        }
        public int PostMessage(uint msg, uint wParam, uint lParam) {
            return NativeMethod.PostMessage(this.Handle, msg, wParam, lParam);

        }

        /// <summary>すべてのウィンドウを列挙します</summary>
        public static HWND[] GetWindows()
        {
            List<HWND> res = new List<HWND>();
            NativeMethod.EnumWindows(
                new EnumWindowsProc(
                    (hwnd, lParam) =>
                    {
                        res.Add(new HWND(hwnd));
                        return true;
                    }),
                IntPtr.Zero).CheckError();
            return res.ToArray();
        }
        /// <summary>キャプションからウィンドウを検索します。検索対象はトップウィンドウのみ</summary>
        public static HWND Find(string caption)
        {
            return new HWND(NativeMethod.FindWindowByCaption(IntPtr.Zero, caption));
        }
        /// <summary>クラス名からウィンドウを検索します。検索対象はトップウィンドウのみ</summary>
        public static HWND FindByClassName(string className)
        {
            return new HWND(NativeMethod.FindWindow(className, null));
        }
        /// <summary>アクティブなウィンドウを取得します</summary>
        public static HWND GetActiveWindow()
        {
            return NativeMethod.GetActiveWindow();
        }
        public static HWND GetDesktopWindow()
        {
            return NativeMethod.GetDesktopWindow();
        }
        public static HWND GetFocusWindow()
        {
            return NativeMethod.GetFocus();
        }
        public static HWND GetForegroundWindow()
        {
            return NativeMethod.GetForegroundWindow();
        }
        public static HWND GetWindowFormPosition(POINT p) {
            return NativeMethod.WindowFromPoint(p);
        }


        public override string ToString()
        {
            string text = this.Text;
            return string.Format("HWND: {0}, \"{1}\"", this.Handle, text);
        }
    }
}
