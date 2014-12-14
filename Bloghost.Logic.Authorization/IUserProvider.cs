using Bloghost.Model;

namespace Bloghost.UI.Web.Authentication
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}
