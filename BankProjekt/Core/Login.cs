using System;
using System.Collections.Generic;
using System.Linq;
using BankProjekt.Core.Users;

namespace BankProjekt.Core
{
    public interface ILoginService
    {
        User Login(string username, string password, string id);
        
        
    }

    public class LoginService : ILoginService
    {
        private readonly List<User> _users;
        private User _currentUser; 

        public LoginService(List<User> users)
        {
            _users = users;
        }

        public User Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Name == username && u.Password == password);
            if (user == null)
                throw new Exception("Invalid username or password.");

            _currentUser = user;
            return _currentUser;
        }

        public User Login(string username, string password, string id)
        {
            throw new NotImplementedException();
        }
    }
}
