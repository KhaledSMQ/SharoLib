using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.Const
{
    public enum AnimateWindowFlags : uint
    {
        /// <summary>左から右</summary>
        AW_HOR_POSITIVE = 0x00000001,
        /// <summary>右から左</summary>
        AW_HOR_NEGATIVE = 0x00000002,
        /// <summary>上から下</summary>
        AW_VER_POSITIVE = 0x00000004,
        /// <summary>下から上</summary>
        AW_VER_NEGATIVE = 0x00000008,
        /// <summary>中心から広がって表示</summary>
        AW_CENTER = 0x00000010,
        /// <summary>ウィンドウを隠す</summary>
        AW_HIDE = 0x00010000,
        /// <summary>ウィンドウをアクティブにする</summary>
        AW_ACTIVATE = 0x00020000,
        /// <summary>スライドアニメーションを使う</summary>
        AW_SLIDE = 0x00040000,
        /// <summary>フェードイン</summary>
        AW_BLEND = 0x00080000
    }

}
