using BankProjekt.Core;

namespace BankProjekt.ConsoleUI
{
    internal class UserMenuUI
    {
        private Bank bank;
        private User loggedInUser;

        public UserMenuUI(Bank bank, User loggedInUser)
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