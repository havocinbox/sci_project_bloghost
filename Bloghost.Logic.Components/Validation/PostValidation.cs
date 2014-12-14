using Bloghost.Model;
using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Validation
{
    public class PostValidator : Validator<Post>
    {
        public PostValidator()
        {
        }

        public PostValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(Post entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Content, a.Title }, results);
            NotNull(entity, a => new[] { a.Blog }, results);
        }
    }
}
