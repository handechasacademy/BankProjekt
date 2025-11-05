using BankProjekt.Core.Accounts;
using BankProjekt.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankProjekt.Core.Exceptions.Exceptions;

namespace BankProjekt.Core.Services
{
    internal class FinderService
    {
        private readonly Bank _bank;

        public FinderService(Bank bank) {  _bank = bank; }

        public Account FindAccountByAccountNumber(User user, string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidInputException("Account number is empty.");
            var account = user.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null) //this will crash the program
                throw new NotFoundException($"Account '{accountNumber}' not found for user.");
            return account;
        }
        public User FindUserById(string id)
        {
            var user = _bank.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new NotFoundException($"User with ID '{id}' was not found.");
            return user;
        }
    }
}
