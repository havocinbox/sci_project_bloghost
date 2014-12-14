using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class RoleApiModelMapper : EntityMapperBase<RoleApiModel, Role>
    {
        protected override RoleApiModel OnMap(Role role)
        {
            var result = new RoleApiModel();

            result.Id = role.Id;
            result.Name = role.Name;
            result.Users = role.Users.Select(u => u.Id);

            return result;
        }

        protected override Role OnMapBack(RoleApiModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}