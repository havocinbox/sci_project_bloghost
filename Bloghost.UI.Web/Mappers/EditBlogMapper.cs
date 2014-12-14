using System;
using System.Linq;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Infrastructure;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class EditBlogMapper : EntityMapperBase<EditBlogModel, Blog>
    {
        protected override EditBlogModel OnMap(Blog blog)
        {
            var model = new EditBlogModel();
            model.Id = blog.Id.ToString();
            model.Title = blog.Title;
            model.Description = blog.Description;
            return model;
        }

        protected override Blog OnMapBack(EditBlogModel model)
        {
            var blog = new Blog
                {
                    Id = Guid.Parse(model.Id),
                    Title = model.Title,
                    Description = model.Description,
                    User = Authentication.Authentication.CurrentUser
                };
            if (model.File != null)
            {
                blog.PathToAvatar = FileLoader.ImageUpload(model.File, blog.Id.ToString());
            }
            blog.AccessName = ServiceFactory.GetBlogService().GetAccessName(blog);

            return blog;
        }
    }
}