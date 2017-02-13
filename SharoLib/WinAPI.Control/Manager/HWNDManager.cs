using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Manager
{
    public static class HWNDManager
    {
        public static void SetNotActiveWindow(HWND hWnd)
        {
            // 現在のスタイルを取得
            WindowStyles unSyle = (WindowStyles)NativeMethod.GetWindowLong(hWnd, GWL.EXSTYLE);

            // キャプションのスタイルを削除
            unSyle = (unSyle | WindowStyles.NOACTIVATE);

            // スタイルを反映
            int unret = NativeMethod.SetWindowLong(hWnd, GWL.EXSTYLE, unSyle);

            // ウィンドウを再描画
            NativeMethod.SetWindowPos(hWnd, IntPtr.Zero,
                0, 0, 0, 0,
                SWP.NOMOVE | SWP.NOSIZE | SWP.NOZORDER | SWP.FRAMECHANGED);
        }
    }
}
