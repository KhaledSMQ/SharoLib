using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public struct WHEELDATA
    {
        ///<summary>ビットデータ。</summary>
        public int State;
        ///<summary>ホイールの回転一刻みを表す。</summary>
        public static readonly int OneWheel = 120;
        ///<summary>ホイールの回転量を表す。クリックされたときは-1。</summary>
        public int WheelDelta
        {
            get
            {
                int delta = State >> 16;
                return (delta < 0) ? -delta : delta;
            }
        }
        ///<summary>ホイールが一刻み分動かされたかどうかを表す。</summary>
        public bool IsOneWheel { get { return (State >> 16) == OneWheel; } }
        ///<summary>ホイールの回転方向を表す。</summary>
        public WHEELDIRECTION Direction
        {
            get
            {
                int delta = State >> 16;
                if (delta == 0) return WHEELDIRECTION.None;
                return (delta < 0) ? WHEELDIRECTION.Backward : WHEELDIRECTION.Forward;
            }
        }
    }
}
