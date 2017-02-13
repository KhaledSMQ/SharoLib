using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinAPI.Const;

namespace WinAPI.Data
{
    /// <summary>
    /// Value type for raw input from a keyboard.
    /// </summary>    
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTKEYBOARD
    {
        /// <summary>Scan code for key depression.</summary>
        public short MakeCode;
        /// <summary>Scan code information.</summary>
        public RawKeyboardFlags Flags;
        /// <summary>Reserved.</summary>
        public short Reserved;
        /// <summary>Virtual key code.</summary>
        public Keys VirtualKey;
        /// <summary>Corresponding window message.</summary>
        public WM Message;
        /// <summary>Extra information.</summary>
        public int ExtraInformation;
    }
}
