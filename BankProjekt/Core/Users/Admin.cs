using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjekt.Core.Users
{
    public class Admin : User
    {
        public Admin(string id, string username, string password) : base(id, username, password){}
    }
}
