using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User();
            user.Name = "New";
            user.AboutUser = "Bloger";

            var blog = new Blog();
            blog.User = user;
            blog.Title = "Blog about butterfly";

            var post = new Post();
            post.Blog = blog;
            post.Title = "Red butterfly";
            post.Content = "First list...";

            Test.userService.Save(user);
            Test.blogService.Save(blog);
            Test.postService.Save(post);

            System.Console.WriteLine(Test.ToString());
            System.Console.ReadKey();
            
            System.Console.Clear();

            post.Title = "Red butterfly or ...";
            post.Content += "Second list...";


            user.AboutUser = "Best Bloger";
            Test.userService.Update(user);
            Test.postService.Update(post);
            System.Console.WriteLine(Test.ToString());
            System.Console.ReadKey();




            ////Test.CreateUsers(new[] { "Alex", "Alice", "Bob", "Piter" }, new[] { "Ivanov", "Sidorov", "Z", "" },
            ////    new[] { "I am a journalist. I write about politics", "Sport bloger", "Love flowers!", "" }, 5);
            ////Test.CreateBlogs(Test.userService.GetAll().ToArray(), new List<Tuple<string, string>>()
            ////    {
            ////        new Tuple<string, string>("About butterfly", "ASf"),
            ////        new Tuple<string, string>("NHL", "About USA hockey"),
            ////        new Tuple<string, string>("World of ...", "World of ..."),
            ////    }, 3);
            ////Test.CreatePosts(Test.blogService.GetAll().ToArray(), new List<Tuple<string, string>>()
            ////    {
            ////        new Tuple<string, string>("Man", "adfadfafda dfa df df"),
            ////        new Tuple<string, string>("Lev", "fdsdfda fa dfa da da fd"),
            ////        new Tuple<string, string>("About Minsk", "Minsk is the capital of Belarus. Lukashenko is a eternal..."),
            ////        new Tuple<string, string>("No one plays football now :(", "Real Madrid ahdf jadh fjkah dkfj"),
            ////        new Tuple<string, string>("Happy New Year! Football closed!", "Real Madrid ahdf jadh fjkah dkfj"),
            ////    }, 5);

            ////Test.CreateComments(
            ////    Test.postService.GetAll().ToArray(),
            ////    Test.userService.GetAll().ToArray(),
            ////    new[]{
            ////            "The best!",
            ////            "Nuts!",
            ////            "Ho-ho-ho",
            ////            "It's really fun!",
            ////            "Чебурашка",
            ////            "@#&*^$(@*&%^&@(_*#$@+#%^#$-)@$&^@$"
            ////        },
            ////    5);

            System.Console.WriteLine(Test.ToString());
            System.Console.ReadKey();
        }
    }
}
