using BankProjekt.Core;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI.ServiceUI
{
    internal class AddUserUI
    {
        private readonly Bank _bank;
        private User _user;

        public AddUserUI(Bank bank) { _bank = bank; }

        public void Run()
        {
            Console.Write("User ID ? ");
            string Id = Console.ReadLine();
            Console.Write("Username ? ");
            string Username = Console.ReadLine();
            Console.Write("Password ? ");
            string Password = Console.ReadLine();

            _user = new User(Id, Username, Password);

            try
            {
                _bank.Users.Add(_user);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to add User Account.");
            }
        }

    }
}
