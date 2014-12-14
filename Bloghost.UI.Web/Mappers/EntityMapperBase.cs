using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Logic.Exceptions;
using Bloghost.Model;
using Utilies;

namespace Bloghost.UI.Web.Mappers
{
    public abstract class EntityMapperBase<TWebModel, TEntity> : IEntityMapper<TWebModel, TEntity>
        where TEntity : Entity
    {
        public TWebModel Map(TEntity entity)
        {
            //Expect.ArgumentNotNull(entity, "entity");

            try
            {
                return OnMap(entity);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new LogicException("Data entity couldn't be mapped. See inner exception", exception);
            }
        }

        public TEntity MapBack(TWebModel model)
        {
            //Expect.ArgumentNotNull(model, "businessEntity");

            try
            {
                return OnMapBack(model);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new LogicException("Business entity couldn't be mapped back. See inner exception", exception);
            }
        }

        protected abstract TWebModel OnMap(TEntity entity);
        protected abstract TEntity OnMapBack(TWebModel model);
    }
}