using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAPI.Data;

namespace WinAPI.Control
{
    public abstract class IThemeTextOption
    {
        internal abstract void Apply(ref DTTOPTS options);
    }
}
