using Bloghost.Logic.Components.Validation;
using Bloghost.Model;
using Bloghost.Logic;
using Bloghost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class BlogService : Service<Blog>, IBlogService
    {
        public BlogService(IDataAccessObject<Blog, Guid> dataAccessObject)
            : base(dataAccessObject, new BlogValidator())
        {
        }

        public string GetAccessName(Blog blog)
        {
            Expect.ArgumentNotNull(blog, "blog");
            Expect.ArgumentNotWhiteSpaceString(blog.Title, "Blog.Title");

            string link;
            string linkBackup = link = blog.Title.ToLower().Trim().Trim(new[] { '&', ',', '.', '/', '\\', '|', '@', '=', '?' }).Replace(" ", string.Empty).Replace("&", "_");
            var i = 0;
            while (GetAll().FirstOrDefault(x => x.AccessName == link) != null)
            {
                link = linkBackup + i;
                i++;
            }

            return link;
        }

        public IEnumerable<Blog> GetByUser(User user)
        {
            Expect.ArgumentNotNull(user, "user");

            return GetAll().Where(x => x.User.Equals(user));
        }

        public IEnumerable<Blog> GetByUser(Guid userId)
        {
            return GetAll().Where(x => x.User.Id.Equals(userId));
        }

        public IEnumerable<Blog> GetPopularBlogs()
        {
            return GetAll().OrderByDescending(blog => blog.Rating);
        }

        public IEnumerable<Blog> GetNewBlogs()
        {
            return GetAll().OrderByDescending(blog => blog.CreatedDate);
        }
    }
}
