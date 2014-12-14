using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Bloghost.UI.Web.Admin
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.EnableFriendlyUrls();
            routes.Ignore("*.aspx");
            routes.MapPageRoute("UserProfile", "users/{id}", "~/Users/UserProfile.aspx");
            routes.MapPageRoute("NewUser", "user/new", "~/Pages/Users/NewUser.aspx");
            routes.MapPageRoute("UserList", "users", "~/Pages/Users/UserList.aspx");
            routes.MapPageRoute("Statistics", "", "~/Pages/Statistics.aspx");
        }
    }
}
