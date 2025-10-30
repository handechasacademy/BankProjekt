using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    public class UserService
    {
        private User _user;

        public UserService(User user)
        {
            _user = user;
        }

        public decimal GetTotalBalance()
        {
            if (_user.Accounts == null)
                throw new NotFoundException("User has no accounts.");
            return _user.Accounts.Sum(a => a.Balance);
        }

        public List<Transaction> GetAllTransactions() //user might have 2 accounts
        {
            if (_user.Accounts == null)
                throw new NotFoundException("User has no accounts.");
            return _user.Accounts
                .SelectMany(acc => acc.GetTransactions())
                .ToList();
        }

        public Account FindAccountByAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidInputException("Account number is empty.");
            var account = _user.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
                throw new NotFoundException($"Account '{accountNumber}' not found for user.");
            return account;
        }

        public void Deposit(Account account, decimal amount)
        {
            if (account == null)
                throw new NotFoundException("Account not found.");
            if (amount <= 0)
                throw new InvalidInputException("Deposit amount must be positive.");
            account.Deposit(amount);
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (account == null)
                throw new NotFoundException("Account not found.");

            if (amount <= 0)
                throw new InvalidInputException("Amount must be positive.");

            if (amount > account.Balance)
                throw new InvalidOperationException("Insufficient funds.");

            account.Withdraw(amount);
        }


        public bool TransferFunds(Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount == null || toAccount == null)
                throw new NotFoundException("One or both accounts not found.");
            if (amount <= 0)
                throw new InvalidInputException("Transfer amount must be positive.");
            if (amount > fromAccount.Balance)
                throw new InvalidInputException("Insufficient funds for transfer.");
            Withdraw(fromAccount, amount);
            Deposit(toAccount, amount);
            return true;
        }

        public List<Transaction> GetTransactionHistory(Account account) //for one account only
        {
            if (account == null)
                throw new NotFoundException("Account not found.");
            return account.GetTransactions();
        }
    }
}

