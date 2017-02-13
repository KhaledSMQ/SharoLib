using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinAPI
{
    public partial class NativeMethod
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);


        [DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
        public static extern IntPtr SelectObjectCE(IntPtr hdc, IntPtr hgdiobj);
    }
}
