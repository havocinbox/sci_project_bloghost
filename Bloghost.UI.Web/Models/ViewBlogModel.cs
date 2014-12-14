using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bloghost.UI.Web.Models
{
    public class ViewBlogModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string AccessName { get; set; }

        public string Description { get; set; }

        public ViewUserModel User { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string PathToAvatar { get; set; }
    }
}