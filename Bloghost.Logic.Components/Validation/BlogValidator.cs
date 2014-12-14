using Bloghost.Model;
using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Validation
{
    public class BlogValidator : Validator<Blog>
    {
        public BlogValidator()
        {
        }

        public BlogValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(Blog entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Title }, results);
            NotNull(entity, a => new[] { a.User }, results);
        }
    }
}
