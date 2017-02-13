using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public enum WHEELDIRECTION
    {
        ///<summary>回転していない。</summary>
        None = 0,
        ///<summary>ユーザから離れる方向へ回転した。</summary>
        Forward = 1,
        ///<summary>ユーザに近づく方向へ回転した。</summary>
        Backward = -1,
    }
}
