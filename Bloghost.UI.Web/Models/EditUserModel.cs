using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class EditUserModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(User.MaxLengthFor.NAME)]
        public string Name { get; set; }

        [StringLength(User.MaxLengthFor.ABOUT_USER)]
        public string AboutUser { get; set; }

        public string DateOfBirthYear { get; set; }

        public string DateOfBirthMonth { get; set; }

        public string DateOfBirthDay { get; set; }
    }
}