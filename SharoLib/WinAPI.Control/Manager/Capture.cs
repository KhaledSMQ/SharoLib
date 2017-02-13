/***********************************
 * 
 * WindowsAPI Monitor
 * * Create: 2014/05/30
 * * Creator: sharoron (Twitter: @sharo0331)
 * 
 * Require:
 *  gui32.cs
 *   gui32/DC.cs
 *  user32.cs
 *   user32/DC.cs
 *   user32/Window.cs
 *   
 *  Reference:
 *   System.Drawing.dll
 *   System.Window.Forms.dll
 * 
 **********************************/


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Manager
{
    public class Capture
    {
        /// <summary>プライマリスクリーンをキャプチャします</summary>
        public static Bitmap CaptureScreen(Screen scr)
        {
            //プライマリモニタのデバイスコンテキストを取得
            IntPtr disDC = NativeMethod.GetDC(IntPtr.Zero);
            //Bitmapの作成
            Bitmap bmp = new Bitmap(scr.Bounds.Width,
                scr.Bounds.Height);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //Graphicsのデバイスコンテキストを取得
            IntPtr hDC = g.GetHdc();
            //Bitmapに画像をコピーする
            NativeMethod.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height,
                disDC, scr.Bounds.X, scr.Bounds.Y, BitBltOp.SRCCOPY);
            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            NativeMethod.ReleaseDC(IntPtr.Zero, disDC);

            return bmp;
        }

        /// <summary>アクティブになっているウィドウをキャプチャします</summary>
        public static Bitmap CaptureActiveWindow(HWND hWnd)
        {
            //アクティブなウィンドウのデバイスコンテキストを取得
            IntPtr winDC = NativeMethod.GetWindowDC(hWnd);
            //ウィンドウの大きさを取得
            RECT winRect = new RECT();
            NativeMethod.GetWindowRect(hWnd, ref winRect);
            //Bitmapの作成
            Bitmap bmp = new Bitmap(winRect.right - winRect.left,
                winRect.bottom - winRect.top);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //Graphicsのデバイスコンテキストを取得
            IntPtr hDC = g.GetHdc();
            //Bitmapに画像をコピーする
            NativeMethod.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height,
                winDC, 0, 0, BitBltOp.SRCCOPY);
            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            NativeMethod.ReleaseDC(hWnd, winDC);

            return bmp;

        }

    }
}
