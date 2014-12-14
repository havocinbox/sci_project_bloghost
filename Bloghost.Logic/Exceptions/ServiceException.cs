using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Exceptions
{
    [Serializable()]
    public class ServiceException : Exception
    {
        public Type ServiceType { get; protected set; }

        public ServiceException()
            : base()
        {
        }

        public ServiceException(Type serviceType)
            : base()
        {
            ServiceType = serviceType;
        }

        public ServiceException(string message)
            : base(message)
        {
        }

        public ServiceException(string message, Type serviceType)
            : base(message)
        {
            ServiceType = serviceType;
        }

        public ServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ServiceException(string message, Exception inner, Type serviceType)
            : base(message, inner)
        {
            ServiceType = serviceType;
        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
        {
        }
    }

    [Serializable()]
    public class ServiceException<TService> : ServiceException
    {
        public ServiceException()
            : base()
        {
            ServiceType = typeof(TService);
        }
    }
}
