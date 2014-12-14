using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bloghost.Logic.Components;
using Bloghost.Model;

namespace Bloghost.UI.Web.Admin
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.Authentication.IsAuthentication)
            {
                Response.Redirect("http://localhost:2317/login");
                return;
            }

            if (!Authentication.Authentication.CurrentUser.HasRole(new Role { Name = Role.Names.ADMINISTRATOR }))
            {
                Response.Redirect("http://localhost:2317/error");
                return;
            }
            CurrentUserName.Text = Authentication.Authentication.CurrentUser.Name;
            MyProfileHeadLink.NavigateUrl = @"http://localhost:2317/users/" +
                                            Authentication.Authentication.CurrentUser.AccessName;
            SettingLink.NavigateUrl = @"/Pages/Users/UserProfile.aspx?id=" +
                                      Authentication.Authentication.CurrentUser.Id.ToString();
            MyProfileLink.NavigateUrl = SettingLink.NavigateUrl;
            if (MenuNavigation.CurrentActiveMenu.Length >= 2)
                if (MenuNavigation.CurrentActiveMenu[1] == "userlist")
                    UserListMenuItem.CssClass = "active";
                else if (MenuNavigation.CurrentActiveMenu[1] == "newuser")
                    NewUserMenuItem.CssClass = "active";
                else if (MenuNavigation.CurrentActiveMenu[1] == "userprofile")
                    UserProfileMenuItem.CssClass = "active";
        }

        protected void Logout(object sender, EventArgs e)
        {
            var auth = new Authentication.Authentication();
            auth.Logout();
            Response.Redirect("http://localhost:2317/login");
        }
    }
}