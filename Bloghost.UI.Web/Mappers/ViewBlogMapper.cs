using System;
using System.Collections.Generic;
using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewBlogMapper : EntityMapperBase<ViewBlogModel, Blog>
    {
        protected override ViewBlogModel OnMap(Blog blog)
        {
            var model = new ViewBlogModel();
            model.Id = blog.Id.ToString();
            model.Title = blog.Title;
            model.AccessName = blog.AccessName;
            model.Description = blog.Description;
            model.PathToAvatar = blog.PathToAvatar;
            model.User = EntityMapperFactory.GetViewUserMapper().Map(blog.User);
            model.Tags = new string[0];

            var tags = new Dictionary<string, int>(5);
            if (blog.Posts != null)
                foreach (var post in blog.Posts)
                {
                    if (post.Tags != null)
                        foreach (var tag in post.Tags)
                        {
                            if (tags.ContainsKey(tag.Content))
                            {
                                tags[tag.Content]++;
                            }
                            else
                            {
                                tags.Add(tag.Content, 1);
                            }
                        }
                }
            foreach (var key in tags.Keys)
            {
                model.Tags = tags.ToList().OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            }
            return model;
        }

        protected override Blog OnMapBack(ViewBlogModel model)
        {
            var blog = new Blog();
            blog.Id = Guid.Parse(model.Id);
            blog.Title = model.Title;
            blog.AccessName = model.AccessName;
            blog.Description = model.Description;
            blog.User = EntityMapperFactory.GetViewUserMapper().MapBack(model.User);
            return blog;
        }
    }
}