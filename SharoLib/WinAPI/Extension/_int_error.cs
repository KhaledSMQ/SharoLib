using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinAPI;
using WinAPI.Const;


public static class _int_error
{
    public static bool IsError(this int value)
    {
        return value == 0;
    }
    public static string GetErrorMessage(this int value)
    {
        int code = Marshal.GetLastWin32Error();
        StringBuilder message = new StringBuilder(255);

        NativeMethod.FormatMessage(
          FormatFlag.FORMAT_MESSAGE_FROM_SYSTEM,
          IntPtr.Zero,
          (uint)code,
          0,
          message,
          message.Capacity,
          IntPtr.Zero);

        return message.ToString();
    }
    public static void CheckError(this int value)
    {
        if (value.IsError())
        {
            throw new WinAPIException(value.GetErrorMessage(), value);
        }
    }

    public static void CheckError(this bool value)
    {
        if (!value)
        {
            throw new WinAPIException(0.GetErrorMessage(), -1);
        }
    }
}

