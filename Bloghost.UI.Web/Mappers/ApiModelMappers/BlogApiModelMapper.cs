using System.Collections.Generic;
using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class BlogApiModelMapper : EntityMapperBase<BlogApiModel, Blog>
    {
        private readonly ShortUserApiModelMapper _shortUserApiModelMapper = new ShortUserApiModelMapper();

        protected override BlogApiModel OnMap(Blog blog)
        {
            var result = new BlogApiModel();

            result.Id = blog.Id;
            result.Title = blog.Title;
            result.DateCreated = blog.CreatedDate;
            result.Description = blog.Description;
            result.User = _shortUserApiModelMapper.Map(blog.User);
            result.Posts = blog.Posts.Select(x => x.Id);

            return result;
        }

        protected override Blog OnMapBack(BlogApiModel blog)
        {
            var result = new Blog();

            result.Id = blog.Id;
            result.Title = blog.Title;
            result.CreatedDate = blog.DateCreated;
            result.Description = blog.Description;
            result.Posts = new List<Post>();

            return result;
        }
    }
}