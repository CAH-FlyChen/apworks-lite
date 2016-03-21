using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Apworks
{
    /// <summary>
    /// Represents the error occurs in the Apworks framework.
    /// </summary>
    public class ApworksException : Exception
    {
        public ApworksException() { }

        public ApworksException(string message)
            : base(message)
        { }

        public ApworksException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ApworksException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        protected ApworksException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
