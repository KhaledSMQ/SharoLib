using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum TWF : uint
    {
        /// <summary>
        /// 手のひらがタッチされていたらそれを弾く。その代わり認識速度が遅くなる
        /// </summary>
        FineTouch = (0x00000001),
        /// <summary>
        /// 手のひらがタッチされていても弾かない。その代わり読み取り速度が速い。
        /// </summary>
        WantPalm = (0x00000002),
    }
}
