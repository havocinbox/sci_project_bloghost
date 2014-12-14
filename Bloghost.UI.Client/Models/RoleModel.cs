using System;
using System.Collections.Generic;

namespace Bloghost.UI.Client.Models
{
    public class RoleModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Guid> Users { get; set; }
    }
}