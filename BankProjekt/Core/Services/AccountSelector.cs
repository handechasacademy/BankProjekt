using BankProjekt.Core.Users;
using BankProjekt.Core.Accounts;
using System;

namespace BankProjekt.Core.Services
{
    internal class AccountSelector
    {
        private readonly User _user;

        public AccountSelector(User user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public Account Run()
        {
            Account selectedAccount = null;

            while (selectedAccount == null)
            {
                Console.Write("Enter account number: ");
                string accountNumber = Console.ReadLine();

                selectedAccount = _user.FindAccountByAccountNumber(accountNumber);

                if (selectedAccount == null)
                {
                    Console.WriteLine("Account not found. Please try again.\n");
                }
                else
                {
                    Console.WriteLine($"\nAccount '{selectedAccount.AccountNumber}' selected.\n");
                }
            }

            return selectedAccount;
        }
    }
}
