using System.ComponentModel.DataAnnotations;
using Bloghost.Model;

namespace Bloghost.UI.Web.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(User.MaxLengthFor.EMAIL)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(User.MaxLengthFor.MAX_PASSWORD, MinimumLength = User.MaxLengthFor.MIN_PASSWORD, ErrorMessage = "dfdfg")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}