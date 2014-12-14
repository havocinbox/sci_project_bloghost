using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Data
{
    public interface ICommentDao : IDataAccessObject<Comment, Guid>
    {
    }
}
