using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public struct XBUTTONDATA
    {
        ///<summary>ビットデータ。</summary>
        public int State;
        ///<summary>操作されたボタンを示す。</summary>
        public int ControlledButton { get { return State >> 16; } }
        ///<summary>Xボタン1が押されたかどうかを示す。</summary>
        public bool IsXButton1 { get { return (State >> 16) == 1; } }
        ///<summary>Xボタン2が押されたかどうかを示す。</summary>
        public bool IsXButton2 { get { return (State >> 16) == 2; } }
    }
}
