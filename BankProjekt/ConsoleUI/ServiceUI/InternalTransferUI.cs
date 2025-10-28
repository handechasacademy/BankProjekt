using BankProjekt.ConsoleUI.ServiceUI;
using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using System;

namespace BankProjekt.ConsoleUI.Menus
{
    internal class InternalTransferUI
    {
        private readonly Bank _bank;
        private readonly User _user;
        public InternalTransferUI(Bank bank, User user)
        {
            _bank = bank;
            _user = user;
        }

        public void Run(Bank _bank, Account sourceAccount)
        {
            var bankService = new BankService(_bank);
            var accountSelector = new AccountSelectorUI(_user);

            Console.Clear();
            Console.WriteLine("---- INTERNAL TRANSFER----");
            Console.WriteLine("Your accounts:");
            foreach(var a in _user.Accounts)
            {
                Console.WriteLine("Account number: " + a.AccountNumber);
                Console.WriteLine("Balance: " + a.Balance);
            }
            accountSelector.Run();



            Console.Write("Enter amount to transfer: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                Console.ReadKey();
                return;
            }

            bool success = bankService.Transfer(sourceAccount.AccountNumber, destAccountNumber, amount, out string message);

            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
