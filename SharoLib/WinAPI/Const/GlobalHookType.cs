using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum GlobalHookTypes : int
    {
        BeforeWindow = 4, //WH_CALLWNDPROC 
        AfterWindow = 12, //WH_CALLWNDPROCRET 
        KeyBoard = 2, //WH_KEYBOARD
        KeyBoard_Global = 13,  //WH_KEYBOARD_LL
        Mouse = 7, //WH_MOUSE
        Mouse_Global = 14, //WH_MOUSE_LL
        JournalRecord = 0, //WH_JOURNALRECORD
        JournalPlayback = 1, //WH_JOURNALPLAYBACK
        ForeGroundIdle = 11, //WH_FOREGROUNDIDLE
        SystemMessages = 6, //WH_SYSMSGFILTER
        MessageQueue = 3, //WH_GETMESSAGE
        ComputerBasedTraining = 5, //WH_CBT 
        Hardware = 8, //WH_HARDWARE 
        Debug = 9, //WH_DEBUG 
        Shell = 10, //WH_SHELL
    }
}
