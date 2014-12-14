using Bloghost.Model;
using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Validation
{
    public class UserValidator : Validator<User>
    {
        public UserValidator()
        {
        }

        public UserValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(User entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Name, a.AccessName, a.Email, a.Password }, results);
        }
    }
}
