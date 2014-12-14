using System;
using System.Collections.Generic;

namespace Bloghost.UI.Web.Models.ApiModels
{
    public class BlogApiModel
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ShortUserApiModel User { get; set; }

        public IEnumerable<Guid> Posts { get; set; }
    }
}