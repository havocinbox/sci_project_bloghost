using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Data.Sql.SqlDataAccessObjects
{
    public class SqlTagDao : SqlDataAccessObject<Tag>, ITagDao
    {
        public SqlTagDao(SqlBlogHostContext sqlBlogHostContext)
            : base(sqlBlogHostContext)
        {
        }
    }
}
