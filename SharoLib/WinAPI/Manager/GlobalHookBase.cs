using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;
using WinAPI.Delegate;

namespace WinAPI.Manager
{
    /// <summary>
    /// Base class to relatively safely register global windows hooks
    /// </summary>
    public abstract class GlobalHookBase : FinalizerBase
    {
        //[DllImport("user32", EntryPoint = "SetWindowsHookExA")]
        //static extern IntPtr SetWindowsHookEx(int idHook, Delegate lpfn, IntPtr hmod, IntPtr dwThreadId);

        //[DllImport("user32", EntryPoint = "UnhookWindowsHookEx")]
        //private static extern int UnhookWindowsHookEx(IntPtr hHook);

        //[DllImport("user32", EntryPoint = "CallNextHookEx")]
        //static extern int CallNextHook(IntPtr hHook, int ncode, IntPtr wParam, IntPtr lParam);


        IntPtr hook;
        public readonly GlobalHookTypes HookId;
        public readonly GlobalHookTypes HookType;

        public GlobalHookBase(GlobalHookTypes Type) : this(Type, false) {
        }

        public GlobalHookBase(GlobalHookTypes Type, bool OnThread) {
            this.HookType = Type;
            del = ProcessMessage;
            if (OnThread)
                hook = NativeMethod.SetWindowsHookEx(Type, del, IntPtr.Zero, NativeMethod.GetCurrentThreadId());
            else {
                var hmod = IntPtr.Zero; // Marshal.GetHINSTANCE(GetType().Module);
                hook = NativeMethod.SetWindowsHookEx(Type, del, hmod, IntPtr.Zero);
            }

            if (hook == IntPtr.Zero) {
                int err = Marshal.GetLastWin32Error();
                string msg = err.GetErrorMessage();

                if (err != 0)
                    throw new Exception("Hookに失敗しました : " + msg);
            }
        }


        private const int HC_ACTION = 0;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        private MessageDelegate del;


        private int ProcessMessage(int hookcode, IntPtr wparam, IntPtr lparam) {
            if (HC_ACTION == hookcode) {
                try {
                    if (Handle(wparam, lparam)) return 1;
                }
                catch { }
            }
            return NativeMethod.CallNextHook(hook, hookcode, wparam, lparam);
        }

        protected abstract bool Handle(IntPtr wparam, IntPtr lparam);



        protected override sealed void OnDispose() {
            NativeMethod.UnhookWindowsHookEx(hook);
            AfterDispose();
        }

        protected virtual void AfterDispose() {
        }

    }

    public abstract class FinalizerBase : IDisposable
    {
        protected readonly AppDomain domain;
        public FinalizerBase() {
            System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            domain = AppDomain.CurrentDomain;
            domain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            domain.DomainUnload += new EventHandler(domain_DomainUnload);
        }

        private bool disposed;
        public bool IsDisposed { get { return disposed; } }
        public void Dispose() {
            if (!disposed) {
                GC.SuppressFinalize(this);
                if (domain != null) {
                    domain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
                    domain.DomainUnload -= new EventHandler(domain_DomainUnload);
                    System.Windows.Forms.Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
                }
                disposed = true;
                OnDispose();
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e) {
            Dispose();
        }

        void domain_DomainUnload(object sender, EventArgs e) {
            Dispose();
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e) {
            Dispose();
        }

        protected abstract void OnDispose();
        /// Destructor
        ~FinalizerBase() {
            Dispose();
        }
    }


}