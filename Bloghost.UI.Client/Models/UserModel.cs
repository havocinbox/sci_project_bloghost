using System;
using System.Collections.Generic;

namespace Bloghost.UI.Client.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public string AboutUser { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<Guid> Blogs { get; set; }
    }
}