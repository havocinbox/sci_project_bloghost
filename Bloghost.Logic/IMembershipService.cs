using Bloghost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic
{
    public interface IMembershipService
    {
        User RegisterUser(string fullName, string email, string password);
        User RegisterUser(User user);
        User LoginUser(string email, string password);
        void LogoutUser(Guid userId);
        void ChangePassword(Guid userId, string oldPassword, string newPassword, string confirmPassword);
        void ResetPassword(string userEmail);
    }
}
