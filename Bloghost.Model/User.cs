using System;
using System.Collections.Generic;
using Utilies;

namespace Bloghost.Model
{
    public class User : Entity
    {
        public static class MaxLengthFor
        {
            public const int NAME = 100;
            public const int ABOUT_USER = 3000;
            public const int LINK = 200;
            public const int EMAIL = 150;
            public const int MAX_PASSWORD = 100;
            public const int MIN_PASSWORD = 2;
        }

        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AboutUser { get; set; }
        public virtual IList<Blog> Blogs { get; set; }
        public virtual Role Role { get; set; }
        public string PathToAvatar { get; set; }

        public string AccessName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsLogged { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? LoggedDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            AboutUser = string.Empty;
            AccessName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            IsLogged = false;
            IsBlocked = false;
            IsDeleted = false;
        }

        public bool HasRole(Role role)
        {
            Expect.ArgumentNotNull(role, "role");

            if (Role.Name == Role.Names.ADMINISTRATOR)
                return true;
            if (role.Name == Role.Names.MODERATOR &&
                (Role.Name == Role.Names.BLOGGER || Role.Name == Role.Names.READER))
                return false;
            if (role.Name == Role.Names.BLOGGER && Role.Name == Role.Names.READER)
                return false;
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
