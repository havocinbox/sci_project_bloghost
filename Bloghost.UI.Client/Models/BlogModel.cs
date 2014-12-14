using System;
using System.Collections.Generic;

namespace Bloghost.UI.Client.Models
{
    public class BlogModel
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ShortUserModel User { get; set; }

        public IEnumerable<Guid> Posts { get; set; }
    }
}