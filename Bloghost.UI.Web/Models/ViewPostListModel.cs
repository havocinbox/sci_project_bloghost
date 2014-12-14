using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Models
{
    public class ViewPostListModel
    {
        public ViewBlogModel Blog { get; set; }

        public IList<ViewPostModel> Posts { get; set; }
    }
}