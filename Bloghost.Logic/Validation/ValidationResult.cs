using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloghost.Model;

namespace Bloghost.Logic.Validation
{
    public class ValidationResult
    {
        public string Message { get; private set; }
        public string PropertyName { get; private set; }
        public IEntity<Guid> Entity { get; private set; }

        public ValidationResult(IEntity<Guid> entity, string propertyName, string message)
        {
            Entity = entity;
            PropertyName = propertyName;
            Message = message;
        }
    }
}
