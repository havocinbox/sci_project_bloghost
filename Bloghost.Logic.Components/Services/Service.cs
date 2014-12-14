using System;
using System.Collections.Generic;
using System.Linq;
using Bloghost.Model;
using Bloghost.Logic.Validation;
using Bloghost.Data;
using Utilies;
using Bloghost.Logic.Exceptions;

namespace Bloghost.Logic.Components.Services
{
    public abstract class Service<TEntity> : IService<TEntity>, IDisposable
        where TEntity : IEntity<Guid>
    {
        private bool _isDisposed;
        protected IDataAccessObject<TEntity, Guid> Repository;
        protected IValidator<TEntity> Validator;

        protected Service(IDataAccessObject<TEntity, Guid> repository, IValidator<TEntity> validator)
        {
            Expect.ArgumentNotNull(repository, "repository");
            Expect.ArgumentNotNull(validator, "validator");

            Repository = repository;
            Validator = validator;
        }

        public void Save(TEntity entity)
        {
            Expect.NotDispose(_isDisposed, GetType().Name);
            Expect.ArgumentNotNull(entity, "entity");

            Validate(entity);
            try
            {
                Repository.Create(entity);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ServiceException("Panic error! Object can not be created", exception);
            }
        }

        public TEntity GetById(Guid id)
        {
            Expect.NotDispose(_isDisposed, GetType().Name);

            try
            {
                return Repository.ReadAll().FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ServiceException("Panic error! Entity don't can be read by Id", exception);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            Expect.NotDispose(_isDisposed, GetType().Name);

            try
            {
                return Repository.ReadAll();
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ServiceException("Panic error! All objects can not be read", exception);
            }
        }

        public void Update(TEntity entity)
        {
            Expect.NotDispose(_isDisposed, GetType().Name);
            Expect.ArgumentNotNull(entity, "entity");

            Validate(entity);
            try
            {
                Repository.Update(entity);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ServiceException("Panic error! Object can not be updated", exception);
            }
        }

        public void Delete(TEntity entity)
        {
            Expect.NotDispose(_isDisposed, GetType().Name);
            Expect.ArgumentNotNull(entity, "entity");

            try
            {
                Repository.Delete(entity.Id);
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ServiceException("Object can not be deleted. Is that good or bad?", exception);
            }
        }

        protected void Validate(TEntity entity)
        {
            var validationResult = Validator.Validate(entity);
            if (validationResult.Any())
            {
                throw new ValidationException(validationResult);
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
            }
            _isDisposed = true;

            Logger.Log(GetType().FullName, "Object is disposed", NLog.LogLevel.Info);
        }
    }
}
