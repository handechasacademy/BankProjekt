using System;
using System.Collections.Generic;
using System.Linq;

namespace BankProjekt.Core.Users
{
    namespace BankProjekt.Core.Users
    {
        public interface ILoginService
        {
            User? CurrentUser { get; }
            User Login(string username, string password);
            void Logout();
        }

        public class LoginService : ILoginService
        {
            private readonly List<User> _users;
            private User? _currentUser;

            public User? CurrentUser => _currentUser;

            public LoginService(List<User> users) => _users = users;

            public User Login(string username, string password)
            {
                var user = _users.FirstOrDefault(u =>
                    u.Name.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    u.Password == password
                );

                if (user == null)
                    throw new Exception("Incorrect username or password.");

                _currentUser = user;
                return user;
            }

            public void Logout()
            {
                _currentUser = null;
            }
        }
    }
}