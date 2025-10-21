
using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using System;
using System.Security.Principal;

namespace BankProjekt.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            string LoggedInUser = Login(bank);
            OpenAccountMenu(bank);
        }

        static string Login(Bank bank)
        {
            Console.WriteLine("---- USER LOGIN ----");
            Console.Write("Enter User ID: ");
            string userId = Console.ReadLine();

            var user = bank.FindUserById(userId);

            if (user != null)
            {
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                // You don't have a Password or Role in the User class currently.
                // Add them to User.cs if needed for login & role check.
                if (user.Password == password) // You'll need to implement this field!
                {
                    if (user.IsAdmin) // Also implement 'Role' property in User.cs
                    {
                        AdminMenu(bank);
                    }
                    else
                    {
                        UserMenu(bank);
                    }
                    return userId;
                }
                else
                {
                    Console.WriteLine("Felaktigt lösenord.");
                }
            }
            else
            {
                Console.WriteLine("Användare hittades inte.");
            }

            return null;
        }


        static void AdminMenu(Bank bank)
        {
            Admin admin = new Admin();
            admin.Users = bank.Users;
            admin.Accounts = bank.Accounts;
            admin.AccountNumbers = bank.AccountNumbers;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- ADMIN MENU ----");
                Console.WriteLine("1. Visa alla användare");
                Console.WriteLine("2. Skapa nytt konto för ny användare");
                Console.WriteLine("3. Visa största insättning/uttag per användare");
                Console.WriteLine("4. Summera total saldo per användare i fallande ordning");
                Console.WriteLine("5. Hitta användaren med flest transaktioner");
                Console.WriteLine("6. Sök konto via kontonummer eller användarnamn");
                Console.WriteLine("7. Visa transaktioner med tidsstämpel och användarnamn");
                Console.WriteLine("0. Logga ut");
                Console.Write("Ditt val: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        admin.ShowAllUsers();
                        break;

                    case "2":
                        Console.Write("Ange User ID: ");
                        string userId = Console.ReadLine();

                        Console.Write("Ange användarnamn: ");
                        string userName = Console.ReadLine();

                        Console.Write("Ange kontonummer: ");
                        string accountNumber = Console.ReadLine();

                        User newUser = new User(userId, userName);
                        admin.OpenAccount(newUser, accountNumber);
                        break;

                    case "3":
                        admin.ShowLargestTransaction();
                        break;

                    case "4":
                        admin.TotalBalanceSummary();
                        break;

                    case "5":
                        admin.AccountWithMostTransactions();
                        break;

                    case "6":
                        Console.Write("Sök kontonummer eller användarnamn: ");
                        string searchInput1 = Console.ReadLine();
                        admin.SearchAccount(searchInput1);
                        break;

                    case "7":
                        Console.Write("Sök kontonummer eller användarnamn: ");
                        string searchInput2 = Console.ReadLine();
                        admin.SearchTransactionsWithTimestamp(searchInput2);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta...");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
            }
        }


        static void UserMenu(Bank bank)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- USER MENU ----");
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

            // Create a new admin instance to use its account creation method
            Admin admin = new Admin();
            admin.Users = bank.Users;
            admin.Accounts = bank.Accounts;
            admin.AccountNumbers = bank.AccountNumbers;

            User existingUser = bank.FindUserById(userId);
            User newUser = existingUser ?? new User(userId, userName);

            admin.OpenAccount(newUser, accountNumber);

            Console.WriteLine("\nAccount opening attempt complete. Press any key to return to main menu...");
            Console.ReadKey();
        }

        static void ManageAccountMenu(Bank bank)
        {
            Console.WriteLine("---- FIND ACCOUNT ----");
            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Account selectedAccount = bank.FindAccountByAccountNumber(accountNumber);

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
                Console.WriteLine("Invalid amount. Please enter a valid amount.");
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
                Console.WriteLine("Invalid amount. Please enter a valid amount.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void HandleTransfer(Bank bank, Account sourceAccount)
        {
            Console.Write("Enter destination account number: ");
            string destAccountNumber = Console.ReadLine();

            Account destinationAccount = bank.FindAccountByAccountNumber(destAccountNumber);

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
                Console.WriteLine("Invalid amount. Please enter a valid amount.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}