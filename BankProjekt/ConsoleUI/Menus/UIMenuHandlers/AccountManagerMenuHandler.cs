using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                try
                {
                    account.Deposit(depositAmount);
                    Console.WriteLine("Deposit successful.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public void HandleWithdrawal(Account account)
        {
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
            {
                try
                {
                    _userService.Withdraw(account, withdrawAmount);
                    Console.WriteLine("Withdrawal successful.");
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
                Console.WriteLine("Invalid amount.");
            }
        }

        public void HandleTransferToUser(Account account)
        {
            Console.Write("Enter receiver username: ");
            string receiverUsername = Console.ReadLine();
            Console.Write("Enter target account number: ");
            string receiverAccountNum = Console.ReadLine();
            Console.Write("Enter amount to transfer: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
            {
                try
                {
                    _bankService.TransferFundsBetweenUsers(account.AccountNumber, receiverAccountNum, transferAmount);
                    Console.WriteLine("Transfer to user successful.");
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
                Console.WriteLine("Invalid input.");
            }
        }

        public void HandleInternalTransfer(Account account)
        {
            Console.Write("Enter your source account number: ");
            string fromAccountNumber = Console.ReadLine();
            Console.Write("Enter your target account number: ");
            string toAccountNumber = Console.ReadLine();
            Console.Write("Enter amount to transfer: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    _userService.UserInternalFundsTransfer(_user, fromAccountNumber, toAccountNumber, amount, BankService.ExchangeRates);
                    Console.WriteLine("Transfer between your accounts successful.");
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
                Console.WriteLine("Invalid input.");
            }
        }

        public void ViewTransactionHistory(Account account)
        {
            try
            {
                var allTransactions = _transactionService.GetTransactionHistory(account);
                Console.WriteLine("---- Transaction History ----");

                if (allTransactions.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                }
                else
                {
                    foreach (var transaction in allTransactions)
                    {
                        Console.WriteLine($"ID: {transaction.Id}");
                        Console.WriteLine($"Account Number: {transaction.AccountNumber}");
                        Console.WriteLine($"Amount: {transaction.Amount:C}");
                        Console.WriteLine($"Date and Time: {transaction.Timestamp}");
                        Console.WriteLine($"Type: {transaction.Type}");
                        Console.WriteLine();
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
                Console.WriteLine($"---- Last {n} Transactions ----");

                if (recentTransactions.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                }
                else
                {
                    foreach (var transaction in recentTransactions)
                    {
                        Console.WriteLine($"Amount: {transaction.Amount:C}, Date: {transaction.Timestamp}, Type: {transaction.Type}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }

        public void ViewDeposits(Account account)
        {
            var deposits = _accountFilteringService.GetDeposits(account);
            Console.WriteLine("---- All Deposits ----");

            if (deposits.Count == 0)
            {
                Console.WriteLine("No deposits found.");
            }
            else
            {
                foreach (var transaction in deposits)
                {
                    Console.WriteLine($"Amount: {transaction.Amount:C}, Date: {transaction.Timestamp}, Type: {transaction.Type}");
                }
            }
        }

        public void ViewWithdrawals(Account account)
        {
            var withdrawals = _accountFilteringService.GetWithdrawals(account);
            Console.WriteLine("---- All Withdrawals ----");

            if (withdrawals.Count == 0)
            {
                Console.WriteLine("No withdrawals found.");
            }
            else
            {
                foreach (var transaction in withdrawals)
                {
                    Console.WriteLine($"Amount: {transaction.Amount:C}, Date: {transaction.Timestamp}, Type: {transaction.Type}");
                }
            }
        }

        public void ViewTransfers(Account account)
        {
            var transfers = _accountFilteringService.GetTransfers(account);
            Console.WriteLine("---- All Transfers ----");

            if (transfers.Count == 0)
            {
                Console.WriteLine("No transfers found.");
            }
            else
            {
                foreach (var transaction in transfers)
                {
                    Console.WriteLine($"Amount: {transaction.Amount:C}, Date: {transaction.Timestamp}, Type: {transaction.Type}");
                }
            }
        }

        public void ViewTotalBalance()
        {
            try
            {
                Console.WriteLine("---- Total Balance Across All Accounts ----");

                if (_user.Accounts == null)
                {
                    Console.WriteLine("No accounts found.");
                    return;
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
    }
}