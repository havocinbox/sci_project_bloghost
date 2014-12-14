using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bloghost.Logic.Components;
using Bloghost.UI.Web.Mappers.ApiModelMappers;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Controllers.ApiControllers
{
    public class BlogsController : ApiController
    {
        private readonly BlogApiModelMapper _blogApiModelMapper = new BlogApiModelMapper();

        public IEnumerable<BlogApiModel> Get()
        {
            return ServiceFactory.GetBlogService().GetAll().Select(x => _blogApiModelMapper.Map(x));
        }

        public HttpResponseMessage Get(Guid id)
        {
            var blog = ServiceFactory.GetBlogService().GetById(id);
            if (blog == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not found");

            return Request.CreateResponse(HttpStatusCode.Created, _blogApiModelMapper.Map(blog));
        }

        public HttpResponseMessage Post(BlogApiModel blogApiModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");

            blogApiModel.Id = Guid.NewGuid();
            blogApiModel.DateCreated = DateTime.Now;
            var blog = _blogApiModelMapper.MapBack(blogApiModel);
            blog.User = Authentication.Authentication.CurrentUser;
            blog.AccessName = ServiceFactory.GetBlogService().GetAccessName(blog);
            ServiceFactory.GetBlogService().Save(blog);
            return Request.CreateResponse(HttpStatusCode.Created, blogApiModel);
        }

        public HttpResponseMessage Put(Guid id, BlogApiModel blogApiModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");

            var user = Authentication.Authentication.CurrentUser;
            var blog = ServiceFactory.GetBlogService().GetById(id);
            if (blog == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Blog don't found");
            if (!user.Id.Equals(blog.User.Id))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This user is not you");

            blog.Title = blogApiModel.Title;
            blog.Description = blogApiModel.Description;
            ServiceFactory.GetBlogService().Update(blog);

            return Request.CreateResponse(HttpStatusCode.OK, blog);
        }

        public HttpResponseMessage Delete(Guid id)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var user = Authentication.Authentication.CurrentUser;

            var blog = ServiceFactory.GetBlogService().GetById(id);
            if (!user.Id.Equals(blog.User.Id))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This user is not you");

            ServiceFactory.GetBlogService().Delete(blog);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
