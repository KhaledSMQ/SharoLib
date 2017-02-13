using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAPI.Control
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        private string cueBannerText_ = string.Empty;

        [DefaultValue(""), Category("Appearance")]
        public string CueBannerText
        {
            get
            {
                return this.cueBannerText_;
            }
            set
            {
                this.cueBannerText_ = value;
                this.SetCueText();
            }
        }
        private void SetCueText()
        {
            NativeMethod.SendMessage(base.Handle, 5891, IntPtr.Zero, this.cueBannerText_);
        }
        public ComboBox()
        {
            base.FlatStyle = FlatStyle.System;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
