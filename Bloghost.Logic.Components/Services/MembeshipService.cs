using System.Linq;
using Bloghost.Logic.Exceptions;
using Bloghost.Model;
using System;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class MembershipService : IMembershipService, IDisposable
    {
        private bool _isDisposed;
        private readonly IUserService _userService;

        public MembershipService(IUserService userService)
        {
            Expect.ArgumentNotNull(userService, "userService");
            _userService = userService;
        }

        public User RegisterUser(string fullName, string email, string password)
        {
            Expect.ArgumentNotWhiteSpaceString(fullName, "fullName");
            Expect.ArgumentNotWhiteSpaceString(email, "email");
            Expect.ArgumentNotWhiteSpaceString(password, "password");

            if (_userService.GetByEmail(email) != null)
                throw new ServiceException("User with this email exists");

            var user = new User();
            user.Name = fullName;
            user.Id = Guid.NewGuid();
            user.AccessName = _userService.GetAccessName(user);
            user.Email = email;
            user.Password = password;
            user.Role = ServiceFactory.GetRoleService().GetByName(Role.Names.READER);
            _userService.Save(user);
            return user;
        }

        public User RegisterUser(User user)
        {
            Expect.ArgumentNotNull(user, "user");

            if (_userService.GetByEmail(user.Email) != null)
                throw new ServiceException("User with this email exists");

            user.AccessName = _userService.GetAccessName(user);

            _userService.Save(user);
            return user;
        }

        public User LoginUser(string email, string password)
        {
            Expect.NotDispose(_isDisposed);

            var user = _userService.GetByEmail(email);
            if (user == null)
                throw new ModelNotFoundException<User>();
            if (user.Password != password)
                throw new AuthenticationException(user.Id);
            user.IsLogged = true;
            user.LoggedDate = DateTime.UtcNow;
            _userService.Update(user);
            return user;
        }

        public void LogoutUser(Guid userId)
        {
            Expect.NotDispose(_isDisposed);

            var user = _userService.GetById(userId);
            if (user == null)
                throw new ModelNotFoundException<User>();
            if (user.IsLogged)
            {
                user.IsLogged = false;
                _userService.Update(user);
            }
        }

        public void ChangePassword(Guid userId, string oldPassword, string newPassword, string confirmPassword)
        {
            Expect.NotDispose(_isDisposed);

            if (newPassword != confirmPassword)
                throw new PasswordChangeException("New password does not match with the confirmation");
            var user = _userService.GetById(userId);
            if (user == null)
                throw new ModelNotFoundException<User>();
            if (!user.IsLogged)
                throw new AuthenticationException("User didn't authorize", userId);
            if (user.Password != oldPassword)
                throw new PasswordChangeException("Old password does not match");
            user.Password = newPassword;
            _userService.Update(user);
        }

        public void ResetPassword(string userEmail)
        {
            Expect.NotDispose(_isDisposed); //what for?

            throw new NotImplementedException("Ohohoho!");
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                var disposable = _userService as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
            _isDisposed = true;

            Logger.Log(GetType().FullName, "Object is disposed", NLog.LogLevel.Info);
        }
    }
}
