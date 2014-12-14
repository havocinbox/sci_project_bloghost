using System;
using System.Web.Mvc;
using Bloghost.Logic.Components;
using Bloghost.Logic.Exceptions;
using Bloghost.Model;
using Bloghost.UI.Web.Authentication;
using Bloghost.UI.Web.Mappers;
using Bloghost.UI.Web.Models;
using Utilies;

namespace Bloghost.UI.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAuthentication _authorization = new Authentication.Authentication();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateProfile()
        {
            if (Authentication.Authentication.IsAuthentication)
                return RedirectToAction("ViewProfile", Authentication.Authentication.CurrentUser.AccessName);
            return View(new RegisterUserModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateProfile(RegisterUserModel registrationUserModel)
        {
            if (Authentication.Authentication.IsAuthentication)
                return Redirect("/users/" + Authentication.Authentication.CurrentUser.AccessName);

            if (!ModelState.IsValid)
                return View(registrationUserModel);

            try
            {
                var user = EntityMapperFactory.GetRegisterUserMapper().MapBack(registrationUserModel);
                user = ServiceFactory.GetMembershipService().RegisterUser(user);

                _authorization.Login(user.Email, user.Password, true);
                return Redirect("/users/" + user.AccessName);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                ModelState.AddModelError("All", exception.Message);
                ViewBag.HasError = true;
                ViewBag.ErrorMessage = "Panic error! Runtime error on the server side. Everything is bad :(";
                return View(registrationUserModel);
            }

        }

        [AllowAnonymous]
        public ActionResult ShowListProfiles()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewProfile(string username)
        {
            try
            {
                var model = EntityMapperFactory.GetViewUserMapper().Map(ServiceFactory.GetUserService().GetByAccessName(username));
                return View(model);
            }
            catch (ModelNotFoundException<User>)
            {
                ViewBag.BackUrl = Request.Url;
                return RedirectToAction("Error", "Home");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return Content("Fill out all fields.");

            try
            {
                var user = ServiceFactory.GetUserService().GetById(Authentication.Authentication.CurrentUser.Id);
                if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmPassword)
                {
                    return Content("New password does not equals to confirm password.");
                }
                if (resetPasswordModel.OldPassword != user.Password)
                {
                    return Content("Old password is incorrect.");
                }

                user.Password = resetPasswordModel.NewPassword;
                ServiceFactory.GetUserService().Update(user);
                return Content("");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteProfile(DeleteUserModel deleteProfileModel)
        {
            try
            {
                var user = ServiceFactory.GetUserService().GetById(Guid.Parse(deleteProfileModel.UserId));
                if (user.Id != Authentication.Authentication.CurrentUser.Id)
                    throw new LogicException("This is not your profile!");

                if (user.Password == deleteProfileModel.Password)
                {
                    _authorization.Logout();
                    ServiceFactory.GetUserService().Delete(user);
                    return Content("");
                }

                return Content("Password is incorrect. Your profile will not be deleted.");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        public ActionResult UpdateProfile(EditUserModel updateUserModel)
        {
            if (!ModelState.IsValid)
                return Content("Fill out all fields.");

            try
            {
                var user = ServiceFactory.GetUserService().GetById(Guid.Parse(updateUserModel.Id));
                if (!user.Id.Equals(Guid.Parse(updateUserModel.Id)))
                {
                    throw new LogicException("This is not your profile!");
                }

                user.Name = updateUserModel.Name;
                user.AboutUser = updateUserModel.AboutUser;

                int day, month, year;
                if (Int32.TryParse(updateUserModel.DateOfBirthDay, out day) &&
                    Int32.TryParse(updateUserModel.DateOfBirthMonth, out month) &&
                    Int32.TryParse(updateUserModel.DateOfBirthYear, out year))
                {
                    user.DateOfBirth = new DateTime(year, month, day);
                }

                ServiceFactory.GetUserService().Update(user);
                return Content("");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }
    }
}
