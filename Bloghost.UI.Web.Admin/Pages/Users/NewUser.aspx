<%@ Page Language="C#" Title="New user" AutoEventWireup="true" MasterPageFile="../../Site.Master" CodeBehind="NewUser.aspx.cs" Inherits="Bloghost.UI.Web.Admin.Pages.NewUser" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="NewUserContent">
    <div class="content">
        <div id="pad-wrapper" class="new-user">
            <div class="row header">
                <div class="col-md-12">
                    <h3>Create a new user</h3>
                </div>
            </div>

            <div class="row form-wrapper">
                <!-- left column -->
                <div class="col-md-12 field-box">
                    <label>Name *</label>
                    <asp:TextBox ID="Name" runat="server" Width="50%" CssClass="col-md-9 form-control"></asp:TextBox>
                    <div style="color: red; padding-left: 5px; font-size: 15px;">
                        <asp:RequiredFieldValidator ID="NameVidodator" runat="server" ControlToValidate="Name" ErrorMessage="&nbsp;&nbsp;Name can not be empty."></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-12 field-box">
                    <label>Role</label>
                    <div class="ui-select span5 ">
                        <asp:DropDownList ID="RoleList" runat="server">
                            <asp:ListItem>Reader</asp:ListItem>
                            <asp:ListItem>Blogger</asp:ListItem>
                            <asp:ListItem>Moderator</asp:ListItem>
                            <asp:ListItem>Administrator</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12 field-box">
                    <label>About you</label>
                    <asp:TextBox ID="AboutYour" runat="server" Width="50%" CssClass="col-md-9 form-control" TextMode="multiline" Columns="50" Rows="5"></asp:TextBox>
                </div>
                <div class="col-md-12 field-box">
                    <label>Email *</label>
                    <asp:TextBox ID="Email" TextMode="Email" Width="50%" runat="server" CssClass="col-md-9 form-control"></asp:TextBox>
                    <div style="color: red; padding-left: 5px; font-size: 15px;">
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ErrorMessage="&nbsp;&nbsp;Invalid email format"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 field-box">
                    <label>Password *</label>
                    <asp:TextBox ID="Password" TextMode="Password" Width="50%" runat="server" CssClass="col-md-9 form-control"></asp:TextBox>
                    <div style="color: red; padding-left: 5px; font-size: 15px;">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w{3,}" ControlToValidate="Password" ErrorMessage="&nbsp;&nbsp;Password length is not less than 3 characters"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-12 field-box">
                    <label>Password Again *</label>
                    <asp:TextBox ID="PasswordAgain" TextMode="Password" Width="50%" runat="server" CssClass="col-md-9 form-control"></asp:TextBox>
                    <div style="color: red; padding-left: 5px; font-size: 15px;">
                        <asp:CompareValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PasswordAgain" ControlToCompare="Password" ErrorMessage="&nbsp;&nbsp;Repeated password does not match your password"></asp:CompareValidator>
                    </div>
                </div>
                <div class="col-md-11 field-box actions" style="width: 65%">
                    <asp:Button runat="server" CssClass="btn-glow primary" OnClick="CreateUser" Text="Create user" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
