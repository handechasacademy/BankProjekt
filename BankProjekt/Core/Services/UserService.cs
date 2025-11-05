using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;
using BankProjekt.Config;

namespace BankProjekt.Core.Services
{
    public class UserService
    {
        private User _user;

        public UserService(User user)
        {
            _user = user;
        }

        public void Deposit(Account account, decimal DepositAmount)
        {
            if (account == null)
                throw new NotFoundException("Account not found.");
            if (DepositAmount <= 0)
                throw new InvalidInputException("Deposit amount must be positive.");
            account.Deposit(DepositAmount);
        }

        public void Withdraw(Account account, decimal withdrawAmount)
        {
            if (account == null)
                throw new NotFoundException("Account not found.");
            if (withdrawAmount <= 0)
                throw new InvalidInputException("Amount must be positive.");
            if (withdrawAmount > account.Balance)
                throw new FundIssueException("Insufficient funds."); 
            account.Withdraw(withdrawAmount);
        }


        public bool UserInternalFundsTransfer(User user, string fromAccountNumber, string toAccountNumber, decimal amount, Dictionary<string, decimal> exchangeRates)
        {
            var fromAccount = user.Accounts?.FirstOrDefault(a => a.AccountNumber == fromAccountNumber);
            var toAccount = user.Accounts?.FirstOrDefault(a => a.AccountNumber == toAccountNumber);

            if (fromAccount == null || toAccount == null)
                throw new NotFoundException("One or both accounts not found.");
            if (fromAccount == toAccount)
                throw new InvalidInputException("Cannot transfer to the same account.");
            if (amount <= 0)
                throw new InvalidInputException("Transfer amount must be positive.");
            if (amount > fromAccount.Balance)
                throw new FundIssueException("Insufficient funds for transfer.");

            decimal transferAmount = amount;
            if (fromAccount.Currency != toAccount.Currency)
            {
                string rateKey = $"{fromAccount.Currency}:{toAccount.Currency}";
                if (!exchangeRates.ContainsKey(rateKey))
                    throw new InvalidInputException("No exchange rate available for this currency pair.");
                transferAmount = Math.Round(amount * exchangeRates[rateKey], 2);
            }

            Withdraw(fromAccount, amount);
            Deposit(toAccount, transferAmount);
            fromAccount.Transactions.Add(new Transaction(fromAccount.Owner.Id,fromAccount.AccountNumber,-amount,DateTime.Now,"Transfer Out")); //saving these so that it is also shown in transaction history
            toAccount.Transactions.Add(new Transaction(toAccount.Owner.Id,toAccount.AccountNumber,transferAmount,DateTime.Now,"Transfer In"));

            return true;
        }

        public Account OpenAccount(Bank bank, User user, string accountNumber, string currency, string accountType)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidInputException("Account number is invalid.");

            if (!bank.AccountNumbers.Add(accountNumber))
                throw new DuplicateException($"Account number {accountNumber} already exists.");

            Account account;

            if (accountType == "Checking")
            {
                account = new CheckingAccount(accountNumber, 0, user)
                {
                    InterestRate = BankConfig.InterestRate,
                    Currency = currency,
                    AccountType = "Checking"
                };
            }
            else if (accountType == "Savings")
            {
                account = new SavingsAccount(accountNumber, 0, user)
                {
                    Currency = currency,
                    AccountType = "Savings"
                };
            }
            else
            {
                throw new InvalidInputException("Invalid account type");
            }

            bank.Accounts[accountNumber] = account;
            (user.Accounts ?? new List<Account>()).Add(account);

            return account;
        }

    }
}

