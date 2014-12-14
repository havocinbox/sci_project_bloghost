using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Data
{
    public interface ITagDao : IDataAccessObject<Tag, Guid>
    {
    }
}
