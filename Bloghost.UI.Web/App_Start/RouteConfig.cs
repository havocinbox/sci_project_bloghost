using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bloghost.UI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Error",
                url: "error",
                defaults: new { controller = "Home", action = "Error" }
                );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Profile", action = "CreateProfile" }
                );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
                );

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Home", action = "Search" }
                );

            //users
            routes.MapRoute(
                name: "UserDescription",
                url: "users/{username}",
                defaults: new { controller = "Profile", action = "ViewProfile" }
                );

            routes.MapRoute(
                name: "UserList",
                url: "users",
                defaults: new { controller = "Profile", action = "ShowListProfiles" }
                );

            //posts
            routes.MapRoute(
                name: "ViewPost",
                url: "blogs/{blogname}/{postname}",
                defaults: new { controller = "Post", action = "ViewPost" }
                );
            
            routes.MapRoute(
                name: "PostList",
                url: "blogs/{blogname}",
                defaults: new { controller = "Post", action = "ShowListPosts" }
                );

            //blogs
            routes.MapRoute(
                name: "BlogList",
                url: "blogs",
                defaults: new { controller = "Blog", action = "ShowListBlogs" }
                );

            //all
            //todo: delete this
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "ShowListBlogs", id = UrlParameter.Optional }
                );
        }
    }
}