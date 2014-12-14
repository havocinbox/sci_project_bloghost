using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Bloghost.Model;
using Bloghost.Logic.Components;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Authentication
{
    public class Authentication : IAuthentication
    {
        private const string COOKIE_NAME = "__AUTH_COOKIE";
        private IPrincipal _currentUser;
        public HttpContext HttpContext { get; set; }

        public static bool IsAuthentication
        {
            get
            {
                var authCookie = HttpContext.Current.Request.Cookies.Get(COOKIE_NAME);
                var result = (authCookie != null && authCookie.Value != string.Empty);
                return result;
            }
        }

        public static User CurrentUser
        {
            get
            {
                var authCookie = HttpContext.Current.Request.Cookies.Get(COOKIE_NAME);
                if (authCookie != null && authCookie.Value != string.Empty)
                {
                    var decTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var user = ServiceFactory.GetUserService().GetById(Guid.Parse(decTicket.UserData));
                    return user;
                }
                return null;
            }
        }

        public IPrincipal CurrentUserIdentity
        {
            get { return _currentUser ?? (_currentUser = CreateUser()); }
        }

        public Authentication()
        {
            HttpContext = HttpContext.Current;
        }

        public User Login(LoginModel loginModel)
        {
            if (loginModel != null)
                return Login(loginModel.Email, loginModel.Password, loginModel.RememberMe);
            return null;
        }

        public User Login(string login, string password, bool isPersistent)
        {
            var user = ServiceFactory.GetMembershipService().LoginUser(login, password);
            if (user != null)
            {
                CreateCookie(user, isPersistent);
                _currentUser = CreateUser();
            }
            return user;
        }

        public void Logout()
        {
            var httpCookie = HttpContext.Response.Cookies[COOKIE_NAME];
            if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
            {
                var decTicket = FormsAuthentication.Decrypt(httpCookie.Value);
                ServiceFactory.GetMembershipService().LogoutUser(Guid.Parse(decTicket.UserData));
                httpCookie.Value = string.Empty;
            }
        }

        private void CreateCookie(User user, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  user.AccessName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  user.Id.ToString(),
                  FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);

            var authCookie = new HttpCookie(COOKIE_NAME)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            HttpContext.Response.Cookies.Set(authCookie);
        }

        private IPrincipal CreateUser()
        {
            var authCookie = HttpContext.Request.Cookies.Get(COOKIE_NAME);
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return new UserProvider(ticket.Name, ServiceFactory.GetUserService());
            }
            return new UserProvider(null, ServiceFactory.GetUserService());
        }
    }
}