using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;
using WinAPI.Data;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RawInput
    {
        /// <summary>Header for the data.</summary>
        [FieldOffset(0)]
        public RAWINPUTHEADER Header;
        /// <summary>Mouse raw input data.</summary>
        [FieldOffset(16)]
        public RAWINPUTMOUSE Mouse;
        /// <summary>Keyboard raw input data.</summary>
        [FieldOffset(16)]
        public RAWINPUTKEYBOARD Keyboard;
        /// <summary>HID raw input data.</summary>
        [FieldOffset(16)]
        public RAWINPUTHID Hid;
    }

    /// <summary>
    /// Value type for a raw input header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTHEADER
    {
        /// <summary>Type of device the input is coming from.</summary>
        public RawInputType Type;
        /// <summary>Size of the packet of data.</summary>
        public int Size;
        /// <summary>Handle to the device sending the data.</summary>
        public IntPtr Device;
        /// <summary>wParam from the window message.</summary>
        public IntPtr wParam;
    }
}
