using System;
using Bloghost.Model;

namespace Bloghost.Data
{
    public interface IRoleDao : IDataAccessObject<Role, Guid>
    {
    }
}