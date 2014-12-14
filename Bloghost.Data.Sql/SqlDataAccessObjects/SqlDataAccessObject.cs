using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Bloghost.Model;
using Bloghost.Data;
using Bloghost.Data.Exceptions;
using Utilies;

namespace Bloghost.Data.Sql
{
    public abstract class SqlDataAccessObject<TEntity> : IDataAccessObject<TEntity, Guid>, IDisposable
        where TEntity : Entity
    {
        private bool _isDisposed;
        protected readonly SqlBlogHostContext SqlBlogHostContext;
        protected DbSet<TEntity> EntityTable;

        protected SqlDataAccessObject(SqlBlogHostContext sqlBlogHostContext)
        {
            Expect.ArgumentNotNull(sqlBlogHostContext, "sqlBlogHostContext");

            SqlBlogHostContext = sqlBlogHostContext;
            EntityTable = SqlBlogHostContext.Set<TEntity>();
            _isDisposed = false;
        }


        public void Create(TEntity entity) 
        {
            Expect.NotDispose(_isDisposed, GetType().Name);            
            Expect.ArgumentNotNull(entity, "entity");
            
            if (EntityTable.FirstOrDefault(x => x.Id == entity.Id) != null)
            {
                throw new DataException("Entity already exists with some id");
            }
            EntityTable.Add(entity);
            SqlBlogHostContext.SaveChanges();
        }

        public TEntity Read(Guid id) 
        {
            Expect.NotDispose(_isDisposed, GetType().Name);

            return EntityTable.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<TEntity> ReadAll() 
        {
            Expect.NotDispose(_isDisposed, GetType().Name);

            return EntityTable.AsEnumerable();
        }
        
        public void Update(TEntity entity) 
        {
            Expect.NotDispose(_isDisposed, GetType().Name);
            Expect.ArgumentNotNull(entity, "entity");

            if (EntityTable.FirstOrDefault(x => x.Id == entity.Id) == null)
            {
                throw new DataException("Entity with this id doesn't exist");
            }
            SqlBlogHostContext.SaveChanges();
        }

        public void Delete(Guid id) 
        {
            Expect.NotDispose(_isDisposed, GetType().Name);

            var entity = EntityTable.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                EntityTable.Remove(entity);
                SqlBlogHostContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _isDisposed = true;

            Logger.Log(GetType().FullName, "Object is disposed", NLog.LogLevel.Info);
        }
    }
}
