using System.Security.Principal;
using Bloghost.Logic;

namespace Bloghost.UI.Web.Authentication
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
            if (_userIdentity.User == null)
            {
                return false;
            }
            return true; // _userIdentity.User.InRoles(role);
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