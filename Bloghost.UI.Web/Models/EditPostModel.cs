using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class EditPostModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string BlogId { get; set; }

        [Required]
        [StringLength(Post.MaxLengthFor.TITLE)]
        public string Title { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}