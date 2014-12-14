using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bloghost.Logic.Components;
using Bloghost.Model;

namespace Bloghost.UI.Web.Admin.Pages
{
    public partial class NewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuNavigation.CurrentActiveMenu = new string[] { "users", "newuser" };
        }

        protected void CreateUser(object sender, EventArgs e)
        {
            if (!IsValid)
                return;
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Password.Text) ||
                Password.Text != PasswordAgain.Text)
                return;
            var user = new User();
            user.Name = Name.Text;
            user.Email = Email.Text;
            user.Password = Password.Text;
            user.AboutUser = AboutYour.Text;
            user.AccessName = ServiceFactory.GetUserService().GetAccessName(user);
            user.Role = ServiceFactory.GetRoleService().GetByName(RoleList.SelectedValue);
            ServiceFactory.GetUserService().Save(user);
            Response.Redirect("UserList.aspx");
        }
    }
}