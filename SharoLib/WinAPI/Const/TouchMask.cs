using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    [Flags]
    public enum TouchMask
    {
        /// <summary>
        /// Default - none of the optional fields are valid
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// The rcContact field is valid
        /// </summary>
        ContactArea = 0x00000001,
        /// <summary>
        /// The orientation field is valid
        /// </summary>
        Orientation = 0x00000002,
        /// <summary>
        /// The pressure field is valid
        /// </summary>
        Pressure = 0x00000004,
    }
}
