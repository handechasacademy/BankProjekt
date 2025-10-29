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

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, out string message)
        {
            fromAccountNumber = new AccountSelectorUI(fromAccountNumber);



            message = $"Successfully transferred {amount:C} from {source.AccountNumber} to {destination.AccountNumber}.";
            return true;
        }
    }
}
