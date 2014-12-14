using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Models
{
    public class DeleteBlogModel
    {
        [Required]
        public string BlogId { get; set; }
    }
}