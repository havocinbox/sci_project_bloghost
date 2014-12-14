<%@ Page Title="Statistics" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="Bloghost.UI.Web.Admin._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="content">
        <!-- main statictics -->
        <div id="main-stats">
            <div class="row stats-row">
                <div class="col-md-3 col-sm-3 stat">
                    <div class="data">
                        <asp:Label runat="server" ID="UsersCount" CssClass="number"></asp:Label>
                        users
                    </div>
                </div>
                <div class="col-md-3 col-sm-3 stat">
                    <div class="data">
                        <asp:Label runat="server" ID="BlogsCount" CssClass="number"></asp:Label>
                        blogs
                    </div>
                </div>
                <div class="col-md-3 col-sm-3 stat">
                    <div class="data">
                        <asp:Label runat="server" ID="PostsCount" CssClass="number"></asp:Label>
                        posts
                    </div>
                </div>
                <div class="col-md-3 col-sm-3 stat last">
                    <div class="data">
                        <asp:Label runat="server" ID="CommentsCount" CssClass="number"></asp:Label>
                        comments
                    </div>
                </div>
            </div>
        </div>
        <!-- end main statictics -->

        <div id="pad-wrapper">

            <!-- Blogs and posts -->
            <div class="row chart">
                <div class="col-md-12">
                    <h4 class="clearfix">Created blogs and posts</h4>
                </div>
                <div class="col-md-12">
                    <canvas id="usersStatsChart" height="350" width="900"></canvas>
                </div>
            </div>

            <!-- Top blogger and blogs -->
            <div class="row section ui-elements">
                <div class="col-md-5 knobs">
                    <!-- Blogs -->
                    <div class="row chart">
                        <div class="col-md-12">
                            <h4 class="clearfix">Top 5 bloggers</h4>
                        </div>
                        <div class="col-md-12">
                            <canvas id="topBloggers" height="350" width="410"></canvas>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 showcase">
                    <!-- Blogs -->
                    <div class="row chart">
                        <div class="col-md-12">
                            <h4 class="clearfix">Top 5 blogs</h4>
                        </div>
                        <div class="col-md-12">
                            <canvas id="topBlogs" height="350" width="450"></canvas>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Roles -->
            <div class="section">
                <div class="row chart">
                    <div class="col-md-12">
                        <h4 class="clearfix">User roles</h4>
                    </div>
                    <div class="col-md-12" style="margin: 0 auto; width: 400px">
                        <canvas id="userRoles" height="350" width="300"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
