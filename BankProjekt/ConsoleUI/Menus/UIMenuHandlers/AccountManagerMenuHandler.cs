using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuHandlers
{
    public class AccountManagerMenuHandler
    {
        private readonly User _user;
        private readonly Bank _bank;
        private readonly UserService _userService;
        private readonly BankService _bankService;
        private readonly TransactionService _transactionService;
        private readonly AccountFilteringService _accountFilteringService;
        private readonly AccountService _accountService;


        public AccountManagerMenuHandler(User user, Bank bank)
        {
            _user = user;
            _bank = bank;
            _userService = new UserService(_user);
            _bankService = new BankService(_bank);
            _transactionService = new TransactionService(_bank);
            _accountFilteringService = new AccountFilteringService(_bank.Users);
            _accountService = new AccountService(_user);
        }

        public void HandleDeposit(Account account)
        {
            Console.Write("Deposit Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                try
                {
                    account.Deposit(depositAmount);
                    Console.WriteLine("\nDeposit successful.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nInvalid amount.");
            }
        }

        public void HandleWithdrawal(Account account)
        {
            Console.Write("Withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
            {
                try
                {
                    _userService.Withdraw(account, withdrawAmount);
                    Console.WriteLine("\nWithdrawal successful.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FundIssueException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nInvalid amount.");
            }
        }

        public void HandleTransferToUser(Account account)
        {
            Console.Write("Receiver username: ");
            string receiverUsername = Console.ReadLine();
            Console.Write("Target account number: ");
            string receiverAccountNum = Console.ReadLine();
            Console.Write("Transfer amount: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                Console.WriteLine();
                try
                {
                    _bankService.TransferFundsBetweenUsers(account.AccountNumber, receiverAccountNum, transferAmount);
                    Console.WriteLine("\nTransfer to user successful.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FundIssueException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input.");
            }
        }

        public void HandleInternalTransfer(Account account)
        {
            Console.Write("Source account number: ");
            string fromAccountNumber = Console.ReadLine();
            Console.Write("Target account number: ");
            string toAccountNumber = Console.ReadLine();
            Console.Write("Amount to transfer: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine();
                try
                {
                    _userService.UserInternalFundsTransfer(_user, fromAccountNumber, toAccountNumber, amount, BankService.ExchangeRates);
                    Console.WriteLine("\nTransfer between your accounts successful.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FundIssueException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input.");
            }
        }

        public void ViewTransactionHistory(Account account)
        {
            try
            {
                var allTransactions = _transactionService.GetTransactionHistory(account);

                if (allTransactions.Count == 0)
                {
                    Console.WriteLine("\nNo transactions found.");
                }
                else
                {
                    foreach (var transaction in allTransactions)
                    {
                        Console.WriteLine(transaction);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewLastNTransactions(Account account)
        {
            Console.Write("How many recent transactions to view? ");
            if (int.TryParse(Console.ReadLine(), out int n))
            {
                var recentTransactions = _accountFilteringService.GetLastNTransactions(account, n);
                Console.WriteLine($"Last {n} Transactions");

                if (recentTransactions.Count == 0)
                {
                    Console.WriteLine("\nNo transactions found.");
                }
                else
                {
                    foreach (var transaction in recentTransactions)
                    {
                        Console.WriteLine(transaction);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nInvalid number.");
            }
        }

        public void ViewDeposits(Account account)
        {
            var deposits = _accountFilteringService.GetDeposits(account);

            if (deposits.Count == 0)
            {
                Console.WriteLine("\nNo deposits found.");
            }
            else
            {
                foreach (var transaction in deposits)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        public void ViewWithdrawals(Account account)
        {
            var withdrawals = _accountFilteringService.GetWithdrawals(account);

            if (withdrawals.Count == 0)
            {
                Console.WriteLine("\nNo withdrawals found.");
            }
            else
            {
                foreach (var transaction in withdrawals)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        public void ViewTransfers(Account account)
        {
            var transfers = _accountFilteringService.GetTransfers(account);

            if (transfers.Count == 0)
            {
                Console.WriteLine("\nNo transfers found.");
            }
            else
            {
                foreach (var transaction in transfers)
                {
                    Console.WriteLine(transaction);
                }
            }
        }

        public void ViewTotalBalance()
        {
            try
            {
                if (_user.Accounts == null)
                {
                    Console.WriteLine("\nNo accounts found.");
                }

                decimal totalInDefaultCurrency = 0;
                foreach (var account in _user.Accounts)
                {
                    Console.WriteLine($"Account: {account.AccountNumber}");
                    Console.WriteLine($"  Balance: {account.Balance:C} {account.Currency}");

                    if (account.Currency != "SEK")
                    {
                        if (BankService.ExchangeRates.TryGetValue(account.Currency, out decimal rate))
                        {
                            decimal balanceInSEK = account.Balance / rate;
                            totalInDefaultCurrency += balanceInSEK;
                        }
                    }
                    else
                    {
                        totalInDefaultCurrency += account.Balance;
                    }
                }

                Console.WriteLine($"\nTotal (in SEK): {totalInDefaultCurrency:C} SEK");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Account GetValidatedAccount(string accountNum)
        {
            var finder = new FinderService(_bank);
            while (true)
            {
                try
                {
                    var account = finder.FindAccountByAccountNumber(_user, accountNum);
                    return account;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Type 'exit' to return to main menu, or enter another account number.");
                    Console.Write("Account number: ");
                    accountNum = Console.ReadLine();

                    if (accountNum?.ToLower() == "exit")
                        return null;
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Type 'exit' to return to main menu, or enter another account number.");
                    Console.Write("Account number: ");
                    accountNum = Console.ReadLine();

                    if (accountNum?.ToLower() == "exit")
                        return null;
                }
            }
        }
    }
}