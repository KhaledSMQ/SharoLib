using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTER_INFO
    {
        public POINTER_INPUT_TYPE pointerType;
        public uint pointerId;
        public uint frameId;
        public IntPtr sourceDevice;
        public IntPtr hwndTarget;
        public POINT ptPixelLocation;
        public POINT ptHimetricLocation;
        public POINT ptPixelLocationPredicted;
        public POINT ptHimetricLocationPredicted;
        public PointerFlags pointerFlags;
        public uint dwTime;
        public uint historyCount;
        public uint inputData;
        public uint dwKeyStates;
        public ulong Reserved;
    }
}
