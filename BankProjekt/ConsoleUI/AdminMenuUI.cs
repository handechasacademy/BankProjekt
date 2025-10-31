using BankProjekt.Core;

namespace BankProjekt.ConsoleUI
{
    internal class AdminMenuUI
    {
        private Bank bank;
        private User loggedInUser;

        public AdminMenuUI(Bank bank, User loggedInUser)
        {
            this.bank = bank;
            this.loggedInUser = loggedInUser;
        }

        internal void Run()
        {
            throw new NotImplementedException();
        }
    }
}