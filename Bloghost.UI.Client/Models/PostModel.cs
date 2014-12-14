using System;
using System.Collections.Generic;

namespace Bloghost.UI.Client.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }

        public Guid BlogId { get; set; }

        public ShortUserModel User { get; set; }

        public DateTime DateCreated { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<Guid> Comments { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}