using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewPostMapper : EntityMapperBase<ViewPostModel, Post>
    {
        protected override ViewPostModel OnMap(Post post)
        {
            var model = new ViewPostModel();
            model.Id = post.Id.ToString();
            model.Blog = EntityMapperFactory.GetViewBlogMapper().Map(post.Blog);
            model.Content = post.Content.Replace("<hr/>", string.Empty).Replace(@"<hr>", string.Empty);
            model.Title = post.Title;
            model.AccessName = post.AccessName;
            model.DateCreate = post.CreatedDate;
            model.Comments = new List<ViewCommentModel>();
            model.Tags = new List<string>();

            if (post.Tags != null)
                model.Tags = new List<string>(post.Tags.Select(x => x.Content));
            if (post.Comments != null)
                model.Comments = new List<ViewCommentModel>(post.Comments.Select(x => EntityMapperFactory.GetViewCommentMapper().Map(x)));
            return model;
        }

        protected override Post OnMapBack(ViewPostModel model)
        {
            var post = new Post();
            Guid newId;
            if (!Guid.TryParse(model.Id, out newId))
                post.Id = Guid.NewGuid();
            else
                post.Id = newId;
            post.Blog = EntityMapperFactory.GetViewBlogMapper().MapBack(model.Blog);
            post.Title = model.Title;
            post.Content = model.Content;
            post.CreatedDate = model.DateCreate;
            var tags = model.Tags.Select(x => new Tag() {Content = x});
            post.Tags = new List<Tag>(tags);
            var comments = model.Comments.Select(x => EntityMapperFactory.GetViewCommentMapper().MapBack(x));
            post.Comments = new List<Comment>(comments);
            return post;
        }
    }
}