using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apworks.Querying
{
    public sealed class QueryingException : ApworksException
    {
        public QueryingException() { }

        public QueryingException(string message)
            : base(message)
        { }

        public QueryingException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public QueryingException(string format, params object[] args)
            : base(string.Format(format, args))
        { }
    }
}
