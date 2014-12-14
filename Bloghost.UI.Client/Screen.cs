using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Bloghost.UI.Client.Models;

namespace Bloghost.UI.Client
{
    public class Screen
    {
        private ShortUserModel _authUser;
        private readonly HttpClient _client;

        private readonly IList<Tuple<string, int>> _mainMenu = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("Get all users", 1),
                new Tuple<string, int>("Get all blog", 2),
            };

        private readonly IList<Tuple<string, int>> _loginMenu = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("Login", 11),
            };

        private readonly IList<Tuple<string, int>> _logoutMenu = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("My profile", 5),
                new Tuple<string, int>("Logout", 12),
            };

        private readonly IList<Tuple<string, int>> _authUserMenu = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("Create blog", 8),
                new Tuple<string, int>("Edit my profile", 6),
                new Tuple<string, int>("Delete my profile", 7),
            };

        private readonly IList<Tuple<string, int>> _authBlogMenu = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("Edit blog", 9),
                new Tuple<string, int>("Delete blog", 10),
            };

        public Screen()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(@"http://localhost:2317/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GetCurrentAuthUser();
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                ShowHead("Main menu");

                var keysTable = new List<Tuple<int, int>>();
                var mainMenu = "";
                var i = 1;

                foreach (var item in _mainMenu)
                {
                    mainMenu += i + ": " + item.Item1 + "\n";
                    keysTable.Add(new Tuple<int, int>(i++, item.Item2));
                }
                if (_authUser == null)
                {
                    foreach (var item in _loginMenu)
                    {
                        mainMenu += i + ": " + item.Item1 + "\n";
                        keysTable.Add(new Tuple<int, int>(i++, item.Item2));
                    }
                }
                else
                {
                    foreach (var item in _logoutMenu)
                    {
                        mainMenu += i + ": " + item.Item1 + "\n";
                        keysTable.Add(new Tuple<int, int>(i++, item.Item2));
                    }
                }

                Console.WriteLine(mainMenu);

                var key = Int32.Parse(Console.ReadLine());
                Action(key, keysTable);
            }
        }

        private void ShowHead(string actionName = "")
        {
            string result;
            if (_authUser != null)
            {
                result = _authUser.Name + " | " + _authUser.RoleName;
            }
            else
            {
                result = "<<Login>>";
            }

            for (var i = 0; result.Length + actionName.Length < 80; i++)
            {
                result = " " + result;
            }
            result = actionName + result;

            Console.WriteLine(result);
        }

        private void Action(int key, IEnumerable<Tuple<int, int>> keysTable)
        {
            var k = keysTable.FirstOrDefault(x => x.Item1 == key);
            if (k == null)
                return;

            switch (k.Item2)
            {
                case 1:
                    {
                        GetAllUsers();
                    }
                    break;
                case 2:
                    {
                        GetAllBlogs();
                    }
                    break;
                case 5:
                    {
                        GetUser(_authUser.Id);
                    }
                    break;
                case 11:
                    {
                        Login();
                    }
                    break;
                case 12:
                    {
                        Logout();
                    }
                    break;
            }
        }

        private void GetAllUsers()
        {
            Console.Clear();
            ShowHead("All users");

            var keysTable = new List<Tuple<int, Guid>>();
            var i = 1;

            var response = _client.GetAsync("api/users").Result;
            if (response.IsSuccessStatusCode)
            {
                var shortUsers = response.Content.ReadAsAsync<IEnumerable<ShortUserModel>>().Result;
                foreach (var u in shortUsers)
                {

                    var responseUser = _client.GetAsync("api/users/" + u.Id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var user = responseUser.Content.ReadAsAsync<UserModel>().Result;

                        Console.WriteLine(i + ": " + user.Name);
                        Console.WriteLine("\t" + user.Id);
                        Console.WriteLine("\tRole: " + user.RoleName);
                        Console.WriteLine("\tDate of registration: " + user.DateOfRegistration);

                        keysTable.Add(new Tuple<int, Guid>(i++, user.Id));
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            var key = int.Parse(Console.ReadLine());
            if (key != 0 && keysTable.Where(x => x.Item1 == key).ToArray().Length > 0)
                GetUser(keysTable.Where(x => x.Item1 == key).ToArray()[0].Item2);
        }

        private void GetAllBlogs()
        {
            Console.Clear();
            ShowHead("All blogs");

            var keysTable = new List<Tuple<int, Guid>>();
            var i = 1;

            var response = _client.GetAsync("api/blogs").Result;
            if (response.IsSuccessStatusCode)
            {
                var blogs = response.Content.ReadAsAsync<IEnumerable<BlogModel>>().Result;
                foreach (var b in blogs)
                {


                    Console.WriteLine(i + ": " + b.Title);
                    Console.WriteLine("\t" + b.Id);
                    Console.WriteLine("\tDate created: " + b.DateCreated);
                    Console.WriteLine("\tAuthor: " + b.User.Name);
                    Console.WriteLine("\tDescription: " + b.Description);

                    keysTable.Add(new Tuple<int, Guid>(i++, b.Id));
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            var key = int.Parse(Console.ReadLine());
            if (key != 0 && keysTable.Where(x => x.Item1 == key).ToArray().Length > 0)
                GetBlog(keysTable.Where(x => x.Item1 == key).ToArray()[0].Item2);
        }

        private void GetUser(Guid id)
        {
            Console.Clear();
            ShowHead("User");

            var i = 1;
            var userActionTable = new List<Tuple<int, int>>();
            if (_authUser != null && _authUser.Id == id)
            {
                foreach (var item in _authUserMenu)
                {
                    Console.WriteLine(i + ": " + item.Item1);
                    userActionTable.Add(new Tuple<int, int>(i++, item.Item2));
                }
                Console.WriteLine();
            }

            var response = _client.GetAsync("api/users/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsAsync<UserModel>().Result;

                Console.WriteLine("Name: " + user.Name);
                Console.WriteLine("Id: " + user.Id);
                Console.WriteLine("Role: " + user.RoleName);
                Console.WriteLine("Date of registration: " + user.DateOfRegistration);
                Console.WriteLine("Date of birth: " + user.DateOfBirth);
                if (string.IsNullOrWhiteSpace(user.AboutUser))
                    Console.WriteLine("About user: " + user.AboutUser);
                Console.WriteLine("User's blogs:");

                var blogsTable = new List<Tuple<int, Guid>>();
                foreach (var blogId in user.Blogs)
                {
                    var responseBlog = _client.GetAsync("api/blogs/" + blogId).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var blog = responseBlog.Content.ReadAsAsync<BlogModel>().Result;

                        Console.WriteLine("\t" + i + ": " + blog.Title);
                        blogsTable.Add(new Tuple<int, Guid>(i++, blogId));
                    }
                }

                var key = int.Parse(Console.ReadLine());
                if (key != 0 && blogsTable.Where(x => x.Item1 == key).ToArray().Length > 0)
                    GetBlog(blogsTable.Where(x => x.Item1 == key).ToArray()[0].Item2);
                else if (key != null && userActionTable.Where(x => x.Item1 == key).ToArray().Length > 0)
                {
                    var k = userActionTable.Where(x => x.Item1 == key).ToArray()[0].Item2;
                    switch (k)
                    {
                        case 8:
                            {
                                CreateBlog();
                            }
                            break;
                        case 6:
                            {
                                EditUser();
                            }
                            break;
                        case 7:
                            {
                                DeleteUser();
                            }
                            break;
                    }
                }
            }
        }

        private void GetBlog(Guid id)
        {
            var i = 1;
            var keyTable = new List<Tuple<int, int>>();
            Console.Clear();
            ShowHead("Blog");

            var response = _client.GetAsync("api/blogs/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var blog = response.Content.ReadAsAsync<BlogModel>().Result;

                if (_authUser != null && blog.User.Id == _authUser.Id)
                {
                    foreach (var item in _authBlogMenu)
                    {
                        Console.WriteLine(i + ": " + item.Item1);
                        keyTable.Add(new Tuple<int, int>(i++, item.Item2));
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(blog.Title + "\n");
                Console.WriteLine("Author: " + blog.User.Name);
                Console.WriteLine("Id: " + blog.Id);
                Console.WriteLine("Date created: " + blog.DateCreated);
                if (string.IsNullOrWhiteSpace(blog.Description))
                    Console.WriteLine("Description: " + blog.Description);
                Console.WriteLine("Posts:");

                var responsePost = _client.GetAsync("api/posts/" + blog.Id).Result;
                if (responsePost.IsSuccessStatusCode)
                {
                    var posts = responsePost.Content.ReadAsAsync<IEnumerable<PostModel>>().Result;

                    i = 1;
                    foreach (var post in posts)
                    {
                        Console.WriteLine("\t" + i + ": " + post.Title);
                    }
                }

                var key = Int32.Parse(Console.ReadLine());
                if (key != 0 && _authUser != null && _authUser.Id == blog.User.Id && keyTable.Where(x => x.Item1 == key).ToArray().Length > 0)
                {
                    var k = keyTable.Where(x => x.Item1 == key).ToArray()[0].Item2;
                    switch (k)
                    {
                        case 9:
                            {
                                EditBlog(blog);
                            }
                            break;
                        case 10:
                            {
                                var responseDeleteBlog = _client.DeleteAsync("api/blogs/" + blog.Id.ToString()).Result;
                                Console.WriteLine(responseDeleteBlog.StatusCode + "\nPlease press any key to continue...");
                            }
                            break;
                    }
                }
            }
        }

        private void CreateBlog()
        {
            Console.Clear();
            ShowHead("Create blog");
            var blog = new BlogModel { Id = Guid.NewGuid() };
            Console.Write("Title: " + blog.Title);
            blog.Title = Console.ReadLine();
            Console.Write("Description: ");
            blog.Description = Console.ReadLine();

            var res = _client.PostAsJsonAsync("api/blogs/", blog).Result;
            Console.WriteLine(res.StatusCode + "\nPlease press any key to continue...");
            Console.ReadKey();
        }

        private void EditBlog(BlogModel blog)
        {
            Console.Clear();
            ShowHead("Edit blog");
            Console.WriteLine("Old title: " + blog.Title);
            Console.WriteLine("Blog id: " + blog.Id + "\n");
            Console.Write("New title: ");
            blog.Title = Console.ReadLine();
            Console.Write("New description: ");
            blog.Description = Console.ReadLine();

            var res = _client.PutAsJsonAsync("api/blogs/" + blog.Id, blog).Result;
            Console.WriteLine(res.StatusCode + "\nPlease press any key to continue...");
            Console.ReadKey();
        }

        private void EditUser()
        {
            Console.Clear();
            ShowHead("Edit user");

            var resUser = _client.GetAsync("api/users/" + _authUser.Id).Result;
            var user = resUser.Content.ReadAsAsync<UserModel>().Result;

            user.Id = _authUser.Id;
            Console.WriteLine("Old name: " + _authUser.Name);
            Console.WriteLine("User id: " + _authUser.Id + "\n");
            Console.Write("New name: ");
            user.Name = Console.ReadLine();
            Console.Write("Date of birth: ");
            user.DateOfBirth = DateTime.Parse(Console.ReadLine());
            Console.Write("About user: ");
            user.AboutUser = Console.ReadLine();

            var res = _client.PutAsJsonAsync("api/users/" + _authUser.Id, user).Result;
            Console.WriteLine(res.StatusCode + "\nPlease press any key to continue...");
            Console.ReadKey();

            GetCurrentAuthUser();
        }

        private void DeleteUser()
        {
            Console.Clear();
            ShowHead("Delete user");
            Console.WriteLine("Password: ");
            var pass = Console.ReadLine();

            var res = _client.DeleteAsync("api/users?password=" + pass).Result;
            Console.WriteLine(res.StatusCode + "\nPlease press any key to continue...");
            Console.ReadKey();
        }

        private void Login()
        {
            Console.Clear();
            Console.WriteLine("Login\n");
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var pass = Console.ReadLine();

            var res = _client.GetAsync("api/auth?email=" + email + "&password=" + pass).Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("\nEmail or password is invalid.\nPlease press any key to continue...");
            }
            else
            {
                _authUser = res.Content.ReadAsAsync<ShortUserModel>().Result;
                Console.WriteLine("\nHello, " + _authUser.Name);
            }
            Console.ReadKey();
        }

        private void Logout()
        {
            var res = _client.DeleteAsync("api/auth").Result;
            _authUser = null;
        }

        private void GetCurrentAuthUser()
        {
            var authUserResult = _client.GetAsync("api/auth").Result;
            if (authUserResult.StatusCode == HttpStatusCode.OK)
            {
                _authUser = authUserResult.Content.ReadAsAsync<ShortUserModel>().Result;
            }
            else
            {
                _authUser = null;
            }
        }
    }
}