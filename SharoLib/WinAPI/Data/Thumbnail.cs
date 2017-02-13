using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using WinAPI.Const;

namespace WinAPI.Data
{
    public sealed class Thumbnail : SafeHandle
    {
        public override bool IsInvalid
        {
            [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            get
            {
                return base.IsClosed || this.handle == IntPtr.Zero;
            }
        }
        public byte Opacity
        {
            set
            {
                DwmThumbnailProperties prop = default(DwmThumbnailProperties);
                prop.dwFlags = DwmThumbnailFlags.Opacity;
                prop.opacity = value;
                NativeMethod.DwmUpdateThumbnailProperties(this, ref prop).CheckError();
            }
        }
        public bool ShowOnlyClientArea
        {
            set
            {
                DwmThumbnailProperties prop = default(DwmThumbnailProperties);
                prop.dwFlags = DwmThumbnailFlags.SourceClientAreaOnly;
                prop.fSourceClientAreaOnly = value;
                NativeMethod.DwmUpdateThumbnailProperties(this, ref prop).CheckError();
            }
        }
        public Rectangle DestinationRectangle
        {
            set
            {
                DwmThumbnailProperties prop = default(DwmThumbnailProperties);
                prop.dwFlags = DwmThumbnailFlags.RectDestination;
                prop.rcDestination = new RECT(value);
                NativeMethod.DwmUpdateThumbnailProperties(this, ref prop).CheckError();       
            }
        }
        public Rectangle SourceRectangle
        {
            set
            {
                DwmThumbnailProperties prop = default(DwmThumbnailProperties);
                prop.dwFlags = DwmThumbnailFlags.RectSource;
                prop.rcSource = new RECT(value);
                NativeMethod.DwmUpdateThumbnailProperties(this, ref prop) .CheckError();
            }
        }
        public bool Visible
        {
            set
            {
                DwmThumbnailProperties prop = default(DwmThumbnailProperties);
                prop.dwFlags = DwmThumbnailFlags.Visible;
                prop.fVisible = value;
                NativeMethod.DwmUpdateThumbnailProperties(this, ref prop) .CheckError();
            }
        }
        public Size SourceSize
        {
            get
            {
                DwmSize size;
                NativeMethod.DwmQueryThumbnailSourceSize(this, out size).CheckError();
                return size.ToSize();
            }
        }

        internal Thumbnail()
            : base(IntPtr.Zero, true)
        {
        }
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override bool ReleaseHandle()
        {
            return NativeMethod.DwmUnregisterThumbnail(this.handle) == 0;
        }
        public void Update(Rectangle destination, Rectangle source, byte opacity, bool visible, bool onlyClientArea)
        {
            if (source.Width < 1 || source.Height < 1)
            {
                throw new ArgumentException("Thumbnail source rectangle cannot have null or negative size.");
            }
            DwmThumbnailProperties prop = default(DwmThumbnailProperties);
            prop.dwFlags = (DwmThumbnailFlags.RectDestination | DwmThumbnailFlags.RectSource | DwmThumbnailFlags.Opacity | DwmThumbnailFlags.Visible | DwmThumbnailFlags.SourceClientAreaOnly);
            prop.rcDestination = new RECT(destination);
            prop.rcSource = new RECT(source);
            prop.opacity = opacity;
            prop.fVisible = visible;
            prop.fSourceClientAreaOnly = onlyClientArea;
            NativeMethod.DwmUpdateThumbnailProperties(this, ref prop) .CheckError();
        }
        public void Update(Rectangle destination, byte opacity, bool visible, bool onlyClientArea)
        {
            DwmThumbnailProperties prop = default(DwmThumbnailProperties);
            prop.dwFlags = (DwmThumbnailFlags.RectDestination | DwmThumbnailFlags.Opacity | DwmThumbnailFlags.Visible | DwmThumbnailFlags.SourceClientAreaOnly);
            prop.rcDestination = new RECT(destination);
            prop.opacity = opacity;
            prop.fVisible = visible;
            prop.fSourceClientAreaOnly = onlyClientArea;
            NativeMethod.DwmUpdateThumbnailProperties(this, ref prop) .CheckError();
        }
    }
}
