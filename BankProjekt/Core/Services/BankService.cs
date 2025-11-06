using BankProjekt.Core.Users;
using java.security.acl;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    public class BankService
    {
        private Bank _bank;
        public static readonly Dictionary<string, decimal> ExchangeRates = new Dictionary<string, decimal>
        {
            { "SEK:USD", 0.09m },
            { "USD:SEK", 11.0m },
            { "SEK:EUR", 0.085m },
            { "EUR:SEK", 11.8m },
            { "USD:EUR", 0.87m },
            {"EUR:USD", 1.15m  }
        };

        public BankService(Bank bank) { _bank = bank; }

        public void TransferFundsBetweenUsers(string fromAccountNumber, string toAccountNumber, decimal transferToUseramount)
        {
            var fromAccount = _bank.Accounts.GetValueOrDefault(fromAccountNumber);
            var toAccount = _bank.Accounts.GetValueOrDefault(toAccountNumber);

            if (fromAccount == null || toAccount == null)
                throw new NotFoundException("Account not found.");
            if (fromAccount == toAccount)
                throw new InvalidInputException("Cannot send money to the same account.");
            if (transferToUseramount <= 0)
                throw new InvalidInputException("Amount must be positive.");
            if (fromAccount.Balance < transferToUseramount)
                throw new FundIssueException("Insufficient funds.");

            decimal transferAmount = transferToUseramount;
            if (fromAccount.Currency != toAccount.Currency)
            {
                string rateKey = $"{fromAccount.Currency}:{toAccount.Currency}";
                if (!ExchangeRates.ContainsKey(rateKey))
                    throw new InvalidInputException("No exchange rate available.");

                decimal rate = ExchangeRates[rateKey];
                transferAmount = Math.Round(transferToUseramount * rate, 2);
            }
            fromAccount.Withdraw(transferToUseramount);
            toAccount.Deposit(transferAmount);

            fromAccount.Transactions.Add(new Transaction(fromAccount.Owner.Id, fromAccount.AccountNumber,-transferToUseramount, DateTime.Now,"Transfer Out", bufferMinutes: 15));

            toAccount.Transactions.Add(new Transaction (toAccount.Owner.Id, toAccount.AccountNumber,transferAmount, DateTime.Now,"Transfer In", bufferMinutes: 15));
        }


        public HashSet<User> GetAllUsers()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            return new HashSet<User>(_bank.Users);
        }

        public List<(User user, decimal totalBalance)> GetTotalBalanceSummaries()
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");

            var results = new List<(User user, decimal totalBalance)>();

            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    throw new NotFoundException($"No accounts found for user '{user.Name}' (ID: {user.Id}).");

                decimal total = user.Accounts.Sum(a => a.Balance);
                results.Add((user, total));
            }

            return results;
        }

        public string SearchAccount(string searchInput) 
        {
            if (!_bank.Users.Any())
                throw new NotFoundException("No users found.");
            foreach (var user in _bank.Users)
            {
                if (user.Accounts == null)
                    continue;
                foreach (var account in user.Accounts)
                {
                    if (account.AccountNumber.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                        user.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase))
                    {
                        return $"User: {user.Name} (ID: {user.Id})\nAccount Number: {account.AccountNumber}";
                    }
                }
            }
            throw new NotFoundException($"No account or user found matching '{searchInput}'.");
        }

    }
}
