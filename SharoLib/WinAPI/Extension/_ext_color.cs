using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class _ext_color
{
    public static int ConvertColourToWindowsRGB(this Color value)
    {
        int winRGB = 0;

        // windows rgb values have byte order 0x00BBGGRR
        winRGB |= (int)value.R;
        winRGB |= (int)value.G << 8;
        winRGB |= (int)value.B << 16;

        return winRGB;
    }
    public static Color ConvertWindowsRGBToColour(this int value)
    {
        int r = 0, g = 0, b = 0;

        // windows rgb values have byte order 0x00BBGGRR
        r = (value & 0x000000FF);
        g = (value & 0x0000FF00) >> 8;
        b = (value & 0x00FF0000) >> 16;

        Color dotNetColour = Color.FromArgb(r, g, b);

        return dotNetColour;
    }
}