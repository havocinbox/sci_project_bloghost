using System;

namespace Bloghost.UI.Client.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public ShortUserModel User { get; set; }
    }
}