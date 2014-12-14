using System.Security.Principal;
using Bloghost.Logic;
using Bloghost.Model;

namespace Bloghost.UI.Web.Admin.Authentication
{
    public class UserProvider : IPrincipal
    {
        private readonly UserIndentity _userIdentity;

        public IIdentity Identity
        {
            get { return _userIdentity; }
        }

        public bool IsInRole(string role)
        {
            return _userIdentity.User.HasRole(new Role { Name = role });
        }

        public UserProvider(string loginOrEmail, IUserService userService)
        {
            _userIdentity = new UserIndentity();
            _userIdentity.Init(loginOrEmail, userService);
        }

        public override string ToString()
        {
            return _userIdentity.Name;
        }
    }
}