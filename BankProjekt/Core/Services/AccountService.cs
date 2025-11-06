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
    internal class AccountService
    {
        private User _user;
        public AccountService(User user) { _user = user; }

        public decimal GetAccountBalanceSum()
        {
            if (_user.Accounts == null)
                throw new NotFoundException("User has no accounts.");
            return _user.Accounts.Sum(a => a.Balance);
        }
    }
}
