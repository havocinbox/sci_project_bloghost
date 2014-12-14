using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;
using Bloghost.Data;
using Bloghost.Data.Sql;

namespace Bloghost.Data.Sql.SqlDataAccessObjects
{
    public class SqlPostDao : SqlDataAccessObject<Post>, IPostDao
    {
        public SqlPostDao(SqlBlogHostContext sqlBlogHostContext)
            : base(sqlBlogHostContext)
        {
        }
    }
}
