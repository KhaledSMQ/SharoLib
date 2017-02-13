using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WinAPI.Const;
using WinAPI.Manager;

namespace WinAPI.Control
{
    public class AeroLabel : global::System.Windows.Forms.Control
    
	{
        public AeroLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

		private AeroText _hText;
		private bool _needsUpdate;
		private int _glowSize = 9;
		private bool _glowEnable = true;
		private ShadowOption.ShadowType _shadowType = ShadowOption.ShadowType.Continuous;
		private Point _shadowOffset = new Point(5, 5);
		private Color _shadowColor = Color.Transparent;
		private bool _shadowEnable;
		private bool _overlayEnable = true;
		private Color _borderColor = Color.Red;
		private int _borderSize = 1;
		private bool _borderEnable;
		private HorizontalAlignment _horizontal;
		private VerticalAlignment _vertical;
		private bool _singleLine = true;
		private bool _endEllipsis;
		private bool _wordBreak;
		private bool _wordEllipsis;
        private AeroLabelMode _mode = AeroLabelMode.Normal; 

        [EditorAttribute(typeof(MultilineStringEditor),
                 typeof(System.Drawing.Design.UITypeEditor))]  
		public override string Text
		{
			set
			{
				base.Text = value;
				this.UpdateText();
			}
		}
		public override Font Font
		{
			set
			{
				base.Font = value;
				this.UpdateText();
			}
		}
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
				this.UpdateText();
			}
		}
		public override Color ForeColor
		{
			set
			{
				base.ForeColor = value;
				this.UpdateText();
			}
		}
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
		[Browsable(false)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}
		[Browsable(false)]
		public new ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}
		[Category("Appearance"), DefaultValue(9)]
		public int GlowSize
		{
			get
			{
				return this._glowSize;
			}
			set
			{
				this._glowSize = value;
				this.UpdateText();
			}
		}
		[DefaultValue(true), Category("Appearance")]
		public bool GlowEnabled
		{
			get
			{
				return this._glowEnable;
			}
			set
			{
				this._glowEnable = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(ShadowOption.ShadowType.Continuous)]
		public ShadowOption.ShadowType ShadowType
		{
			get
			{
				return this._shadowType;
			}
			set
			{
				this._shadowType = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Point), "5, 5")]
		public Point ShadowOffset
		{
			get
			{
				return this._shadowOffset;
			}
			set
			{
				this._shadowOffset = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "Black")]
		public Color ShadowColor
		{
			get
			{
				return this._shadowColor;
			}
			set
			{
				this._shadowColor = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(false)]
		public bool ShadowEnabled
		{
			get
			{
				return this._shadowEnable;
			}
			set
			{
				this._shadowEnable = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(true)]
		public bool OverlayEnabled
		{
			get
			{
				return this._overlayEnable;
			}
			set
			{
				this._overlayEnable = value;
				this.UpdateText();
			}
		}
		[DefaultValue(typeof(Color), "Red"), Category("Appearance")]
		public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
			set
			{
				this._borderColor = value;
				this.UpdateText();
			}
		}
		[Category("Appearance"), DefaultValue(1)]
		public int BorderSize
		{
			get
			{
				return this._borderSize;
			}
			set
			{
				this._borderSize = value;
				this.UpdateText();
			}
		}
		[DefaultValue(false), Category("Appearance")]
		public bool BorderEnabled
		{
			get
			{
				return this._borderEnable;
			}
			set
			{
				this._borderEnable = value;
				this.UpdateText();
			}
		}
		[DefaultValue(typeof(HorizontalAlignment), "Left"), Category("Appearance")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this._horizontal;
			}
			set
			{
				this._horizontal = value;
				this.UpdateText();
			}
		}
        [Category("Appearance"), DefaultValue(typeof(VerticalAlignment), "Top")]
		public VerticalAlignment TextAlignVertical
		{
			get
			{
				return this._vertical;
			}
			set
			{
				this._vertical = value;
				this.UpdateText();
			}
		}
        [DefaultValue(true), Category("Appearance")]
		public bool SingleLine
		{
			get
			{
				return this._singleLine;
			}
			set
			{
				this._singleLine = value;
				this.UpdateText();
			}
		}
        [DefaultValue(false), Category("Appearance")]
		public bool EndEllipsis
		{
			get
			{
				return this._endEllipsis;
			}
			set
			{
				this._endEllipsis = value;
				this.UpdateText();
			}
		}
		[DefaultValue(false), Category("Appearance")]
		public bool WordBreak
		{
			get
			{
				return this._wordBreak;
			}
			set
			{
				this._wordBreak = value;
				this.UpdateText();
			}
		}
        [DefaultValue(false), Category("Appearance")]
        public bool WordEllipsis
        {
            get
            {
                return this._wordBreak;
            }
            set
            {
                this._wordBreak = value;
                this.UpdateText();
            }
        }
        [DefaultValue(false), Category("Appearance")]
        public AeroLabelMode DrawMode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
                if (value == AeroLabelMode.Aero)
                {
                    if (this.BackColor == Color.Transparent)
                        this.BackColor = Color.Black;
                }
                else
                {
                    if (this.BackColor == Color.Black)
                        this.BackColor = Color.Transparent;
                }
                this.UpdateText();
            }
        }


		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (this._hText != null)
			{
				this._hText.Dispose();
			}
			this._hText = null;
			base.OnHandleDestroyed(e);
		}
		private void UpdateText()
		{
			this._needsUpdate = true;
			base.Invalidate(false);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.UpdateText();
		}
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 132 && !base.DesignMode)
			{
				base.WndProc(ref m);
				m.Result = new IntPtr(-1);
				return;
			}
            else if (m.Msg == (int)WM.WM_DWMCOMPOSITIONCHANGED)
            {
                if (!SystemSupports.IsAeroSupport && this._mode == AeroLabelMode.Aero)
                    this.DrawMode = AeroLabelMode.Normal;
            }
			base.WndProc(ref m);
		}
		protected int CountOptions()
		{
			int count = 0;
			if (this._glowEnable)
			{
				count++;
			}
			if (this._shadowEnable)
			{
				count++;
			}
			if (!this._overlayEnable)
			{
				count++;
			}
			if (this._borderEnable)
			{
				count++;
			}
			return count;
		}
		protected IThemeTextOption[] CreateOptions()
		{
			IThemeTextOption[] options = new IThemeTextOption[this.CountOptions()];
			int curr = 0;
			if (this._glowEnable)
			{
				options[curr] = new GlowOption(this._glowSize);
				curr++;
			}
			if (this._shadowEnable)
			{
				options[curr] = new ShadowOption(this._shadowColor, this._shadowOffset, this._shadowType);
				curr++;
			}
			if (!this._overlayEnable)
			{
				options[curr] = new OverlayOption(this._overlayEnable);
				curr++;
			}
			if (this._borderEnable)
			{
				options[curr] = new BorderOption(this._borderColor, this._borderSize);
				curr++;
			}
			return options;
		}
		protected TextFormatFlags CreateFormatFlags()
		{
			TextFormatFlags ret = TextFormatFlags.Default;
			switch (this._horizontal)
			{
				case HorizontalAlignment.Left:
				{
					ret = ret;
					break;
				}
				case HorizontalAlignment.Right:
				{
					ret |= TextFormatFlags.Right;
					break;
				}
				case HorizontalAlignment.Center:
				{
					ret |= TextFormatFlags.HorizontalCenter;
					break;
				}
			}
			switch (this._vertical)
			{
				case VerticalAlignment.Top:
				{
					ret = ret;
					break;
				}
				case VerticalAlignment.Center:
				{
					ret |= TextFormatFlags.VerticalCenter;
					break;
				}
				case VerticalAlignment.Bottom:
				{
					ret |= TextFormatFlags.Bottom;
					break;
				}
			}
			if (this._singleLine)
			{
				ret |= TextFormatFlags.SingleLine;
			}
			if (this._endEllipsis)
			{
				ret |= TextFormatFlags.EndEllipsis;
			}
			if (this._wordBreak)
			{
				ret |= TextFormatFlags.WordBreak;
			}
			if (this._wordEllipsis)
			{
				ret |= TextFormatFlags.WordEllipsis;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				ret |= TextFormatFlags.RightToLeft;
			}
			return ret;
		}
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			if (base.Parent != null)
			{
				base.Parent.Invalidate(base.ClientRectangle, false);
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Visible)
			{
				if (this._hText == null)
				{
					this._hText = AeroText.Create(e.Graphics, this.Text, this.Font, this.Padding, new Rectangle(0, 0, base.Size.Width, base.Size.Height), this.ForeColor, this.CreateFormatFlags(), this._mode, this.CreateOptions());
                }
				if (this._needsUpdate)
				{
					this._hText.Draw(e.Graphics, new Rectangle(0, 0, base.Size.Width, base.Size.Height));
					this._hText.Update(e.Graphics, this.Text, this.Font, this.Padding, new Rectangle(0, 0, base.Size.Width, base.Size.Height), this.ForeColor, this.BackColor, this.CreateFormatFlags(), this.CreateOptions());
					this._needsUpdate = false;
					base.Invalidate();
					return;
				}
				this._hText.Draw(e.Graphics, new Rectangle(0, 0, base.Size.Width, base.Size.Height));
			}
		}

	}
}
