using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Logic
{
    public interface IPostService : IService<Post>
    {
        string GetAccessName(Post post);
        string GetPreviewPost(Post post);
        IEnumerable<Post> GetLatestPosts();
    }
}
