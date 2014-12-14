using Bloghost.Model;
using System.Security.Principal;
using System.Web;

namespace Bloghost.UI.Web.Admin.Authentication
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }
        User Login(string login, string password, bool isPersistent);
        void Logout();
        IPrincipal CurrentUserIdentity { get; }
    }
}
