﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI.Data
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        /// <summary>
        /// A BITMAPINFOHEADER structure that contains information about the dimensions of color format.
        /// </summary>
        public BITMAPINFOHEADER bmiHeader;

        /// <summary>
        /// An array of RGBQUAD. The elements of the array that make up the color table.
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
        public RGBQUAD[] bmiColors;
    }
}
