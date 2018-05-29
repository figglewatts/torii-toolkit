using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Exceptions
{
    public class ToriiException : Exception
    {
        public ToriiException()
        {
        }

        public ToriiException(string message) : base(message)
        {
        }

        public ToriiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
