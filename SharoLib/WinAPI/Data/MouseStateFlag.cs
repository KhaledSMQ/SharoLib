using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public struct MOUSESTATEFLAG
    {
        ///<summary>ビットデータ。</summary>
        public int Flag;
        ///<summary>イベントがインジェクトされたかどうかを表す。</summary>
        public bool IsInjected
        {
            get { return (Flag & 1) != 0; }
            set { Flag = value ? (Flag | 1) : (Flag & ~1); }
        }
    }
}
