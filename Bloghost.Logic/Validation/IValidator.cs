using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Logic.Validation
{
    public interface IValidator<TEntity>
        where TEntity : IEntity<Guid>
    {
        IEnumerable<ValidationResult> Validate(TEntity entity);
    }
}
