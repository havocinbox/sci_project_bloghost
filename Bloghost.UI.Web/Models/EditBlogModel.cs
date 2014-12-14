using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class EditBlogModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(Blog.MaxLengthFor.TITLE)]
        public string Title { get; set; }

        [StringLength(Blog.MaxLengthFor.DESCRIPTION)]
        public string Description { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}