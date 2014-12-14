using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD)]
        public string ConfirmPassword { get; set; }
    }
}