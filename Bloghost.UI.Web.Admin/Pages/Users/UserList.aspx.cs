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
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuNavigation.CurrentActiveMenu = new string[] { "users", "userlist" };
            UserTable.DataSource = ServiceFactory.GetUserService().GetAll().ToArray();
            UserTable.DataBind();
        }

        protected void UsersRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            var user = (User)e.Item.DataItem;

            var userIdLabel = (Label)e.Item.FindControl("UserIdLabel");
            var userNameLabel = (LinkButton)e.Item.FindControl("UserNameLabel");
            var emailLabel = (Label)e.Item.FindControl("UserEmailLabel");
            var statusLabel = (Label)e.Item.FindControl("UserStatusLabel");
            var roleList = (Label)e.Item.FindControl("UserRoleLabel");
            var editButton = (ImageButton)e.Item.FindControl("EditButton");
            var deleteButton = (ImageButton)e.Item.FindControl("DeleteButton");

            userIdLabel.Text = user.Id.ToString();
            userNameLabel.Text = user.Name;
            userNameLabel.CommandArgument = user.Id.ToString();
            userNameLabel.Click += EditUser;
            emailLabel.Text = user.Email;
            if (user.IsBlocked)
            {
                statusLabel.Text = "Blocked";
                statusLabel.CssClass = "label label-danger";
            }
            else
            {
                statusLabel.Text = "Active";
                statusLabel.CssClass = "label label-success";
            }
            if (user.Role == null)
                user.Role = ServiceFactory.GetRoleService().GetByName(Role.Names.READER);
            roleList.Text = user.Role.Name;
            switch (user.Role.Name)
            {
                case Role.Names.READER:
                    roleList.CssClass = "label label-success";
                    break;
                case Role.Names.BLOGGER:
                    roleList.CssClass = "label label-info";
                    break;
                case Role.Names.MODERATOR:
                    roleList.CssClass = "label label-warning";
                    break;
                case Role.Names.ADMINISTRATOR:
                    roleList.CssClass = "label label-danger";
                    break;
            }
            deleteButton.CommandArgument = user.Id.ToString();
            deleteButton.Click += DeleteUser;
            editButton.CommandArgument = user.Id.ToString();
            editButton.Click += EditUser;
        }

        private void DeleteUser(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var id = (sender as ImageButton).CommandArgument;
                ServiceFactory.GetUserService().Delete(ServiceFactory.GetUserService().GetById(Guid.Parse(id)));
                UserTable.DataSource = ServiceFactory.GetUserService().GetAll().ToArray();
                UserTable.DataBind();
            }
        }

        private void EditUser(object sender, EventArgs e)
        {
            var id = (sender as IButtonControl).CommandArgument;
            Response.Redirect("UserProfile.aspx" + @"?id=" + id);
        }

        protected void SelectRole(object sender, EventArgs e)
        {
            var listBox = sender as DropDownList;
            if (listBox == null)
                return;
            var id = listBox.ID;
            if (string.IsNullOrEmpty(id))
                return;
            var user = ServiceFactory.GetUserService().GetById(Guid.Parse(id));
            if (user == null)
                return;
            var roleName = string.Empty;
            switch (listBox.SelectedIndex)
            {
                case 0:
                    roleName = Role.Names.READER;
                    break;
                case 1:
                    roleName = Role.Names.BLOGGER;
                    break;
                case 2:
                    roleName = Role.Names.MODERATOR;
                    break;
                case 3:
                    roleName = Role.Names.ADMINISTRATOR;
                    break;
                default:
                    return;
            }
            user.Role = ServiceFactory.GetRoleService().GetByName(roleName);
            ServiceFactory.GetUserService().Update(user);
        }
    }
}