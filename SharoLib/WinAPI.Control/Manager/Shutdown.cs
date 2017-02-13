using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Manager
{
    public static class Shutdown
    {
        public static void AdjustToken()
        {
            const uint TOKEN_ADJUST_PRIVILEGES = 0x20;
            const uint TOKEN_QUERY = 0x8;
            const int SE_PRIVILEGE_ENABLED = 0x2;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                return;

            IntPtr procHandle = NativeMethod.GetCurrentProcess();

            //トークンを取得する
            IntPtr tokenHandle;
            NativeMethod.OpenProcessToken(procHandle,
                TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out tokenHandle);
            //LUIDを取得する
            TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES();
            tp.Attributes = SE_PRIVILEGE_ENABLED;
            tp.PrivilegeCount = 1;
            NativeMethod.LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, out tp.Luid);
            //特権を有効にする
            NativeMethod.AdjustTokenPrivileges(
                tokenHandle, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

            //閉じる
            NativeMethod.CloseHandle(tokenHandle);

        }
        public static void DoShutdown()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.POWEROFF, 0);
        }
        public static void ForceShutdown()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.POWEROFF | EWX.FORCE, 0);
        }
        public static void Logoff()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.LOGOFF, 0);
        }
        public static void ForceLogoff()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.LOGOFF | EWX.FORCE, 0);
        }
        public static void Reboot()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.REBOOT, 0);
        }
        public static void ForceReboot()
        {
            AdjustToken();
            NativeMethod.ExitWindowsEx(EWX.REBOOT | EWX.FORCE, 0);
        }
        public static void Sleep()
        {
            AdjustToken();

        }

    }
}
