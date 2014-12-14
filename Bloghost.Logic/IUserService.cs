using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Logic
{
    public interface IUserService : IService<User>
    {
        string GetAccessName(User user);
        User GetByEmail(string email);
        User GetByAccessName(string accessName);
        IEnumerable<User> GetTop5Users();
    }
}
