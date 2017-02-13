using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    /// <summary>
    /// Contains information about the state of the mouse.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTMOUSE
    {
        /// <summary>
        /// The mouse state.
        /// </summary>
        public RawMouseFlags Flags;

        [StructLayout(LayoutKind.Explicit)]
        public struct MData
        {
            [FieldOffset(0)]
            public uint Buttons;
            /// <summary>
            /// If the mouse wheel is moved, this will contain the delta amount.
            /// </summary>
            [FieldOffset(2)]
            public ushort ButtonData;
            /// <summary>
            /// Flags for the event.
            /// </summary>
            [FieldOffset(0)]
            public RawMouseButtons ButtonFlags;
        }

        public MData Data;

        /// <summary>
        /// Raw button data.
        /// </summary>
        public uint RawButtons;
        /// <summary>
        /// The motion in the X direction. This is signed relative motion or 
        /// absolute motion, depending on the value of usFlags. 
        /// </summary>
        public int LastX;
        /// <summary>
        /// The motion in the Y direction. This is signed relative motion or absolute motion, 
        /// depending on the value of usFlags. 
        /// </summary>
        public int LastY;
        /// <summary>
        /// The device-specific additional information for the event. 
        /// </summary>
        public uint ExtraInformation;
    }
}
