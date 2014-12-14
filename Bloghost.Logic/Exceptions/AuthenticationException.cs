using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Exceptions
{
    public class AuthenticationException : Exception
    {
        public Guid UserId { get; protected set; }

        public AuthenticationException()
            : base("User not authorized")
        {
        }

        public AuthenticationException(Guid userId)
            : this()
        {
            UserId = userId;
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, Guid userId)
            : base(message)
        {
            UserId = userId;
        }

        protected AuthenticationException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
