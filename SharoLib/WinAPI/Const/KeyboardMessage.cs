using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum KeyboardMessage
    {
        ///<summary>キーが押された。</summary>
        KeyDown = 0x100,
        ///<summary>キーが放された。</summary>
        KeyUp = 0x101,
        ///<summary>システムキーが押された。</summary>
        SysKeyDown = 0x104,
        ///<summary>システムキーが放された。</summary>
        SysKeyUp = 0x105,
    }
}
