using System;

namespace Bloghost.UI.Web.Models.ApiModels
{
    public class CommentApiModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public ShortUserApiModel User { get; set; }
    }
}