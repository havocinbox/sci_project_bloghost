using System;
using System.Collections.Generic;
using Utilies;

namespace Bloghost.Model
{
    public class Role : Entity, IEquatable<Role>
    {
        public static class Names
        {
            public const string ADMINISTRATOR = "Administrator";
            public const string MODERATOR = "Moderator";
            public const string BLOGGER = "Blogger";
            public const string READER = "Reader";
        }

        public static class MaxLengthFor
        {
            public const int NAME = 25;
        }

        public string Name { get; set; }
        public virtual IList<User> Users { get; set; }
 
        public void AddUser(User user)
        {
            Expect.ArgumentNotNull(user, "user");

            if (user.Role != null)
            {
                user.Role.Users.Remove(user);
            }
            if (Users == null)
                Users = new List<User>(1);
            Users.Add(user);
            user.Role = this;
        }


        public bool Equals(Role other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (other.Name == Name)
                return true;
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
