using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bloghost.UI.Web.Models.ApiModels
{
    public class PostApiModel
    {
        public Guid Id { get; set; }

        public Guid BlogId { get; set; }

        public ShortUserApiModel User { get; set; }

        public DateTime DateCreated { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public IEnumerable<Guid> Comments { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}