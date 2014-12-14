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
    public class UsersController : ApiController
    {
        private readonly UserApiModelMapper _userApiModelMapper = new UserApiModelMapper();
        private readonly ShortUserApiModelMapper _shortUserApiModelMapper = new ShortUserApiModelMapper();

        public IEnumerable<ShortUserApiModel> Get()
        {
            return ServiceFactory.GetUserService().GetAll().Select(x => _shortUserApiModelMapper.Map(x));
        }

        public HttpResponseMessage Get(Guid id)
        {
            var user = ServiceFactory.GetUserService().GetById(id);
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not found");

            return Request.CreateResponse(HttpStatusCode.Created, _userApiModelMapper.Map(user));
        }

        public HttpResponseMessage Put(Guid id, UserApiModel userApiModel)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");

            var user = Authentication.Authentication.CurrentUser;
            if (!user.Id.Equals(id))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This user is not you!");

            userApiModel.Id = id;
            user.Name = userApiModel.Name;
            user.AboutUser = userApiModel.AboutUser;
            user.DateOfBirth = userApiModel.DateOfBirth;
            ServiceFactory.GetUserService().Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(string password)
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var auth = new Authentication.Authentication();
            var user = Authentication.Authentication.CurrentUser;

            if (user.Password != password)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            auth.Logout();
            ServiceFactory.GetUserService().Delete(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
