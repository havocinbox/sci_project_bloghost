using Bloghost.Model;
using System.Security.Principal;
using System.Web;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Authentication
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }
        User Login(LoginModel loginModel);
        User Login(string login, string password, bool isPersistent);
        void Logout();
        IPrincipal CurrentUserIdentity { get; }
    }
}
