using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Data
{
    public struct KEYBOARDSTATEFLAG
    {
        private int flag;
        private bool IsFlagging(int value)
        {
            return (flag & value) != 0;
        }
        private void Flag(bool value, int digit)
        {
            flag = value ? (flag | digit) : (flag & ~digit);
        }
        ///<summary>キーがテンキー上のキーのような拡張キーかどうかを表す。</summary>
        public bool IsExtended { get { return IsFlagging(0x01); } set { Flag(value, 0x01); } }
        ///<summary>イベントがインジェクトされたかどうかを表す。</summary>
        public bool IsInjected { get { return IsFlagging(0x10); } set { Flag(value, 0x10); } }
        ///<summary>ALTキーが押されているかどうかを表す。</summary>
        public bool AltDown { get { return IsFlagging(0x20); } set { Flag(value, 0x20); } }
        ///<summary>キーが放されたどうかを表す。</summary>
        public bool IsUp { get { return IsFlagging(0x80); } set { Flag(value, 0x80); } }
    }
}
