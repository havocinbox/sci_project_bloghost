using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewPostListMapper : EntityMapperBase<ViewPostListModel, Blog>
    {
        protected override ViewPostListModel OnMap(Blog blog)
        {
            var model = new ViewPostListModel();
            model.Posts = new List<ViewPostModel>(10);
            if (blog.Posts != null)
                foreach (var post in blog.Posts)
                {
                    model.Posts.Add(EntityMapperFactory.GetViewPostMapper().Map(post));
                    if (model.Posts.Last().Content.Length >= 300)
                    {
                        model.Posts.Last().Content = model.Posts.Last().Content.Substring(0, 300) + "...";
                    }
                }
            model.Posts = model.Posts.OrderByDescending(post => post.DateCreate).ToList();
            model.Blog = EntityMapperFactory.GetViewBlogMapper().Map(blog);
            return model;
        }

        protected override Blog OnMapBack(ViewPostListModel model)
        {
            throw new NotImplementedException();
        }
    }
}