using Bloghost.Logic.Components.Validation;
using Bloghost.Model;
using Bloghost.Logic;
using Bloghost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Components.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        public CommentService(IDataAccessObject<Comment, Guid> dataAccessObject)
            : base(dataAccessObject, new CommentValidator())
        {
        }
    }
}
