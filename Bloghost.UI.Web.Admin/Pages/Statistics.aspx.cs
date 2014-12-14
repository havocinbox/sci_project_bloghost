using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bloghost.Logic.Components;
using Bloghost.Model;

namespace Bloghost.UI.Web.Admin
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuNavigation.CurrentActiveMenu = new string[]{"statictics"};

            UsersCount.Text = ServiceFactory.GetUserService().GetAll().Count().ToString();
            BlogsCount.Text = ServiceFactory.GetBlogService().GetAll().Count().ToString();
            PostsCount.Text = ServiceFactory.GetPostService().GetAll().Count().ToString();
            CommentsCount.Text = ServiceFactory.GetCommentService().GetAll().Count().ToString();
            ClientScript.RegisterStartupScript(GetType(), "InitBlogsAndPostsChart", GetScriptForInitBlogsAndPostsChart(), true);
            ClientScript.RegisterStartupScript(GetType(), "InitTop5Bloggers", GetScriptForInitTop5Bloggers(), true);
            ClientScript.RegisterStartupScript(GetType(), "InitTop5Blogs", GetScriptForInitTop5Blogs(), true);
            ClientScript.RegisterStartupScript(GetType(), "InitUserRoles", GetGetScriptForInitUserRoles(), true);
        }

        private string GetScriptForInitBlogsAndPostsChart()
        {
            var dataPost = new int[12];
            var dataBlog = new int[12];
            var max = 0;
            var stepCount = 10;
            var stepWidth = 0;
            foreach (var post in ServiceFactory.GetPostService().GetAll())
            {
                dataPost[post.CreatedDate.Month - 1]++;
            }
            foreach (var blog in ServiceFactory.GetBlogService().GetAll())
            {
                dataBlog[blog.CreatedDate.Month - 1]++;
            }
            var dataPostString = dataPost.Aggregate(string.Empty, (c, x) => c + x + ",");
            var dataBlogString = dataBlog.Aggregate(string.Empty, (c, x) => c + x + ",");
            dataPostString = dataPostString.Remove(dataPostString.Length - 1, 1);
            dataBlogString = dataBlogString.Remove(dataBlogString.Length - 1, 1);
            max = dataPost.Max();
            if (max < dataBlog.Max())
                max = dataBlog.Max();
            max = RoundNumber(max);
            stepWidth = max / stepCount;
            var script = "var usersStatChartData = {" +
                        "labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']," +
                        "datasets: [" +
                        "{" +
                        "fillColor: 'rgba(220,220,220,0.5)'," +
                        "strokeColor: 'rgba(220,220,220,1)'," +
                        "pointColor: 'rgba(220,220,220,1)'," +
                        "pointStrokeColor: '#fff'," +
                        "data: [" + dataPostString + "]," +
                        "}," +
                        "{" +
                        "fillColor: 'rgba(151,187,205,0.5)'," +
                        "strokeColor: 'rgba(151,187,205,1)'," +
                        "pointColor: 'rgba(151,187,205,1)'," +
                        "pointStrokeColor: '#fff'," +
                        "    data: [" + dataBlogString + "]" +
                        "}" +
                        "]," +
                        "};" +
                        "var uscdlineOptions = {" +
                        "scaleOverride: true, scaleStepWidth: " + stepWidth + ", scaleStartValue: 0, scaleSteps: " + stepCount + "," +
                        "bezierCurve: false," +
                        "};" +
                        "var plot3 = new Chart(document.getElementById('usersStatsChart').getContext('2d')).Line(usersStatChartData, uscdlineOptions);";
            return script;
        }

        private string GetScriptForInitTop5Bloggers()
        {
            var top5Bloggers = ServiceFactory.GetUserService().GetAll().OrderByDescending(x => x.Blogs.Count).Take(5);
            var names = string.Empty;
            var data = string.Empty;
            var max = 0;
            var stepCount = 10;
            var stepWidth = 0;
            foreach (var user in top5Bloggers)
            {
                names += "'" + user.Name + "',";
            }
            names = names.Remove(names.Length - 1, 1);
            foreach (var user in top5Bloggers)
            {
                if (max < user.Blogs.Count)
                    max = user.Blogs.Count;
                data += user.Blogs.Count + ",";
            }
            data = data.Remove(data.Length - 1, 1);
            max = RoundNumber(max);
            stepWidth = max / stepCount;
            var script = "var topBloggers = {" +
                        "labels: [" + names + "]," +
                        "datasets: [" +
                        "{" +
                        "fillColor: 'rgba(220,220,220,0.5)'," +
                        "strokeColor: 'rgba(220,220,220,1)'," +
                        "pointColor: 'rgba(220,220,220,1)'," +
                        "pointStrokeColor: '#fff'," +
                        "data: [" + data + "]," +
                        "}" +
                        "]," +
                        "};" +
                        "var tbloggersOpt = {scaleShowGridLines: false, datasetStroke: false, scaleOverride: true, scaleStepWidth: " + stepWidth + ", scaleStartValue: 0, scaleSteps: " + stepCount + "};" +
                        "var plot2 = new Chart(document.getElementById('topBloggers').getContext('2d')).Bar(topBloggers, tbloggersOpt);";
            return script;
        }

        private string GetScriptForInitTop5Blogs()
        {
            var top5Blogs = ServiceFactory.GetBlogService().GetAll().OrderByDescending(x => x.Posts.Count).Take(5);
            var names = string.Empty;
            var data = string.Empty;
            var max = 0;
            var stepCount = 10;
            var stepWidth = 0;
            foreach (var blog in top5Blogs)
            {
                names += "'" + blog.Title.Replace(@"'", @"\'") + "',";
            }
            names = names.Remove(names.Length - 1, 1);
            foreach (var blog in top5Blogs)
            {
                if (max < blog.Posts.Count)
                    max = blog.Posts.Count;
                data += blog.Posts.Count + ",";
            }
            data = data.Remove(data.Length - 1, 1);
            max = RoundNumber(max);
            stepWidth = max / stepCount;
            var script = "var topBlogs = {" +
                        "labels: [" + names + "]," +
                        "datasets: [" +
                        "{" +
                        "fillColor : 'rgba(151,187,205,0.5)'," +
                        "strokeColor : 'rgba(151,187,205,1)'," +
                        "data: [" + data + "]," +
                        "}" +
                        "]," +
                        "};" +
                        "var tblogsOpt = {scaleOverride: true, scaleStepWidth: " + stepWidth + ", scaleStartValue: 0, scaleSteps: " + stepCount + "};" +
                        "var plot3 = new Chart(document.getElementById('topBlogs').getContext('2d')).Bar(topBlogs, tblogsOpt);";
            return script;
        }

        private string GetGetScriptForInitUserRoles()
        {
            var data = new int[4];
            foreach (var role in ServiceFactory.GetUserService().GetAll().Select(x => x.Role))
            {
                if (role.Name == Role.Names.ADMINISTRATOR)
                    data[0]++;
                else if (role.Name == Role.Names.BLOGGER)
                    data[1]++;
                else if (role.Name == Role.Names.MODERATOR)
                    data[2]++;
                else if (role.Name == Role.Names.READER)
                    data[3]++;
            }
            var script = "var dataBlogsAndPosts = [" +
                        "    {" +
                        "        value: " + data[0] + "," +
                        "        color:'#F38630'," +
                        "        label: 'Administrator'," +
                        "        labelColor : 'white'," +
                        "        labelFontSize : '16'," +
                        "        labelAlign : 'left'"+
                        "    }," +
                        "    {" +
                        "        value : " + data[1] + "," +
                        "        color :'#E0E4CC'," +
                        "        label: 'Blogger'," +
                        "        labelColor : 'white'," +
                        "        labelFontSize : '16'," +
                        "        labelAlign : 'left'" +
                        "    }," +
                        "    {" +
                        "        value : " + data[2] + "," +
                        "        color : '#69D2E7'," +
                        "        label: 'Moderator'," +
                        "        labelColor : 'white'," +
                        "        labelFontSize : '16'," +
                        "        labelAlign : 'left'" +
                        "    }," +
                        "    {" +
                        "        value : " + data[3] + "," +
                        "        color : '#111111'," +
                        "        label: 'Reader'," +
                        "        labelColor : 'white'," +
                        "        labelFontSize : '16'," +
                        "        labelAlign : 'left'" +
                        "    }" +
                        "];" +
                        "var plot4 = new Chart(document.getElementById('userRoles').getContext('2d')).Pie(dataBlogsAndPosts);";
            return script;
        }

        private int RoundNumber(int number)
        {
            return number + (10 - number % 10);
        }
    }
}