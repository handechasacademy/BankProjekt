using BankProjekt.Core.Services;
using BankProjekt.Core.Users;

namespace BankProjekt.ConsoleUI.Menus
{
    public class LoginUI
    {
        private readonly ILoginService _loginService;

        public LoginUI(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public User Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("LOGIN PAGE");
                Console.WriteLine("1. Login");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    if (choice == "1")
                    {
                        Console.Write("Enter user ID or username: ");
                        string userIdOrName = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = ReadPassword();
                        Console.WriteLine();

                        var user = _loginService.Login(userIdOrName, password); 

                        if (user != null)
                        {
                            Console.WriteLine($"Welcome, {user.Name}!");
                            return user;
                        }
                        else
                        {
                            Console.WriteLine("Invalid credentials. Try again.");
                            Console.ReadKey();
                        }
                    }
                    else if (choice == "0")
                    {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Login error: " + ex.Message);
                }
            }
        }

        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info;
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Backspace && info.Key != ConsoleKey.Enter)
                {
                    password += info.KeyChar;
                    Console.Write("*");
                }
                else if (info.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            } while (info.Key != ConsoleKey.Enter);
            return password;
        }
    }
}
