using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hackathon.Domain.Exceptions
{
    public class HackathonException : Exception
    {
        public HackathonException()
        {
        }

        public HackathonException(string message) : base(message)
        {
        }

        public HackathonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HackathonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
