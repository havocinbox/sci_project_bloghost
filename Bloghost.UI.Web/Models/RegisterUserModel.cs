using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bloghost.Model;
using Bloghost.UI.Web.Infrastructure;

namespace Bloghost.UI.Web.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Input name")]
        [StringLength(User.MaxLengthFor.NAME)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Input email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(User.MaxLengthFor.EMAIL)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD)]
        [System.Web.Mvc.Compare("Password")]
        public string ChangePassword { get; set; }

        [StringLength(User.MaxLengthFor.ABOUT_USER)]
        public string AboutUser { get; set; }

        public string DateOfBirthYear { get; set; }

        public string DateOfBirthMonth { get; set; }

        public string DateOfBirthDay { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}