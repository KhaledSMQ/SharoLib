using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WinAPI.Const;
using WinAPI.Data;
using WinAPI.Manager;

namespace WinAPI.Control
{
    public class AeroText : IDisposable
    
	{
		private IntPtr _TextHDC;
		private Rectangle _TextHDCBounds;
        private AeroLabelMode _mode = AeroLabelMode.Normal;

        internal AeroText(IntPtr textHdc, Rectangle bounds, AeroLabelMode mode)
		{
			this._TextHDC = textHdc;
			this._TextHDCBounds = bounds;
            this._mode = mode;
		}
        public static AeroText CreateWithGlow(Graphics g, string text, Font font, Padding internalBounds, Rectangle bounds, Color color, TextFormatFlags formatFlags, int glowSize)
		{
            return AeroText.Create(g, text, font, internalBounds, bounds, color, formatFlags, AeroLabelMode.Aero, new IThemeTextOption[]
			{
				new GlowOption(glowSize)
			});
		}
        public static AeroText Create(Graphics g, string text, Font font, Padding internalBounds, Rectangle bounds, Color color, TextFormatFlags formatFlags, AeroLabelMode mode, IThemeTextOption[] options)
		{
			IntPtr outputHdc = g.GetHdc();
			IntPtr DIB;
            IntPtr compatHdc = AeroText.CreateNewHDC(outputHdc, bounds, out DIB);
            AeroText.DrawOnHDC(compatHdc, text, font, internalBounds, bounds, color, formatFlags, mode, options);
            AeroText.CleanUpHDC(DIB);
			g.ReleaseHdc(outputHdc);
            return new AeroText(compatHdc, bounds, mode);
		}
		public void Update(Graphics g, string text, Font font, Padding internalBounds, Rectangle bounds, Color color, Color backColor, TextFormatFlags formatFlags, IThemeTextOption[] options)
		{
			IntPtr compatHdc = this._TextHDC;
			if (bounds.Equals(this._TextHDCBounds))
			{
                IntPtr hClearBrush = NativeMethod.CreateSolidBrush(ColorTranslator.ToWin32(backColor));
				RECT cleanRect = new RECT(bounds);
                NativeMethod.FillRect(compatHdc, ref cleanRect, hClearBrush);
                NativeMethod.DeleteObject(hClearBrush);
			}
			else
			{
				IntPtr outputHdc = g.GetHdc();
				IntPtr DIB;
				compatHdc = AeroText.CreateNewHDC(outputHdc, bounds, out DIB);
				this._TextHDC = compatHdc;
				this._TextHDCBounds = bounds;
				AeroText.CleanUpHDC(DIB);
				g.ReleaseHdc(outputHdc);
			}
			AeroText.DrawOnHDC(compatHdc, text, font, internalBounds, bounds, color, formatFlags, this._mode, options);
		}
		private static IntPtr CreateNewHDC(IntPtr outputHdc, Rectangle bounds, out IntPtr DIB)
		{
			IntPtr compatHdc = NativeMethod.CreateCompatibleDC(outputHdc);
			BITMAPINFO info = default(BITMAPINFO);
            info.bmiHeader = new BITMAPINFOHEADER();
            info.bmiHeader.biSize = (uint)Marshal.SizeOf(info);
            info.bmiHeader.biWidth = bounds.Width;
            info.bmiHeader.biHeight = -bounds.Height;
            info.bmiHeader.biPlanes = 1;
            info.bmiHeader.biBitCount = 32;
            info.bmiHeader.biCompression = 0;
			DIB = NativeMethod.CreateDIBSection(outputHdc, ref info, 0u, 0, IntPtr.Zero, 0u);
			NativeMethod.SelectObject(compatHdc, DIB);
			return compatHdc;
		}
		private static void DrawOnHDC(IntPtr compatHdc, string text, Font font, Padding internalBounds, Rectangle bounds, Color color, TextFormatFlags formatFlags, AeroLabelMode mode, IThemeTextOption[] options)
		{
            if (SystemSupports.IsAeroSupport && mode == AeroLabelMode.Aero)
            {
                IntPtr hFont = font.ToHfont();
                NativeMethod.SelectObject(compatHdc, hFont);
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
                DTTOPTS dttOpts = default(DTTOPTS);
                dttOpts.dwSize = Marshal.SizeOf(dttOpts);
                dttOpts.dwFlags = (DTTOPSFlags)8193;
                dttOpts.crText = ColorTranslator.ToWin32(color);
                for (int i = 0; i < options.Length; i++)
                {
                    IThemeTextOption op = options[i];
                    op.Apply(ref dttOpts);
                }
                RECT RECT = new RECT(internalBounds.Left, internalBounds.Top, bounds.Width - internalBounds.Right, bounds.Height - internalBounds.Bottom);
                int ret = NativeMethod.DrawThemeTextEx(renderer.Handle, compatHdc, 0, 0, text, -1, (int)formatFlags, ref RECT, ref dttOpts);
                if (ret != 0)
                {
                    Marshal.ThrowExceptionForHR(ret);
                }
                NativeMethod.DeleteObject(hFont); 
            }
            else
            {
                Graphics gc = Graphics.FromHdc(compatHdc);
                gc.TextRenderingHint = TextRenderingHint.AntiAlias;
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(254, color)))
                    gc.DrawString(text, font, sb, bounds);
            }

		}
		private static void CleanUpHDC(IntPtr DIB)
		{
            NativeMethod.DeleteObject(DIB);
		}
		public void Dispose()
		{
            NativeMethod.DeleteDC(this._TextHDC);
		}
		public void Draw(Graphics g, RectangleF rect)
		{
			this.Draw(g, new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
		}
		public void Draw(Graphics g, Point location, Size size)
		{
			this.Draw(g, new Rectangle(location, size));
		}
		public void Draw(Graphics g, PointF location, SizeF size)
		{
			this.Draw(g, new Rectangle((int)location.X, (int)location.Y, (int)size.Width, (int)size.Height));
		}
		public void Draw(Graphics g, Rectangle rect)
		{
			IntPtr outputHDC = g.GetHdc();
            NativeMethod.BitBlt(outputHDC, rect.X, rect.Y, rect.Width, rect.Height, this._TextHDC, 0, 0, BitBltOp.SRCCOPY);
            g.ReleaseHdc(outputHDC);
        }
	}
}
