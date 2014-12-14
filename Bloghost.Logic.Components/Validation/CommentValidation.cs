using Bloghost.Model;
using Bloghost.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Validation
{
    public class CommentValidator : Validator<Comment>
    {
        public CommentValidator()
        {
        }

        public CommentValidator(string prefix)
            : base(prefix)
        {
        }

        protected override void OnValidate(Comment entity, List<ValidationResult> results)
        {
            NotAllEmptyString(entity, a => new[] { a.Content }, results);
            NotNull(entity, a => new IEntity<Guid>[] { a.Post, a.User }, results);
        }
    }
}
