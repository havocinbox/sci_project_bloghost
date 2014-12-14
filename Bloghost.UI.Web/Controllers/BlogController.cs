using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Logic.Components;
using Bloghost.Logic.Exceptions;
using Bloghost.Model;
using Bloghost.UI.Web.Authentication;
using Bloghost.UI.Web.Infrastructure;
using Bloghost.UI.Web.Mappers;
using Bloghost.UI.Web.Models;
using Utilies;

namespace Bloghost.UI.Web.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public ActionResult CreateBlog()
        {
            if (!Authentication.Authentication.IsAuthentication)
                return RedirectToAction("Login", "Home");
            var createBlogModel = new EditBlogModel
                {
                    Id = Guid.NewGuid().ToString(),
                };
            return View("CreateBlog", (EditBlogModel)createBlogModel);
        }

        [HttpPost]
        public ActionResult CreateBlog(EditBlogModel editBlogModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
            {
                ViewBag.HasError = true;
                ViewBag.ErrorMessage = "Fill out all fields.";
                return View(editBlogModel);
            }

            try
            {
                var blog = EntityMapperFactory.GetEditBlogMapper().MapBack(editBlogModel);
                if (
                    ServiceFactory.GetBlogService()
                                  .GetAll()
                                  .FirstOrDefault(x => x.Id.Equals(Guid.Parse(editBlogModel.Id))) != null)
                {
                    ViewBag.HasError = true;
                    ViewBag.ErrorMessage = "Blog with same name exists.";
                    return View(editBlogModel);
                }
                ServiceFactory.GetBlogService().Save(blog);
                var user = blog.User;
                if (user.Role.Name == Role.Names.READER)
                    ServiceFactory.GetRoleService().GetByName(Role.Names.BLOGGER).AddUser(user);
                ServiceFactory.GetUserService().Update(user);

                return Redirect("/blogs/" + blog.AccessName);
            }
            catch (NullReferenceException exception)
            {
                ViewBag.HasError = true;
                ViewBag.ErrorMessage = "Blog does not exist!";
                return View(editBlogModel);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                ViewBag.HasError = true;
                ViewBag.ErrorMessage = exception.Message;
                return View(editBlogModel);
            }
        }

        [HttpPost]
        public ActionResult EditBlog(EditBlogModel editBlogModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
            {
                return Content("Fill out all fields.");
            }

            try
            {
                var editedBlog = ServiceFactory.GetBlogService().GetAll().FirstOrDefault(x => x.Id.Equals(Guid.Parse(editBlogModel.Id)));
                editedBlog.Title = editBlogModel.Title;
                editedBlog.Description = editedBlog.Description;
                ServiceFactory.GetBlogService().Update(editedBlog);

                return Content("");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteBlog(DeleteBlogModel deleteBlogModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
            {
                return Content("");
            }

            if (!ModelState.IsValid)
            {
                return Content("Fill out all fields.");
            }

            var blog =
                ServiceFactory.GetBlogService()
                              .GetAll()
                              .FirstOrDefault(x => x.Id == Guid.Parse(deleteBlogModel.BlogId));
            if (blog == null)
            {
                return Content("Blog with link don't exists :(");
            }

            ServiceFactory.GetBlogService().Delete(blog);
            return Content("");
        }

        public ActionResult ShowListBlogs()
        {
            var urlAction = UrlParse();
            if (urlAction == null || urlAction == "all")
            {
                var model = EntityMapperFactory.GetViewBlogListMapper().Map(null);
                return View(model);
            }
            if (urlAction == "popular")
            {
                var model = new ViewBlogListModel();
                foreach (var blog in ServiceFactory.GetBlogService().GetPopularBlogs())
                {
                    model.BlogList.Add(EntityMapperFactory.GetViewBlogMapper().Map(blog));
                }
                return View(model);
            }
            if (urlAction == "new")
            {
                var model = new ViewBlogListModel();
                foreach (var blog in ServiceFactory.GetBlogService().GetNewBlogs())
                {
                    model.BlogList.Add(EntityMapperFactory.GetViewBlogMapper().Map(blog));
                }
                return View(model);
            }
            if (urlAction == "create")
                return CreateBlog();
            return null;
        }

        private string UrlParse()
        {
            var url = Request.Url;
            var queryString = string.Join(string.Empty, url.ToString().Split('?').Skip(1));
            var param = HttpUtility.ParseQueryString(queryString);

            if (!string.IsNullOrEmpty(param.Get("action")))
            {
                var action = param.Get("action");
                if (action == "create")
                    return "create";
                if (action == "show")
                {
                    return param.Get("filter");
                }
            }

            if (!string.IsNullOrEmpty(param.Get("by")))
            {
                var action = param.Get("by");
                if (!string.IsNullOrWhiteSpace(action))
                    return action;
            }
            return null;
        }
    }
}
