using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum MouseMessage
    {
        ///<summary>マウスカーソルが移動した。</summary>
        Move = 0x200,
        ///<summary>左ボタンが押された。</summary>
        LDown = 0x201,
        ///<summary>左ボタンが解放された。</summary>
        LUp = 0x202,
        ///<summary>右ボタンが押された。</summary>
        RDown = 0x204,
        ///<summary>左ボタンが解放された。</summary>
        RUp = 0x205,
        ///<summary>中ボタンが押された。</summary>
        MDown = 0x207,
        ///<summary>中ボタンが解放された。</summary>
        MUp = 0x208,
        ///<summary>ホイールが回転した。</summary>
        Wheel = 0x20A,
        ///<summary>Xボタンが押された。</summary>
        XDown = 0x20B,
        ///<summary>Xボタンが解放された。</summary>
        XUp = 0x20C,
    }
}
