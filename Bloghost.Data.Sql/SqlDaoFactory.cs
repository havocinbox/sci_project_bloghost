using System;
using Bloghost.Data.Sql.SqlDataAccessObjects;
using Utilies;

namespace Bloghost.Data.Sql
{
    public class SqlDaoFactory : IDaoFactory, IDisposable
    {
        private static SqlBlogHostContext _sqlBlogHostContext;
        private static Lazy<SqlUserDao> _userDao;
        private static Lazy<SqlBlogDao> _blogDao;
        private static Lazy<SqlPostDao> _postDao;
        private static Lazy<SqlCommentDao> _commentDao;
        private static Lazy<SqlTagDao> _tagDao;
        private static Lazy<SqlRoleDao> _roleDao;

        static SqlDaoFactory()
        {
            _sqlBlogHostContext = new SqlBlogHostContext();
            _userDao = new Lazy<SqlUserDao>(() => new SqlUserDao(_sqlBlogHostContext));
            _blogDao = new Lazy<SqlBlogDao>(() => new SqlBlogDao(_sqlBlogHostContext));
            _postDao = new Lazy<SqlPostDao>(() => new SqlPostDao(_sqlBlogHostContext));
            _commentDao = new Lazy<SqlCommentDao>(() => new SqlCommentDao(_sqlBlogHostContext));
            _tagDao = new Lazy<SqlTagDao>(() => new SqlTagDao(_sqlBlogHostContext));
            _roleDao = new Lazy<SqlRoleDao>(() => new SqlRoleDao(_sqlBlogHostContext));
        }

        public IUserDao GetUserDao() 
        {
            return _userDao.Value;
        }

        public IBlogDao GetBlogDao() 
        { 
            return _blogDao.Value; 
        }

        public IPostDao GetPostDao() 
        {
            return _postDao.Value; 
        }

        public ICommentDao GetCommentDao() 
        { 
            return _commentDao.Value; 
        }

        public ITagDao GetTagDao()
        {
            return _tagDao.Value; 
        }

        public IRoleDao GetRoleDao()
        {
            return _roleDao.Value;
        }

        public void Dispose()
        {
            if (_userDao.IsValueCreated)
            {
                _userDao.Value.Dispose();
            }
            if (_blogDao.IsValueCreated)
            {
                _blogDao.Value.Dispose();
            }
            if (_postDao.IsValueCreated)
            {
                _postDao.Value.Dispose();
            }
            if (_commentDao.IsValueCreated)
            {
                _commentDao.Value.Dispose();
            }
            if (_roleDao.IsValueCreated)
            {
                _roleDao.Value.Dispose();
            }
            if (_tagDao.IsValueCreated)
            {
                _tagDao.Value.Dispose();
            }
            _sqlBlogHostContext.Dispose();

            Logger.Log(GetType().FullName, "Object is disposed", NLog.LogLevel.Info);
        }
    }
}
