using System.Security.Principal;
using Bloghost.Logic;
using Bloghost.Model;

namespace Bloghost.UI.Web.Authentication
{
    public class UserIndentity : IIdentity, IUserProvider
    {
        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Email;
                }
                return "anonym";
            }
        }

        public void Init(string email, IUserService userService)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User = userService.GetByEmail(email);
            }
        }
    }
}