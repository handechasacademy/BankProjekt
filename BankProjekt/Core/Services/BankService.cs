using BankProjekt.Core.Accounts;
using System;

namespace BankProjekt.Core.Services
{
    public class BankService
    {
        private readonly Bank _bank;

        public BankService(Bank bank)
        {
            _bank = bank;
        }

        public bool Transfer(string fromAccountNumber, string toAccountNumber, decimal amount, out string message)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(fromAccountNumber) ||
                string.IsNullOrWhiteSpace(toAccountNumber))
            {
                message = "Account numbers cannot be empty.";
                return false;
            }

            if (amount <= 0)
            {
                message = "Transfer amount must be positive.";
                return false;
            }

            var source = _bank.FindAccountByAccountNumber(fromAccountNumber);
            var destination = _bank.FindAccountByAccountNumber(toAccountNumber);

            if (source == null)
            {
                message = "Source account not found.";
                return false;
            }

            if (destination == null)
            {
                message = "Destination account not found.";
                return false;
            }

            if (source.Balance < amount)
            {
                message = "Insufficient funds for transfer.";
                return false;
            }
            source.Withdraw(amount);
            destination.Deposit(amount);

            message = $"Successfully transferred {amount:C} from {source.AccountNumber} to {destination.AccountNumber}.";
            return true;
        }
    }
}
