using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Models
{
    public class EditCommentModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string PostId { get; set; }
    }
}