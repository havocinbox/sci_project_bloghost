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
    public class UserBlockedException : Exception
    {
        public User BlockedUser { private set; get; }

        public UserBlockedException()
            : base()
        {
        }

        public UserBlockedException(string message)
            : base(message)
        {
        }

        public UserBlockedException(User user)
            : base("User is blocked (" + user.BlockedDate.Value + ")")
        {
            BlockedUser = user;
        }

        public UserBlockedException(string message, User user)
            : base(message)
        {
            BlockedUser = user;
        }
    }
}
