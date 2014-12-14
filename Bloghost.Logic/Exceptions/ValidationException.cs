using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Utilies;

namespace Bloghost.Logic.Exceptions
{
    [Serializable()]
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationResult> ValidationResults { get; set; }

        public ValidationException()
            : this("Don't panic! Something failed validation.")
        {
        }

        public ValidationException(IEnumerable<ValidationResult> validationResults)
            : this()
        {
            Expect.ArgumentNotNull(validationResults, "validationResults");
            ValidationResults = validationResults;
        }

        public ValidationException(string message)
            : base(message)
        {
            ValidationResults = new List<ValidationResult>();
        }

        public ValidationException(string message, IEnumerable<ValidationResult> validationResults)
            : this(message)
        {
            Expect.ArgumentNotNull(validationResults, "validationResults");
            ValidationResults = validationResults;
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
