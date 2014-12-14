using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class ShortUserApiModelMapper : EntityMapperBase<ShortUserApiModel, User>
    {
        protected override ShortUserApiModel OnMap(User user)
        {
            var result = new ShortUserApiModel();

            result.Id = user.Id;
            result.Name = user.Name;
            result.RoleName = user.Role.Name;

            return result;
        }

        protected override User OnMapBack(ShortUserApiModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}