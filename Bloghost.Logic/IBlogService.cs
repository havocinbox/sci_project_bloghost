using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Logic
{
    public interface IBlogService : IService<Blog>
    {
        string GetAccessName(Blog blog);
        IEnumerable<Blog> GetByUser(User user);
        IEnumerable<Blog> GetByUser(Guid userId);
        IEnumerable<Blog> GetPopularBlogs();
        IEnumerable<Blog> GetNewBlogs();
    }
}
