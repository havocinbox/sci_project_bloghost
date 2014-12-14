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
    public class UserDeletedException : Exception
    {
        public User DeletedUser { private set; get; }

        public UserDeletedException()
            : base()
        {
        }

        public UserDeletedException(string message)
            : base(message)
        {
        }

        public UserDeletedException(User user)
            : base("User is blocked (" + user.BlockedDate.Value + ")")
        {
            DeletedUser = user;
        }

        public UserDeletedException(string message, User user)
            : base(message)
        {
            DeletedUser = user;
        }
    }
}
