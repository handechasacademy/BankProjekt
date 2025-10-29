using BankProjekt.Core;
using BankProjekt.Core.Users;

namespace BankProjekt.Core.Services
{
    internal class AddAdmin
    {
        private readonly Bank _bank;
        private Admin _admin;

        public AddAdmin(Bank bank) { _bank = bank; }

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
