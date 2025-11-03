using System;
using BankProjekt.Core.Users;
using BankProjekt.Core.Services;
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
            var user = _users
                .FirstOrDefault(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase)
                                   && u.Password == password);

            if (user == null)
                throw new InvalidInputException("Incorrect username or password.");

            _currentUser = user;
            return user;
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}
