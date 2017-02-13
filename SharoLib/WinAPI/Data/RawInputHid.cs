using System;
using System.Runtime.InteropServices;

namespace WinAPI.Data
{
    /// <summary>
    /// Value type for raw input from a HID.
    /// </summary>    
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTHID
    {
        /// <summary>Size of the HID data in bytes.</summary>
        public int Size;
        /// <summary>Number of HID in Data.</summary>
        public int Count;
        /// <summary>Data for the HID.</summary>
        public IntPtr Data;
    }
}