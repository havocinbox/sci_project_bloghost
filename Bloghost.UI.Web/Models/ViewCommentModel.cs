using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Models
{
    public class ViewCommentModel
    {
        public string Id { get; set; }

        public string PostId { get; set; }

        public ViewUserModel User { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public ViewCommentModel ParentComment { get; set; }
    }
}