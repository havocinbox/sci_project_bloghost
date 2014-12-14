using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class ViewPostModel
    {
        public string Id { get; set; }

        public ViewBlogModel Blog { get; set; }

        public string Title { get; set; }

        public string AccessName { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public DateTime DateCreate { get; set; }

        public IList<ViewCommentModel> Comments { get; set; }

        public IList<string> Tags { get; set; }
    }
}