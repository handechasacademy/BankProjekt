


//Admin could probably be removed...




//using BankProjekt.Core.Accounts;
//using BankProjekt.Core.Users;
//using static BankProjekt.Core.Exceptions.Exceptions;

//namespace BankProjekt.Core.Services
//{
//    internal class AdminService : UserService
//    {
//        private Bank _bank;
//        private BankService _bankservice;
//        public AdminService(Admin admin, Bank bank, BankService bankService) : base(admin)
//        {
//            _bank = bank;
//            _bankservice = bankService;
//        }

          
          //Moved to UserService.cs 
//        //public Account OpenAccount(User user, string accountNumber)
//        //{
//        //    if (user == null || string.IsNullOrWhiteSpace(accountNumber))
//        //        throw new InvalidInputException("User or account number is invalid.");

//        //    if (_bankservice.FindUserById(user.Id) == null)
//        //        _bank.Users.Add(user);

//        //    if (!_bank.AccountNumbers.Add(accountNumber))
//        //        throw new DuplicateException($"Account number '{accountNumber}' already exists.");

//        //    var account = new Account(accountNumber, 0m, user);
//        //    _bank.Accounts[accountNumber] = account;
//        //    (user.Accounts ??= new List<Account>()).Add(account);

//        //    return account;
//        //}

//        //more functions can be added like delete account
//    }
//}
