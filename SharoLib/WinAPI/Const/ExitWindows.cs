using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinAPI.Const
{
    public enum EWX : uint
    {
        LOGOFF = 0x00,
        SHUTDOWN = 0x01,
        REBOOT = 0x02,
        POWEROFF = 0x08,
        RESTARTAPPS = 0x40,
        FORCE = 0x04,
        FORCEIFHUNG = 0x10,
    }
}
