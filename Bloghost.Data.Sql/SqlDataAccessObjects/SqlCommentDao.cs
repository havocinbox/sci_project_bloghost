﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;
using Bloghost.Data;

namespace Bloghost.Data.Sql.SqlDataAccessObjects
{
    public class SqlCommentDao : SqlDataAccessObject<Comment>, ICommentDao
    {
        public SqlCommentDao(SqlBlogHostContext sqlBlogHostContext)
            : base(sqlBlogHostContext)
        {
        }
    }
}
