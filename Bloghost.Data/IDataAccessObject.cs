using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Data
{
    public interface IDataAccessObject<TEntity, in TId> 
        where TEntity : IEntity<TId>
    {
        void Create(TEntity entity);
        TEntity Read(TId id);
        IEnumerable<TEntity> ReadAll();
        void Update(TEntity entity);
        void Delete(TId id);
    }
}
