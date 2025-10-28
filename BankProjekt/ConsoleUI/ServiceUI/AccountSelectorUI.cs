using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BankProjekt.ConsoleUI.ServiceUI
{
    internal class AccountSelectorUI
    {
        private readonly User _user;
        public Account _account { get; set; }

        public AccountSelectorUI(User user) 
        {
            _user = user;
        }

        public void Run()
        {
            Console.Write("Account number ? ");
            string AN = Console.ReadLine();

            _account = _user.FindAccountByAccountNumber(AN);

            if (_account == null)
            {
                Console.WriteLine("Account not found.");
                Console.Write("Account number ? ");
                AN = Console.ReadLine();
                _account = _user.FindAccountByAccountNumber(AN);
            }
            else { Console.WriteLine($"\nAccount '{_account.AccountNumber}' selected.");}
        }
    }
}
