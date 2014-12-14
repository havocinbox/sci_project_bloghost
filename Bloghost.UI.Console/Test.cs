using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.UI.Console
{
    public static class Test
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static IUserService userService = ServiceFactory.GetUserService();
        public static IBlogService blogService = ServiceFactory.GetBlogService();
        public static IPostService postService = ServiceFactory.GetPostService();
        public static ICommentService commentService = ServiceFactory.GetCommentService();
        
        public static void CreateUsers(string[] firstNames, string[] lastNames, string[] aboutDesc, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var user = new User();
                user.Name = firstNames[random.Next(0, firstNames.Length)]
                    + " " + lastNames[random.Next(0, lastNames.Length)];
                user.AboutUser = aboutDesc[random.Next(0, aboutDesc.Length)];
                userService.Save(user);
            }
        }

        public static void CreateBlogs(User[] users, List<Tuple<string, string>> blogDesc, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var blog = new Blog();
                blog.User = users[random.Next(0, users.Length)];
                var d = blogDesc[random.Next(0, blogDesc.Count)];
                blog.Title = d.Item1;
                blog.Description = d.Item2;
                blogService.Save(blog);
            }
        }

        public static void CreatePosts(Blog[] blogs, List<Tuple<string, string>> postDesc, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var post = new Post();
                post.Blog = blogs[random.Next(0, blogs.Length)]; 
                var d = postDesc[random.Next(0, postDesc.Count)];
                post.Title = d.Item1;
                post.Content = d.Item2;
                postService.Save(post);
            }
        }

        public static void CreateComments(Post[] posts, User[] users, string[] contents, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var com = new Comment();
                com.Post = posts[random.Next(0, posts.Length)];
                com.User = users[random.Next(0, users.Length)];

                com.Content = contents[random.Next(0, contents.Length)];
                commentService.Save(com);
            }
        }

        public static string ToString()
        {
            var result = new StringBuilder(100);
            var users = userService.GetAll();
            foreach (var user in users)
            {
                var prefix = string.Empty;

                result.Append("Name: " + user.Name + "\n");
                if (user.DateOfBirth.HasValue)
                    result.Append("Date of birth: " + user.DateOfBirth.Value.ToShortDateString() + "\n");
                if (!string.IsNullOrEmpty(user.AboutUser))
                    result.Append("About user: " + user.AboutUser + "\n");

                var i = 0;
                foreach (var blog in user.Blogs)
                {
                    i++;
                    prefix = "\t";

                    result.Append("  ==");
                    result.Append(prefix + "Blog #"+i+"\n" + prefix + "Title: " + blog.Title + "\n");
                    result.Append(prefix + "Description: " + blog.Description + "\n\n");

                    var j = 0;
                    foreach (var post in blog.Posts)
                    {
                        j++;
                        prefix = "\t\t";

                        result.Append(prefix + "Post #"+j+"\n" + prefix + "Title: " + post.Title + "\n");
                        result.Append(prefix + "Content: " + post.Content + "\n\n");

                        var k = 0;
                        foreach (var com in post.Comments)
                        {
                            k++;
                            prefix = "\t\t\t";

                            result.Append(prefix + "Comment #" + k + "\n");
                            result.Append(prefix + "By: " + com.User.Name + "\n");
                            result.Append(prefix + "Content: " + com.Content + "\n\n");
                        }
                    }
                }

                result.Append("-----------------------------\n\n");
            }
            return result.ToString();
        }
    }
}
