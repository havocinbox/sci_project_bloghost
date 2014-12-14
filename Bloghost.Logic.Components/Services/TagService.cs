using Bloghost.Data;
using Bloghost.Logic.Components.Validation;
using Bloghost.Logic.Exceptions;
using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class TagService : Service<Tag>, ITagSerivce
    {
        public TagService(IDataAccessObject<Tag, Guid> dataAccessObject)
            : base(dataAccessObject, new TagValidator())
        {
        }

        public Tag GetByContent(string content)
        {
            Expect.ArgumentNotEmptyString(content, "content");

            var tag = GetAll().FirstOrDefault(x => x.Content == content);
            if (tag == null)
            {
                tag = new Tag();
                tag.Content = content;
                tag.Id = Guid.NewGuid();
                Save(tag);
            }
            return tag;
        }

        public IEnumerable<Tag> GetTop5Tags()
        {
            return GetAll().OrderByDescending(x => x.Posts.Count).Take(10);
        }
    }
}
