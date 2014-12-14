using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Infrastructure;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class RegisterUserMapper : EntityMapperBase<RegisterUserModel, User>
    {
        protected override RegisterUserModel OnMap(User entity)
        {
            throw new NotImplementedException();
        }

        protected override User OnMapBack(RegisterUserModel model)
        {
            var user = new User();
            int day, month, year;
            user.Id = Guid.NewGuid();
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            user.AboutUser = model.AboutUser;
            user.AccessName = ServiceFactory.GetUserService().GetAccessName(user);
            if (Int32.TryParse(model.DateOfBirthDay, out day) &&
                Int32.TryParse(model.DateOfBirthMonth, out month) &&
                Int32.TryParse(model.DateOfBirthYear, out year))
            {
                user.DateOfBirth = new DateTime(year, month, day);
            }

            if (model.File != null)
            {
                user.PathToAvatar = FileLoader.ImageUpload(model.File, user.Id.ToString());
            }

            ServiceFactory.GetRoleService().GetByName(Role.Names.READER).AddUser(user);

            return user;
        }
    }
}