using System;
using System.Collections.Generic;
using System.Linq;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewUserMapper : EntityMapperBase<ViewUserModel, User>
    {
        protected override ViewUserModel OnMap(User entity)
        {
            var model = new ViewUserModel();

            model.Id = entity.Id.ToString();
            model.Name = entity.Name;
            model.AboutUser = entity.AboutUser;
            model.DateOfBirth = entity.DateOfBirth;
            model.IsBlocked = entity.IsBlocked;
            model.IsDeleted = entity.IsDeleted;
            model.IsLogged = entity.IsLogged;
            model.DeletedDate = entity.DeletedDate;
            model.BlockedDate = entity.BlockedDate;
            model.LoggedDate = entity.LoggedDate;
            model.AccessName = entity.AccessName;
            model.PathToAvatar = entity.PathToAvatar;
            return model;
        }

        protected override User OnMapBack(ViewUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}