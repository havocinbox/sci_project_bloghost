using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic
{
    public interface ITagSerivce : IService<Tag>
    {
        Tag GetByContent(string content);
        IEnumerable<Tag> GetTop5Tags();
    }
}
