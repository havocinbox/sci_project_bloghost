using Bloghost.Model;

namespace Bloghost.Logic
{
    public interface IRoleService : IService<Role>
    {
        Role GetByName(string name);
    }
}