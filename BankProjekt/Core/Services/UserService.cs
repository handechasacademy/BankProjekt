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

        //moved to TransactionService.cs
        //public List<Transaction> GetAllTransactions() //user might have 2 accounts
        //{
        //    if (_user.Accounts == null)
        //        throw new NotFoundException("User has no accounts.");
        //    return _user.Accounts
        //        .SelectMany(acc => acc.GetTransactions())
        //        .ToList();
        //}
        

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


        public bool ÚserInternalFundsTransfer(Account fromAccount, Account toAccount, decimal amount) //changed name
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

        //moved to TransactionService.cs
        //public List<Transaction> GetTransactionHistory(Account account) //for one account only
        //{
        //    if (account == null)
        //        throw new NotFoundException("Account not found.");
        //    return account.GetTransactions();
        //}

        //Account management

        public Account OpenAccount(Bank bank, string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidInputException("Account number is invalid.");

            if (!bank.AccountNumbers.Add(accountNumber))
                throw new DuplicateException($"Account number '{accountNumber}' already exists.");

            var account = new Account(accountNumber, 0m, _user);
            bank.Accounts[accountNumber] = account;
            (_user.Accounts ??= new List<Account>()).Add(account);

            return account;
        }
    }
}

