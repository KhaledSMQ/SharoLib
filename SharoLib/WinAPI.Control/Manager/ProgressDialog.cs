using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinAPI.Const;
using WinAPI.Interface;

namespace WinAPI.Manager
{
    public class ProgressDialog
    {
        public ProgressDialog(IntPtr parentHandle)
        {
            this._parentHandle = parentHandle;
        }        

        private IntPtr _parentHandle;
        private Win32IProgressDialog pd = null;
        private uint _maximum = 100;
        private string _line3 = null;
        private uint _value = 0;
        private string _title = null;
        private string _cancelMessage = null;
        private string _line1 = null;
        private string _line2 = null;
     
        public void ShowDialog(params PROGDLG[] flags)
        {
            if (pd == null)
            {
                pd = (Win32IProgressDialog)new Win32ProgressDialog();

                pd.SetTitle(this._title);
                pd.SetCancelMsg(this._cancelMessage, null);
                pd.SetLine(1, this._line1, false, IntPtr.Zero);
                pd.SetLine(2, this._line2, false, IntPtr.Zero);
                pd.SetLine(3, this._line3, false, IntPtr.Zero);

                PROGDLG dialogFlags = PROGDLG.Normal;
                if (flags.Length != 0)
                {
                    dialogFlags = flags[0];
                    for (var i = 1; i < flags.Length; i++)
                    {
                        dialogFlags = dialogFlags | flags[i];
                    }
                }

                pd.StartProgressDialog(this._parentHandle, null, dialogFlags, IntPtr.Zero);
            }
        }
        public void CloseDialog()
        {
            if (pd != null)
            {
                pd.StopProgressDialog();
                
                pd = null;
            }
        }
        public string DialogTitle
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                if (pd != null)
                {
                    pd.SetTitle(this._title);
                }
            }
        }
        public string CancelMessage
        {
            get
            {
                return this._cancelMessage;
            }
            set
            {
                this._cancelMessage = value;
                if (pd != null)
                {
                    pd.SetCancelMsg(this._cancelMessage, null);
                }
            }
        }
        public string Title
        {
            get
            {
                return this._line1;
            }
            set
            {
                this._line1 = value;
                if (pd != null)
                {
                    pd.SetLine(1, this._line1, false, IntPtr.Zero);
                }
            }
        }
        public string Line1
        {
            get
            {
                return this._line2;
            }
            set
            {
                this._line2 = value;
                if (pd != null)
                {
                    pd.SetLine(2, this._line2, false, IntPtr.Zero);
                }
            }
        }
        public string Line2
        {
            get
            {
                return this._line3;
            }
            set
            {
                this._line3 = value;
                if (pd != null)
                {
                    pd.SetLine(3, this._line3, false, IntPtr.Zero);
                }
            }
        }
        public uint Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
                if (pd != null)
                {
                    pd.SetProgress(this._value, this._maximum);
                }
            }
        }
        public uint Maximum
        {
            get
            {
                return this._maximum;
            }
            set
            {
                this._maximum = value;
                if (pd != null)
                {
                    pd.SetProgress(this._value, this._maximum);
                }
            }
        }
        public bool HasUserCancelled
        {
            get
            {
                if (pd != null)
                {
                    return pd.HasUserCancelled();
                }
                else
                    return false;
            }
        }
    }
}
