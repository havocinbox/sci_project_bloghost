using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bloghost.UI.Web.Models
{
    public class ViewUserModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AccessName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string AboutUser { get; set; }

        public bool IsLogged { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LoggedDate { get; set; }

        public DateTime? BlockedDate { get; set; }

        public List<ViewBlogModel> BlogList { get; set; } 
        
        public DateTime? DeletedDate { get; set; }

        public string PathToAvatar { get; set; }
    }
}