using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Authentication;
using Bloghost.UI.Web.Infrastructure;
using Utilies;
using WebMatrix.WebData;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAuthentication _authorization = new Authentication.Authentication();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (!Authentication.Authentication.IsAuthentication)
                return View(new LoginModel());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel loginModel, string redirectUrl = null)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(loginModel);

                var user = _authorization.Login(loginModel);
                if (user != null && !user.IsBlocked)
                {
                    if (redirectUrl == null)
                        return RedirectToAction("ShowListBlogs", "Blog");
                    return RedirectToLocal(redirectUrl);
                }
                if (user != null && user.IsBlocked)
                {
                    ViewBag.ErrorMessage = "User is blocked.";
                    _authorization.Logout();
                }
                else
                {
                    ViewBag.ErrorMessage = "Username or password incorrect.";
                }
                ViewBag.HasError = true;
                loginModel.Password = string.Empty;
                return View(loginModel);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                ViewBag.HasError = true;
                ViewBag.ErrorMessage = "Email or password is invalid.";
                loginModel.Password = string.Empty;
                return View(loginModel);
            }
        }

        [HttpGet]
        public ActionResult Logout(string redirectUrl = null)
        {
            _authorization.Logout();
            if (redirectUrl == null)
                return RedirectToAction("ShowListBlogs", "Blog");
            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
