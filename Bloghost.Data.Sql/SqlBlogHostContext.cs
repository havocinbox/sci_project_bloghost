using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Data.Sql
{
    public class SqlBlogHostContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Role> Roles { get; set; }

        public SqlBlogHostContext()
            : base(@"Data Source=(localdb)\v11.0;Initial Catalog=Bloghost.Data.Sql.SqlBlogHostContext;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False")
        {
        }
    }
}
