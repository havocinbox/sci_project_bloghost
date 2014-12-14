using System;
using System.Collections.Generic;

namespace Bloghost.UI.Web.Models.ApiModels
{
    public class RoleApiModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Guid> Users { get; set; }
    }
}