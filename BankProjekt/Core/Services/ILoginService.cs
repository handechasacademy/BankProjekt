using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProjekt.Core.Users;

namespace BankProjekt.Core.Services
{
    public interface ILoginService
    {
        User? CurrentUser { get; }
        User Login(string username, string password);
        void Logout();
    }
}
