using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bloghost.Logic.Components;
using Bloghost.UI.Web.Mappers.ApiModelMappers;

namespace Bloghost.UI.Web.Controllers.ApiControllers
{
    public class AuthController : ApiController
    {
        private readonly ShortUserApiModelMapper _shortUserApiModelMapper = new ShortUserApiModelMapper();
        
        public HttpResponseMessage Get()
        {
            if (!Authentication.Authentication.IsAuthentication)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");

            var user = Authentication.Authentication.CurrentUser;
            return Request.CreateResponse(HttpStatusCode.OK, _shortUserApiModelMapper.Map(user));
        }

        public HttpResponseMessage Get(string email, string password)
        {
            var auth = new Authentication.Authentication();

            var user = ServiceFactory.GetUserService().GetByEmail(email);
            if (user == null || Authentication.Authentication.IsAuthentication)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");

            if (user.Password != password)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");

            auth.Login(email, password, true);
            return Request.CreateResponse(HttpStatusCode.OK, _shortUserApiModelMapper.Map(user));
        }

        public void Delete()
        {
            if (Authentication.Authentication.IsAuthentication)
            {
                var a = new Authentication.Authentication();
                a.Logout();
            }
        }
    }
}
