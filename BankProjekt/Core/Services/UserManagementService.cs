using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    public class UserManagementService
    {
        private Bank _bank;

        public UserManagementService(Bank bank)
        {
            _bank = bank;
        }


        public User AddAdmin(string id, string name, string password)
        {
            try
            {
                if (_bank.Users.Any(u => u.Id == id))
                    throw new DuplicateException("User already exists.");
            } catch(DuplicateException ex)
            {
                Console.WriteLine("ERROR: "+ex.Message);
            }

            var admin = new User(id, name, password);
            admin.IsAdmin = true;
            _bank.Users.Add(admin);
            return admin;
        }
        public User CreateUser(string id, string name, string password)
        {
            try
            {
                if (_bank.Users.Any(u => u.Id == id))
                    throw new DuplicateException("User already exists.");
            }
            catch(DuplicateException ex) { Console.WriteLine("ERROR: " + ex.Message); }
            var user = new User(id, name, password);
            _bank.Users.Add(user);
            return user;
        }
    }
}
