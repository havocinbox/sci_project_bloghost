<%@ Page Language="C#" Title="User list" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" MasterPageFile="../../Site.Master" Inherits="Bloghost.UI.Web.Admin.Pages.UserList" %>

<asp:Content runat="server" ID="UserListContent" ContentPlaceHolderID="MainContent">
    <div class="content">

        <div id="pad-wrapper" class="users-list">
            <div class="row header">
                <h3>Users</h3>
                <div class="col-md-10 col-sm-12 col-xs-12 pull-right">
                    <asp:HyperLink runat="server" CssClass="btn-flat success pull-right" NavigateUrl="/Pages/Users/NewUser.aspx">
                        <asp:Label runat="server">&#43;</asp:Label>
                        NEW USER
                    </asp:HyperLink>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 table-products">
                    <table class="table table-hover">
                        <asp:Repeater ID="UserTable" runat="server" OnItemDataBound="UsersRepeater_OnItemDataBound" ViewStateMode="Disabled">
                            <HeaderTemplate>
                                <tr>
                                    <th class="col-md-3">Id</th>
                                    <th class="col-md-2">Name</th>
                                    <th class="col-md-2">Email</th>
                                    <th class="col-md-2">Role</th>
                                    <th class="col-md-2">Status</th>
                                </tr>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="UserIdLabel" runat="server" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="UserNameLabel" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="UserEmailLabel" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="UserRoleLabel" CssClass="label" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="UserStatusLabel" CssClass="label label-danger" runat="server" />
                                        <ul class="actions">
                                            <li>
                                                <asp:ImageButton ID="EditButton" runat="server" ImageUrl="~/Images/ico-table-new.png" />
                                            </li>
                                            <li class="last">
                                                <asp:ImageButton ID="DeleteButton" runat="server" ImageUrl="~/Images/ico-table-delete.png" />
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
