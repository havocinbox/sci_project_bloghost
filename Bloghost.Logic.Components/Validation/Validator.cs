using Bloghost.Model;
using Bloghost.Logic.Exceptions;
using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilies;

namespace Bloghost.Logic.Components.Validation
{
    public abstract class Validator<TEntity> : IValidator<TEntity>
        where TEntity : IEntity<Guid>
    {
        private readonly string _prefix;

        protected Validator()
        {
            _prefix = string.Empty;
        }

        protected Validator(string prefix)
        {
            _prefix = prefix;
        }

        public IEnumerable<ValidationResult> Validate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            try
            {
                var results = new List<ValidationResult>();
                OnValidate(entity, results);
                return results;
            }
            catch (Exception exception)
            {
                Logger.Log(GetType().FullName, exception);

                throw new ValidationException("Validation error. See inner exception", exception);
            }
        }

        protected abstract void OnValidate(TEntity entity, List<ValidationResult> results);

        protected void NotEmptyString(TEntity entity, Expression<Func<TEntity, string>> accessor,
            IList<ValidationResult> results)
        {
            if (string.IsNullOrEmpty(accessor.Compile()(entity)))
            {
                results.Add(new ValidationResult(
                    entity,
                    string.Format("{0}.{1}", _prefix, PropertyName.For(accessor)),
                    "Empty string is not acceptable"
                    ));
            }
        }

        protected void NotNull(TEntity entity, Expression<Func<TEntity, object>> accessor,
            IList<ValidationResult> results)
        {
            if (accessor.Compile()(entity) == null)
            {
                results.Add(new ValidationResult(
                    entity,
                    string.Format("{0}.{1}", _prefix, PropertyName.For(accessor)),
                    "Null object is not acceptable"
                    ));
            }
        }

        protected void NotAllEmptyString(TEntity entity, Expression<Func<TEntity, string[]>> accessor,
            IList<ValidationResult> results)
        {
            if (accessor.Compile()(entity).Any(s => !string.IsNullOrWhiteSpace(s)))
            {
                return;
            }
            results.Add(new ValidationResult(
                entity,
                string.Format("{0}.{1}", _prefix, PropertyName.For(accessor)),
                "Empty string is not acceptable"
                ));
        }

        protected void PositiveInteger(TEntity entity, Expression<Func<TEntity, int>> accessor,
            IList<ValidationResult> results)
        {
            if (accessor.Compile()(entity) < 1)
            {
                results.Add(new ValidationResult(
                    entity,
                    string.Format("{0}.{1}", _prefix, PropertyName.For(accessor)),
                    "Not positive value is not acceptable"
                    ));
            }
        }

        protected void RangeInteger(int minimum, int maximum,
            TEntity entity, Expression<Func<TEntity, int>> accessor,
            IList<ValidationResult> results)
        {
            var value = accessor.Compile()(entity);

            if (value < minimum || value > maximum)
            {
                results.Add(new ValidationResult(
                    entity,
                    string.Format("{0}.{1}", _prefix, PropertyName.For(accessor)),
                    string.Format("Value between {0} and {1} is expected", minimum, maximum)
                    ));
            }
        }

    }
}
