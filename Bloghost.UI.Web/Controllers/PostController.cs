using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Authentication;
using Bloghost.UI.Web.Infrastructure;
using Bloghost.UI.Web.Mappers;
using Bloghost.UI.Web.Models;
using Utilies;

namespace Bloghost.UI.Web.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult CreatePost(string blogname)
        {
            try
            {
                var model = new EditPostModel();
                model.Id = Guid.NewGuid().ToString();
                var blog = ServiceFactory.GetBlogService().GetAll().FirstOrDefault(x => x.AccessName == blogname);
                model.BlogId = EntityMapperFactory.GetViewBlogMapper().Map(blog).Id;

                return View("CreatePost", model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreatePost(EditPostModel editPostModel)
        {
            try
            {
                var post = EntityMapperFactory.GetEditPostMapper().MapBack(editPostModel);

                if (post.Blog == null)
                {
                    return Content("This blog does not exist! O_O");
                }
                if (!post.Blog.User.Id.Equals(Authentication.Authentication.CurrentUser.Id))
                {
                    return Content("You are not author of this blog!");
                }
                ServiceFactory.GetPostService().Save(post);

                return Content("");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult EditPost(string blogname, string postId)
        {
            try
            {
                var model = EntityMapperFactory.GetEditPostMapper().Map(ServiceFactory.GetPostService().GetById(Guid.Parse(postId)));

                return View("EditPost", model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult EditPost(EditPostModel model)
        {
            if (!ModelState.IsValid)
                return Content("All fileds cannot be empty.");

            try
            {
                var post = EntityMapperFactory.GetEditPostMapper().MapBack(model);

                if (post.Blog == null)
                {
                    return Content("This blog does not exist! O_O");
                }
                if (!post.Blog.User.Id.Equals(Authentication.Authentication.CurrentUser.Id))
                {
                    return Content("You are not author of this blog!");
                }
                var existPost = ServiceFactory.GetPostService().GetById(post.Id);
                existPost.Title = post.Title;
                existPost.Tags = post.Tags;
                existPost.Content = post.Content;

                ServiceFactory.GetPostService().Update(existPost);

                return Content("");
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return Content(exception.Message);
            }
        }

        public ActionResult ShowListPosts(string blogname)
        {
            var param = UrlParse();
            if (param != null)
            {
                if (param.ContainsKey("show_description"))
                    return ViewBlog(blogname);
                if (param.ContainsKey("create"))
                    return CreatePost(blogname);
                if (param.ContainsKey("edit"))
                    return EditPost(blogname, param.ToArray()[0].Value);
            }
            try
            {
                var blog = ServiceFactory.GetBlogService().GetAll().FirstOrDefault(x => x.AccessName == blogname);
                var model = EntityMapperFactory.GetViewPostListMapper().Map(blog);
                return View(model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ViewPost(string postname)
        {
            try
            {
                var model =
                    EntityMapperFactory.GetViewPostMapper()
                                       .Map(
                                           ServiceFactory.GetPostService()
                                                         .GetAll()
                                                         .FirstOrDefault(x => x.AccessName == postname));

                return View(model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ViewBlog(string blogname)
        {
            try
            {
                var blog =
                    ServiceFactory.GetBlogService().GetAll().FirstOrDefault(x => x.AccessName == blogname);
                var model = EntityMapperFactory.GetViewBlogMapper().Map(blog);
                return View("ViewBlog", model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeletePost(DeleteBlogModel model)
        {
            if (!Authentication.Authentication.IsAuthentication)
            {
                return Content("");
            }

            if (!ModelState.IsValid)
            {
                return Content("Fill out all fields.");
            }

            var post =
                ServiceFactory.GetPostService()
                              .GetAll()
                              .FirstOrDefault(x => x.Id == Guid.Parse(model.BlogId));
            if (post == null)
            {
                return Content("Blog with link don't exists :(");
            }

            ServiceFactory.GetPostService().Delete(post);
            return Content("");
        }

        [HttpPost]
        public ActionResult AddComment(EditCommentModel editComment)
        {
            if (!ModelState.IsValid)
                return Content("");

            try
            {
                var comment = EntityMapperFactory.GetEditCommentMapper().MapBack(editComment);
                var post = ServiceFactory.GetPostService().GetById(Guid.Parse(editComment.PostId));
                if (post.Comments == null)
                    post.Comments = new List<Comment>(1);
                post.Comments.Add(comment);

                ServiceFactory.GetCommentService().Save(comment);
                ServiceFactory.GetPostService().Update(post);


                var newCommentHtml = @"<li id='comment-1'>
                            <footer class='comment-meta'>
                                <a href='#' rel='external nofollow' title='" + comment.User.Name + @"'><img alt='" + comment.User.Name + @"' src='" +
                               Url.Content("~/Images/" + comment.User.PathToAvatar)
                               + @"' class='avatar' /></a>
                                <div class='comment-meta'>
                                    <span class='comment-author vcard'><cite title='LinkURLofauthor'><a href='#' title='" +
                               comment.User.Name + @"' class='url' rel='external nofollow'>" + comment.User.Name +
                @"</a></cite></span><time class='published' datetime='2013-10-10T09:57:00Z'>" + comment.CreatedDate.ToLongDateString() +
            @"</time> | <a class='permalink' href='#' title='Permalink to comment 1'>Permalink</a> | <a class='comment-reply-link' href='#'>Reply</a>
                                </div>
                            </footer>
                            <div class='text'>
                                <p>" + comment.Content + @"</p>
                            </div>
                        </li>";

                return Content(newCommentHtml);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                return RedirectToAction("Error", "Home");
            }
        }

        private Dictionary<string, string> UrlParse()
        {
            var url = Request.Url;
            var queryString = string.Join(string.Empty, url.ToString().Split('?').Skip(1));
            var param = HttpUtility.ParseQueryString(queryString);

            if (!string.IsNullOrEmpty(param.Get("action")))
            {
                var action = param.Get("action");
                if (action == "create")
                    return new Dictionary<string, string> { { "create", "" } };
                if (action == "edit")
                    return new Dictionary<string, string> { { "edit", param.GetValues("id")[0] } };
                if (action == "show_description")
                    return new Dictionary<string, string> { { "show_description", "" } };
            }

            if (!string.IsNullOrEmpty(param.Get("by")))
            {
                var action = param.Get("by");
                if (!string.IsNullOrWhiteSpace(action))
                    return null;
            }
            return null;
        }
    }
}
