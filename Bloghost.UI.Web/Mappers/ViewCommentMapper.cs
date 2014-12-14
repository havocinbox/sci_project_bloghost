using System;
using System.Linq;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewCommentMapper : EntityMapperBase<ViewCommentModel, Comment>
    {
        protected override ViewCommentModel OnMap(Comment comment)
        {
            var model = new ViewCommentModel();
            model.Id = comment.Id.ToString();
            model.PostId = comment.Post.Id.ToString();
            model.Content = comment.Content;
            model.User = EntityMapperFactory.GetViewUserMapper().Map(comment.User);
            model.CreateDate = comment.CreatedDate;
            if (comment.ParentCommet != null)
                model.ParentComment = EntityMapperFactory.GetViewCommentMapper().Map(comment.ParentCommet);
            return model;
        }

        protected override Comment OnMapBack(ViewCommentModel model)
        {
            var comment = new Comment();
            Guid newId;
            if (!Guid.TryParse(model.Id, out newId))
                comment.Id = Guid.NewGuid();
            else
                comment.Id = newId;
            comment.Content = model.Content;
            comment.Post =
                ServiceFactory.GetPostService().GetAll().FirstOrDefault(x => x.Id.Equals(Guid.Parse(model.Id)));
            comment.User = EntityMapperFactory.GetViewUserMapper().MapBack(model.User);
            if (comment.ParentCommet != null)
                comment.ParentCommet = EntityMapperFactory.GetViewCommentMapper().MapBack(model.ParentComment);
            return comment;
        }
    }
}