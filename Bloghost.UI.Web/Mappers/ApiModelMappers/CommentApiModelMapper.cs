using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class CommentApiModelMapper : EntityMapperBase<CommentApiModel, Comment>
    {
        private readonly ShortUserApiModelMapper _shortUserApiModelMapper = new ShortUserApiModelMapper();
        
        protected override CommentApiModel OnMap(Comment comment)
        {
            var result = new CommentApiModel();

            result.Id = comment.Id;
            result.DateCreated = comment.CreatedDate;
            result.Content = comment.Content;
            result.User = _shortUserApiModelMapper.Map(comment.User);

            return result;
        }

        protected override Comment OnMapBack(CommentApiModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}