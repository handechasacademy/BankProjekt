using BankProjekt.Core;
using BankProjekt.Core.Users;

namespace BankProjekt.ConsoleUI.ServiceUI
{
    internal class AddAdminUI
    {
        private readonly Bank _bank;
        private Admin _admin;

        public AddAdminUI(Bank bank) { _bank = bank; }

        public void Run()
        {
            Console.Write("User ID ? ");
            string Id = Console.ReadLine();
            Console.Write("Username ? ");
            string Username = Console.ReadLine();
            Console.Write("Password ? ");
            string Password = Console.ReadLine();

            _admin = new Admin(Id, Username, Password);

            try
            {
                _bank.Users.Add(_admin);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to add User Account.");
            }
        }

    }
}
