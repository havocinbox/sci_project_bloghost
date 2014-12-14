using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Data
{
    public interface IDaoFactory
    {
        IUserDao GetUserDao();
        IBlogDao GetBlogDao();
        IPostDao GetPostDao();
        ICommentDao GetCommentDao();
        ITagDao GetTagDao();
        IRoleDao GetRoleDao();
    }
}
