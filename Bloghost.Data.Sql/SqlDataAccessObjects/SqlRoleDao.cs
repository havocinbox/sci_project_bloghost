using Bloghost.Model;

namespace Bloghost.Data.Sql.SqlDataAccessObjects
{
    public class SqlRoleDao : SqlDataAccessObject<Role>, IRoleDao
    {
        public SqlRoleDao(SqlBlogHostContext sqlBlogHostContext)
            : base(sqlBlogHostContext)
        {
        }
    }
}