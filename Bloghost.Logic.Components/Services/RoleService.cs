using System;
using System.Linq;
using Bloghost.Data;
using Bloghost.Logic.Exceptions;
using Bloghost.Logic.Validation;
using Bloghost.Model;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class RoleService : Service<Role>, IRoleService
    {
        public RoleService(IDataAccessObject<Role, Guid> dataAccessObject) 
            : base(dataAccessObject, new Validation.RoleValidator())
        {
        }

        public Role GetByName(string name)
        {
            Expect.ArgumentNotEmptyString(name, "name");

            var role = GetAll().FirstOrDefault(x => x.Name == name);
            if (role == null)
            {
                role = new Role();
                role.Name = name;
                role.Id = Guid.NewGuid();
                Save(role);
            }
            return role;
        }
    }
}