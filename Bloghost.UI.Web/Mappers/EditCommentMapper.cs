using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class EditCommentMapper : EntityMapperBase<EditCommentModel, Comment>
    {
        protected override EditCommentModel OnMap(Comment blog)
        {
            throw new NotImplementedException();
        }

        protected override Comment OnMapBack(EditCommentModel model)
        {
            var comment = new Comment();
            comment.Content = model.Message;
            comment.Post = ServiceFactory.GetPostService().GetById(Guid.Parse(model.PostId));
            comment.User = Authentication.Authentication.CurrentUser;
            comment.Id = Guid.NewGuid();

            return comment;
        }
    }
}