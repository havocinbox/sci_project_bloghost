using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models.ApiModels;

namespace Bloghost.UI.Web.Mappers.ApiModelMappers
{
    public class UserApiModelMapper : EntityMapperBase<UserApiModel, User>
    {
        protected override UserApiModel OnMap(User user)
        {
            var result = new UserApiModel();

            result.Id = user.Id;
            result.Name = user.Name;
            result.DateOfRegistration = user.CreatedDate;
            result.DateOfBirth = user.DateOfBirth;
            result.AboutUser = user.AboutUser;
            result.RoleName = user.Role.Name;
            result.Blogs = user.Blogs.Select(x => x.Id);

            return result;
        }

        protected override User OnMapBack(UserApiModel user)
        {
            throw new System.NotImplementedException();
        }
    }
}