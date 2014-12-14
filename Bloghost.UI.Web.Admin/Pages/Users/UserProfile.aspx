<%@ Page Language="C#" Title="User profile" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" MasterPageFile="../../Site.Master" Inherits="Bloghost.UI.Web.Admin.Pages.Users.UserProfile" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="content">
        <div id="pad-wrapper" class="new-user">
            <div class="row header">
                <div class="col-md-12">
                    <h3>User profile</h3>
                </div>
            </div>


            <div class="row">
                <div class="col-md-3 knobs">
                    <div class="image-slider">
                        <div id="slider" class="flexslider">
                            <asp:Image runat="server" ID="UserAvatar" Width="200px" AlternateText="Avatar" />
                            
                        </div>
                    </div>
                </div>

                <div class="col-md-7">
                    <!-- left column -->
                    <div class="field-box" style="padding: 5px">
                        <label class="col-md-3" style="text-align: right;">Id *</label>
                        <asp:TextBox ID="UserId" runat="server" CssClass="inline_input form-control" Width="70%" Enabled="False"></asp:TextBox>
                    </div>
                    <div class="field-box">
                        <label class="col-md-3" style="text-align: right;">Name *</label>
                        <asp:TextBox ID="NameTextBox" runat="server" TextChanged="SaveUser" CssClass="inline_input form-control" Width="70%"></asp:TextBox>
                        <div style="color: red; padding-left: 140px; font-size: 15px;">
                            <asp:RequiredFieldValidator ID="NameVidodator" runat="server" ControlToValidate="NameTextBox" ErrorMessage="&nbsp;&nbsp;Name can not be empty."></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field-box" style="padding: 5px">
                        <label class="col-md-3" style="text-align: right;">Role</label>
                        <div class="ui-select span5">
                            <asp:DropDownList ID="RoleList" runat="server">
                                <asp:ListItem>Reader</asp:ListItem>
                                <asp:ListItem>Blogger</asp:ListItem>
                                <asp:ListItem>Moderator</asp:ListItem>
                                <asp:ListItem>Administrator</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="field-box" style="padding: 5px">
                        <label class="col-md-3" style="text-align: right;">About you</label>
                        <asp:TextBox ID="AboutYour" runat="server" Width="70%" CssClass="inline_input form-control" TextMode="multiline" Columns="50" Rows="5"></asp:TextBox>
                    </div>
                    <div class="field-box">
                        <label class="col-md-3" style="text-align: right;">Email *</label>
                        <asp:TextBox ID="Email" TextMode="Email" Width="70%" runat="server" CssClass="inline_input  form-control"></asp:TextBox>
                        <div style="color: red; padding-left: 140px; font-size: 15px;">
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="Email" ErrorMessage="&nbsp;&nbsp;Invalid email format"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="field-box">
                        <label class="col-md-3" style="text-align: right;">Password *</label>
                        <asp:TextBox ID="Password" Width="70%" runat="server" CssClass="inline_input form-control"></asp:TextBox>
                        <div style="color: red; padding-left: 140px; font-size: 15px;">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w{3,}" ControlToValidate="Password" ErrorMessage="&nbsp;&nbsp;Password length is not less than 3 characters"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="field-box">
                        <label class="col-md-3" style="text-align: right;">Is blocked</label>
                        <asp:CheckBox ID="UserBlockedFlag" Width="70%" runat="server"></asp:CheckBox>
                    </div>
                    <div class="field-box" style="width: 65%; margin-left: 450px;">
                        <asp:Button ID="SaveUserButton" runat="server" OnClick="SaveUser" CssClass="btn-glow primary" Text="Save" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
