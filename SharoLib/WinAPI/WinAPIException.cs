using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI
{
    [Serializable]
    public class WinAPIException : Exception
    {
        public int ErrorCode { get; private set; }

        public WinAPIException() { }
        public WinAPIException(string message) : base(message) { }
        public WinAPIException(string message, int code) : base(message) { this.ErrorCode = code; }
        public WinAPIException(string message, Exception inner) : base(message, inner) { }
        public WinAPIException(string message, Exception inner, int code) : base(message, inner) { this.ErrorCode = code; }
        protected WinAPIException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
