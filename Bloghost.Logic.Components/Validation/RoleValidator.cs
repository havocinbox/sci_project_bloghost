using System.Collections.Generic;
using Bloghost.Logic.Validation;
using Bloghost.Model;

namespace Bloghost.Logic.Components.Validation
{
    public class RoleValidator : Validator<Role>
    {
        public RoleValidator()
        {
        }

        public RoleValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(Role entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Name }, results);
        }
    }
}