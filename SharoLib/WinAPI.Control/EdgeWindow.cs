using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinAPI.Data;
using WinAPI;
using WinAPI.Const;
using WinAPI.Control.Option;

namespace WinAPI.Control
{
    public class EdgeWindow : Form, IDisposable
    {

        private enum MousePositionCodes
        {
            HTERROR = (-2),
            HTTRANSPARENT = (-1),
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

        private Boolean AppbarNew()
        {
            if (CallbackMessageID == 0)
                throw new Exception("CallbackMessageID is 0");

            if (IsAppbarMode)
                return true;

            m_PrevSize = Size;
            m_PrevLocation = Location;

            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uCallbackMessage = CallbackMessageID;

            
            UInt32 retVal = NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.New, ref msgData);
            IsAppbarMode = (retVal != 0);

            SizeAppBar();

            return IsAppbarMode;
        }
        private Boolean AppbarRemove()
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;

            
            UInt32 retVal = NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.Remove, ref msgData);

            IsAppbarMode = false;

            Size = m_PrevSize;
            Location = m_PrevLocation;

            return (retVal != 0) ? true : false;
        }
        private void AppbarQueryPos(ref RECT appRect)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uEdge = (UInt32)m_Edge;
            msgData.rc = appRect;

            
            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.QueryPos, ref msgData);
            appRect = msgData.rc;
        }
        private void AppbarSetPos(ref RECT appRect)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uEdge = (UInt32)m_Edge;
            msgData.rc = appRect;

            
            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.SetPos, ref msgData);
            appRect = msgData.rc;
        }
        private void AppbarActivate()
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;

            
            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.Activate, ref msgData);
        }
        private void AppbarWindowPosChanged()
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;

            
            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.WindowPosChanged, ref msgData);
        }
        private Boolean AppbarSetAutoHideBar(Boolean hideValue)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = Handle;
            msgData.uEdge = (UInt32)m_Edge;
            msgData.lParam = (hideValue) ? 1 : 0;


            UInt32 retVal = NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.SetAutoHideBar, ref msgData);
            return (retVal != 0) ? true : false;
        }
        private IntPtr AppbarGetAutoHideBar(AppBarEdges edge)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.uEdge = (UInt32)edge;


            IntPtr retVal = (IntPtr)NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.GetAutoHideBar, ref msgData);
            return retVal;
        }
        private AppBarStates AppbarGetTaskbarState()
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);


            UInt32 retVal = NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.GetState, ref msgData);
            return (AppBarStates)retVal;
        }
        private void AppbarSetTaskbarState(AppBarStates state)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.lParam = (Int32)state;


            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.SetState, ref msgData);
        }
        private void AppbarGetTaskbarPos(out RECT taskRect)
        {
            
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);


            NativeMethod.SHAppBarMessage((UInt32)AppBarMessages.GetTaskBarPos, ref msgData);
            taskRect = msgData.rc;
        }

        private AppBarEdges m_Edge = AppBarEdges.Float;
        private UInt32 CallbackMessageID = 0;
        private Boolean IsAppbarMode = false;
        private Size m_PrevSize;
        private Point m_PrevLocation;

        public EdgeWindow()
        {
            FormBorderStyle = FormBorderStyle.SizableToolWindow;

            if (!DesignMode)
            {
                CallbackMessageID = RegisterCallbackMessage();
                if (CallbackMessageID == 0)
                    throw new Exception("RegisterCallbackMessage failed");
            }
        }
        private UInt32 RegisterCallbackMessage()
        {
            String uniqueMessageString = Guid.NewGuid().ToString();
            return NativeMethod.RegisterWindowMessage(uniqueMessageString);
        }
        private void SizeAppBar()
        {
            RECT rt = new RECT();

            if ((m_Edge == AppBarEdges.Left) ||
                (m_Edge == AppBarEdges.Right))
            {
                rt.top = 0;
                rt.bottom = SystemInformation.PrimaryMonitorSize.Height;
                if (m_Edge == AppBarEdges.Left)
                {
                    rt.right = m_PrevSize.Width;
                }
                else
                {
                    rt.right = SystemInformation.PrimaryMonitorSize.Width;
                    rt.left = rt.right - m_PrevSize.Width;
                }
            }
            else
            {
                rt.left = 0;
                rt.right = SystemInformation.PrimaryMonitorSize.Width;
                if (m_Edge == AppBarEdges.Top)
                {
                    rt.bottom = m_PrevSize.Height;
                }
                else
                {
                    rt.bottom = SystemInformation.PrimaryMonitorSize.Height;
                    rt.top = rt.bottom - m_PrevSize.Height;
                }
            }

            AppbarQueryPos(ref rt);

            switch (m_Edge)
            {
                case AppBarEdges.Left:
                    rt.right = rt.left + m_PrevSize.Width;
                    break;
                case AppBarEdges.Right:
                    rt.left = rt.right - m_PrevSize.Width;
                    break;
                case AppBarEdges.Top:
                    rt.bottom = rt.top + m_PrevSize.Height;
                    break;
                case AppBarEdges.Bottom:
                    rt.top = rt.bottom - m_PrevSize.Height;
                    break;
            }

            AppbarSetPos(ref rt);

            Location = new Point(rt.left, rt.top);
            Size = new Size(rt.right - rt.left, rt.bottom - rt.top);
        }
        void OnAppbarNotification(ref Message msg)
        {
            AppBarStates state;
            AppBarNotifications msgType = (AppBarNotifications)(Int32)msg.WParam;

            switch (msgType)
            {
                case AppBarNotifications.PosChanged:
                    SizeAppBar();
                    break;

                case AppBarNotifications.StateChange:
                    state = AppbarGetTaskbarState();
                    if ((state & AppBarStates.AlwaysOnTop) != 0)
                    {
                        TopMost = true;
                        BringToFront();
                    }
                    else
                    {
                        TopMost = false;
                        SendToBack();
                    }
                    break;

                case AppBarNotifications.FullScreenApp:
                    if ((int)msg.LParam != 0)
                    {
                        TopMost = false;
                        SendToBack();
                    }
                    else
                    {
                        state = AppbarGetTaskbarState();
                        if ((state & AppBarStates.AlwaysOnTop) != 0)
                        {
                            TopMost = true;
                            BringToFront();
                        }
                        else
                        {
                            TopMost = false;
                            SendToBack();
                        }
                    }

                    break;

                case AppBarNotifications.WindowArrange:
                    if ((int)msg.LParam != 0)	
                        Visible = false;
                    else						
                        Visible = true;

                    break;
            }
        }
        void OnNcHitTest(ref Message msg)
        {
            DefWndProc(ref msg);
            if ((m_Edge == AppBarEdges.Top) && ((int)msg.Result == (int)MousePositionCodes.HTBOTTOM))
                0.ToString();
            else if ((m_Edge == AppBarEdges.Bottom) && ((int)msg.Result == (int)MousePositionCodes.HTTOP))
                0.ToString();
            else if ((m_Edge == AppBarEdges.Left) && ((int)msg.Result == (int)MousePositionCodes.HTRIGHT))
                0.ToString();
            else if ((m_Edge == AppBarEdges.Right) && ((int)msg.Result == (int)MousePositionCodes.HTLEFT))
                0.ToString();
            else if ((int)msg.Result == (int)MousePositionCodes.HTCLOSE)
                0.ToString();
            else
            {
                msg.Result = (IntPtr)MousePositionCodes.HTCLIENT;
                return;
            }
            base.WndProc(ref msg);
        }
        protected override void WndProc(ref Message msg)
        {
            if (IsAppbarMode)
            {
                if (msg.Msg == CallbackMessageID)
                {
                    OnAppbarNotification(ref msg);
                }
                else if (msg.Msg == (int)WM.WM_ACTIVATE)
                {
                    AppbarActivate();
                }
                else if (msg.Msg == (int)WM.WM_WINDOWPOSCHANGED)
                {
                    AppbarWindowPosChanged();
                }
                else if (msg.Msg == (int)WM.WM_NCHITTEST)
                {
                    OnNcHitTest(ref msg);
                    return;
                }
            }

            base.WndProc(ref msg);
        }

        protected override void OnLoad(EventArgs e)
        {
            m_PrevSize = Size;
            m_PrevLocation = Location;
            base.OnLoad(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            AppbarRemove();
            base.OnClosing(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            if (IsAppbarMode)
            {
                if (m_Edge == AppBarEdges.Top || m_Edge == AppBarEdges.Bottom)
                    m_PrevSize.Height = Size.Height;
                else
                    m_PrevSize.Width = Size.Width;

                SizeAppBar();
            }

            base.OnSizeChanged(e);
        }

        [Browsable(false)]
        public AppBarEdges Edge
        {
            get
            {
                return m_Edge;
            }
            set
            {
                m_Edge = value;
                if (value == AppBarEdges.Float)
                    AppbarRemove();
                else
                    AppbarNew();

                if (IsAppbarMode)
                    SizeAppBar();

            }
        }
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle 
        { get { return base.FormBorderStyle; } set { base.FormBorderStyle = value; } }

        protected override void Dispose(bool disposing)
        {
            /*if (this.Edge != AppBarEdges.Float)
                this.Edge = AppBarEdges.Float; //*/
        }
    }
}