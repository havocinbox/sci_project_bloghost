using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Model
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
