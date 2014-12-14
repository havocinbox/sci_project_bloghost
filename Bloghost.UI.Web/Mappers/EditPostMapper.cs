using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class EditPostMapper : EntityMapperBase<EditPostModel, Post>
    {
        protected override EditPostModel OnMap(Post post)
        {
            var model = new EditPostModel();
            model.Id = post.Id.ToString();
            model.Title = post.Title;
            if (post.Tags != null && post.Tags.Count != 0)
            {
                var result = "";
                foreach (var tag in post.Tags)
                {
                    result += tag + ", ";
                }
                model.Tags = result.Substring(0, result.Length - 2);
            }
            else
                model.Tags = "";
            model.Content = post.Content;
            model.BlogId = post.Blog.Id.ToString();
            return model;
        }

        protected override Post OnMapBack(EditPostModel model)
        {
            var post = new Post();
            post.Id = Guid.Parse(model.Id);
            post.Title = model.Title;
            post.Content = model.Content.Replace(@"&lt;hr/&gt;", "<hr/>");

            post.Tags = new List<Tag>(2);
            foreach (var tag in model.Tags.ToLower().Replace(" ", string.Empty).Split(','))
            {
                post.Tags.Add(ServiceFactory.GetTagService().GetByContent(tag));
            }

            post.Blog = ServiceFactory.GetBlogService().GetAll().FirstOrDefault(x => x.Id.Equals(Guid.Parse(model.BlogId)));
            post.AccessName = ServiceFactory.GetPostService().GetAccessName(post);
            return post;
        }
    }
}