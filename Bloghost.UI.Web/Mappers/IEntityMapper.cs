using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.UI.Web.Mappers
{
    public interface IEntityMapper<TWebModel, TEntity>
        where TEntity : Entity
    {
        TWebModel Map(TEntity entity);
        TEntity MapBack(TWebModel model);
    }
}
