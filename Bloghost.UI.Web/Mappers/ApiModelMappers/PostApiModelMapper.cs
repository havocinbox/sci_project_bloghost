using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class PostApiModelMapper : EntityMapperBase<PostApiModel, Post>
    {
        private readonly ShortUserApiModelMapper _shortUserApiModelMapper = new ShortUserApiModelMapper();
        
        protected override PostApiModel OnMap(Post post)
        {
            var result = new PostApiModel();

            result.Id = post.Id;
            result.BlogId = post.Blog.Id;
            result.User = _shortUserApiModelMapper.Map(post.Blog.User);
            result.Title = post.Title;
            result.Tags = post.Tags.Select(x => x.Content);
            result.Content = post.Content;
            result.DateCreated = post.CreatedDate;
            result.Comments = post.Comments.Select(x => x.Id);

            return result;
        }

        protected override Post OnMapBack(PostApiModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}