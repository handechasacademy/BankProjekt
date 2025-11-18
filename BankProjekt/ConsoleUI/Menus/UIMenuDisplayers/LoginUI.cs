using BankProjekt.Core.Services;
using BankProjekt.Core.Users;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.ConsoleUI.UIMenuDisplayers
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
                //"bank" header
                Console.WriteLine("oooooooooo.        .o.       ooooo      ooo oooo    oooo \r\n`888'   `Y8b      .888.      `888b.     `8' `888   .8P'  \r\n 888     888     .8\"888.      8 `88b.    8   888  d8'    \r\n 888oooo888'    .8' `888.     8   `88b.  8   88888[      \r\n 888    `88b   .88ooo8888.    8     `88b.8   888`88b.    \r\n 888    .88P  .8'     `888.   8       `888   888  `88b.  \r\no888bood8P'  o88o     o8888o o8o        `8  o888o  o888o \r\n                                                         ");
                Console.WriteLine("Stockholm 1 \n\n");
                //Login
                Console.WriteLine("                  \r\n |   _   _  o ._  \r\n |_ (_) (_| | | | \r\n         _|       ");
                Console.WriteLine("1. Login");
                Console.WriteLine("0. Exit");

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Username: ");
                            string userIdOrName = Console.ReadLine();
                            Console.Write("Password: ");
                            string password = ReadPassword();
                            Console.WriteLine();

                            var user = _loginService.Login(userIdOrName, password);

                            if (user != null)
                            {
                                return user;
                            }
                            else
                            {
                                Console.WriteLine("Invalid credentials. Try again.");
                                Console.ReadKey();
                            }
                            break;

                        case "0":
                            return null;

                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
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
