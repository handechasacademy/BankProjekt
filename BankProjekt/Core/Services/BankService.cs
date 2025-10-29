using BankProjekt.ConsoleUI.ServiceUI;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;

namespace BankProjekt.Core.Services
{
    public class BankService
    {
        private readonly Bank _bank;
        private readonly User _user;

        public BankService(Bank bank, User user)
        {
            _bank = bank;
        }

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return true;
        }

        public bool HandleWithdraw(Account account)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return true;
        }
        public bool HandleDeposit(Account account)
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return true; 
        }
    }
}
