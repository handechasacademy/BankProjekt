using System;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly List<User> _users;
        private User? _currentUser;

        public LoginService(List<User> users)
        {
            _users = users;
        }

        public User? CurrentUser => _currentUser;

        public User Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new InvalidInputException("Incorrect username or password.");

            if (user.LockoutEndTime.HasValue && user.LockoutEndTime > DateTime.Now)
            {
                double minutesLeft = (user.LockoutEndTime.Value - DateTime.Now).TotalMinutes;
                throw new InvalidInputException(
                    $"Account locked. Try again in {Math.Ceiling(minutesLeft)} minutes.");
            }

            if (user.Password != password)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 4)
                {
                    user.LockoutEndTime = DateTime.Now.AddMinutes(10);
                    user.FailedLoginAttempts = 0;
                    throw new InvalidInputException(
                        "Too many failed attempts. Account locked for 10 minutes.");
                }

                throw new InvalidInputException(
                    $"Incorrect password. {4 - user.FailedLoginAttempts} attempts left.");
            }

            user.FailedLoginAttempts = 0;
            user.LockoutEndTime = null;
            _currentUser = user;
            return user;
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}
