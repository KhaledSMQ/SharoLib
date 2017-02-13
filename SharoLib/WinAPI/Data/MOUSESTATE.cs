using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSESTATE
    {
        ///<summary>スクリーン座標によるマウスカーソルの現在位置。</summary>
        [FieldOffset(0)]
        public Point Point;
        ///<summary>messageがMouseMessage.Wheelの時にその詳細データを持つ。</summary>
        [FieldOffset(8)]
        public WHEELDATA WheelData;
        ///<summary>messageがMouseMessage.XDown/MouseMessage.XUpの時にその詳細データを持つ。</summary>
        [FieldOffset(8)]
        public XBUTTONDATA XButtonData;
        ///<summary>マウスのイベントインジェクト。</summary>
        [FieldOffset(12)]
        public MOUSESTATEFLAG Flag;
        ///<summary>メッセージが送られたときの時間</summary>
        [FieldOffset(16)]
        public int Time;
        ///<summary>メッセージに関連づけられた拡張情報</summary>
        [FieldOffset(20)]
        public IntPtr ExtraInfo;
    }
}
