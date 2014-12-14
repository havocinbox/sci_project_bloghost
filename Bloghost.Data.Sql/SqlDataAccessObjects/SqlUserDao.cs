using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Data.Exceptions;
using Bloghost.Model;
using Bloghost.Data;
using Utilies;

namespace Bloghost.Data.Sql.SqlDataAccessObjects
{
    public class SqlUserDao : SqlDataAccessObject<User>, IUserDao
    {
        public SqlUserDao(SqlBlogHostContext sqlBlogHostContext)
            : base(sqlBlogHostContext)
        {
        }
    }
}
