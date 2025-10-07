
using BankProject.Core;
using BankProjekt.Core;
using System;
using System.Security.Principal;

namespace BankProjekt.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- MAIN MENU ----");
                Console.WriteLine("1. Open a new account");
                Console.WriteLine("2. Find and manage an account");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        OpenAccountMenu(bank);
                        break;
                    case "2":
                        ManageAccountMenu(bank);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void OpenAccountMenu(Bank bank)
        {
            Console.WriteLine("---- OPEN A NEW ACCOUNT ----");
            Console.Write("Enter User ID: ");
            string userId = Console.ReadLine();

            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            User newUser = new User(userId, userName);
            bank.OpenAccount(newUser, accountNumber);

            Console.WriteLine("\nAccount opening attempt complete. Press any key to return to main menu...");
            Console.ReadKey();
        }

        static void ManageAccountMenu(Bank bank)
        {
            Console.WriteLine("---- FIND ACCOUNT ----");
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Account selectedAccount = bank.FindAccount(accountNumber);

            if (selectedAccount == null)
            {
                Console.WriteLine("Account not found. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nAccount '{selectedAccount.AccountNumber}' selected.");

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"---- ACCOUNT MENU: {selectedAccount.AccountNumber} ----");
                Console.WriteLine($"Current Balance: {selectedAccount.Balance:C}");
                Console.WriteLine("1. Deposit funds");
                Console.WriteLine("2. Withdraw funds");
                Console.WriteLine("3. Transfer funds");
                Console.WriteLine("4. View transaction history");
                Console.WriteLine("0. Return to main menu");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        HandleDeposit(selectedAccount);
                        break;
                    case "2":
                        HandleWithdraw(selectedAccount);
                        break;
                    case "3":
                        HandleTransfer(bank, selectedAccount);
                        break;
                    case "4":
                        selectedAccount.GetTransactionHistory();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void HandleDeposit(Account account)
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                account.Deposit(amount);
                Console.WriteLine($"Successfully deposited {amount:C}. New balance is {account.Balance:C}.");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void HandleWithdraw(Account account)
        {
            Console.Write("Enter amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0 && amount <= account.Balance)
            {
                account.Withdraw(amount);
                Console.WriteLine($"Successfully withdrew {amount:C}. New balance is {account.Balance:C}.");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void HandleTransfer(Bank bank, Account sourceAccount)
        {
            Console.Write("Enter destination account number: ");
            string destAccountNumber = Console.ReadLine();

            Account destinationAccount = bank.FindAccount(destAccountNumber);

            if (destinationAccount == null)
            {
                Console.WriteLine("Destination account not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter amount to transfer: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (sourceAccount.Balance >= amount)
                {
                    sourceAccount.Withdraw(amount);
                    destinationAccount.Deposit(amount);
                    Console.WriteLine($"Successfully transferred {amount:C} from {sourceAccount.AccountNumber} to {destinationAccount.AccountNumber}.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds for this transfer.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}