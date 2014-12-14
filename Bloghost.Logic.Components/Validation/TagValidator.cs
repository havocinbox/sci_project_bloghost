using Bloghost.Logic.Validation;
using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Validation
{
    public class TagValidator : Validator<Tag>
    {
        public TagValidator()
        {
        }

        public TagValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(Tag entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Content }, results);
        }
    }
}
