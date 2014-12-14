using System.Collections.Generic;
using Bloghost.Logic.Components.Validation;
using Bloghost.Model;
using Bloghost.Data;
using System;
using System.Linq;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IDataAccessObject<User, Guid> dataAccessObject)
            : base(dataAccessObject, new UserValidator())
        {
        }

        public string GetAccessName(User user)
        {
            Expect.ArgumentNotNull(user, "user");
            Expect.ArgumentNotWhiteSpaceString(user.Name, "User.Name");

            string link;
            string linkBackup = link = user.Name.ToLower().Trim().Trim(new[] { '&', ',', '.', '/', '\\', '|', '@', '=', '?' }).Replace(" ", string.Empty).Replace("&", "_");
            var i = 0;
            while (GetAll().FirstOrDefault(x => x.AccessName == link) != null)
            {
                link = linkBackup + i;
                i++;
            }

            return link;
        }
    
        public User GetByEmail(string email)
        {
            Expect.ArgumentNotWhiteSpaceString(email, "email");

            return GetAll().FirstOrDefault(x => x.Email == email);
        }

        public User GetByAccessName(string accessName)
        {
            Expect.ArgumentNotNull(accessName, "accessName");

            return GetAll().FirstOrDefault(x => x.AccessName == accessName);
        }

        public IEnumerable<User> GetTop5Users()
        {
            return GetAll().Where(user => user.Blogs != null).OrderByDescending(user => user.Blogs.Sum(blog => blog.Rating)).Take(5);
        }
    }
}
