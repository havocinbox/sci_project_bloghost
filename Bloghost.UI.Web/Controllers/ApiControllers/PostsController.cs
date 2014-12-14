using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bloghost.Logic.Components;
using Bloghost.UI.Web.Mappers.ApiModelMappers;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Controllers.ApiControllers
{
    public class PostsController : ApiController
    {
        private readonly PostApiModelMapper _postApiModelMapper = new PostApiModelMapper();

        public IEnumerable<PostApiModel> Get(Guid id)
        {
            return ServiceFactory.GetPostService().GetAll().Where(x => x.Blog.Id == id).Select(x => _postApiModelMapper.Map(x));
        }
    }
}
