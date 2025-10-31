using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core
{
    public interface ILoginService
    {
        User Login(string username, string password);
    }
}