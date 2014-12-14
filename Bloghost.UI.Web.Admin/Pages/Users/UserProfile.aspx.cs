using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bloghost.Logic.Components;
using Bloghost.Model;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace Bloghost.UI.Web.Admin.Pages.Users
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            MenuNavigation.CurrentActiveMenu = new string[] { "users", "userprofile" };
            var id = Request.Params["id"];
            if (id == null)
            {
                Response.Redirect("UserList.aspx");
                return;
            }
            var user = ServiceFactory.GetUserService().GetById(Guid.Parse(id));
            if (user == null)
            {
                Response.Redirect("UserList.aspx");
                return;
            }

            if (user.PathToAvatar != null)
                UserAvatar.ImageUrl = @"~/Images/" + user.PathToAvatar;
            NameTextBox.Text = user.Name;
            Email.Text = user.Email;
            UserId.Text = user.Id.ToString();
            UserBlockedFlag.Checked = user.IsBlocked;
            Password.Text = user.Password;
            AboutYour.Text = user.AboutUser;
            if (user.Role == null)
                user.Role = ServiceFactory.GetRoleService().GetByName(Role.Names.READER);
            if (user.Role.Name == Role.Names.READER)
                RoleList.SelectedIndex = 0;
            else if (user.Role.Name == Role.Names.BLOGGER)
                RoleList.SelectedIndex = 1;
            else if (user.Role.Name == Role.Names.MODERATOR)
                RoleList.SelectedIndex = 2;
            else if (user.Role.Name == Role.Names.ADMINISTRATOR)
                RoleList.SelectedIndex = 3;
        }
        
        protected void SaveUser(object sender, EventArgs e)
        {
            if (!IsValid)
                return;
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Password.Text))
                return;
            var user = ServiceFactory.GetUserService().GetById(Guid.Parse(UserId.Text));
            user.Name = NameTextBox.Text;
            user.Email = Email.Text;
            user.Password = Password.Text;
            user.AboutUser = AboutYour.Text;
            user.AccessName = ServiceFactory.GetUserService().GetAccessName(user);
            user.Role = ServiceFactory.GetRoleService().GetByName(RoleList.SelectedValue);
            user.IsBlocked = UserBlockedFlag.Checked;
            ServiceFactory.GetUserService().Update(user);
        }
    }
}