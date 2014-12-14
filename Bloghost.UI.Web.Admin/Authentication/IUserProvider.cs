using Bloghost.Model;

namespace Bloghost.UI.Web.Admin.Authentication
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}
