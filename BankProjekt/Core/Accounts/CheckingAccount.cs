using BankProjekt.Core.Users;
using System;

namespace BankProjekt.Core.Accounts
{
    internal class CheckingAccount : Account
    {
        public decimal AllowedDebt;
        public decimal InterestRate;

        public CheckingAccount(string accountNumber, decimal balance, User owner)
            : base(accountNumber, balance, owner)
        {
            AllowedDebt = -balance * 5m;
        }

        public override decimal Withdraw(decimal amount)
        {
            decimal newBalance = Balance - amount;

            if (newBalance < AllowedDebt)
            {
                Console.WriteLine($"Your debt would exceed {AllowedDebt:C}. Withdrawal failed.");
            }
            else
            {
                base.Withdraw(amount);

                if (Balance < 0)
                {
                    decimal interest = Math.Abs(Balance) * InterestRate;
                    Console.WriteLine($"You now have a debt of {Balance:C}. Interest will be {interest:C} per period.");
                }
            }
            return newBalance;
        }
    }
}
