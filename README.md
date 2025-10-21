BANK PROJECT REPORT
Report 1

Reporter and Date: Sami – 23/09
Participants (attendance): Sami, Hande, Vivienne, Sepideh

Sprint Goal (Definition of Done for the week):
Implement basic use cases — create account, list accounts, view balance — and have a working demo.

What We Did:
Created the User, Account, and Transaction classes.
Implemented deposit and withdrawal functions with validation.
Started developing a simple Console UI to test the demo.

Problems / Blockers:
None – everything worked fine.

Report 2

Reporter and Date: Hande Bengü – 30/09
Participants (attendance): Sami, Hande, Vivienne, Sepideh

Sprint Goal (Definition of Done for the week):
Implement basic use cases — create account, list accounts, view balance — and have a working demo.

What We Did:
Vivienne acted as repository owner and created a new GitHub repository since the previous project had been merged into the main branch.
She also created the User class and Program.cs file.
Sami developed the Account class.
Sepideh created the Transaction class.
Hande acted as the reporter and created the Bank class and UML diagram.
Vivienne and Hande were jointly responsible for merging the code.

Because we ran into issues with the demo, Hande created a new project and repository to start fresh, and Vivienne verified that all files were properly structured in the correct folders.

Problems / Blockers:
Initially, we created the project directly on GitHub without .sln or .csproj files, which caused the program not to run.
We tried adding them later and pushing again, but it still didn’t work.
Eventually, we created a completely new project and repository on GitHub and pushed the same code again, which solved the issue.

Week 41

Reporter and Date: Sepideh – 07/10
Participants (attendance): Sami, Sepideh, Vivienne, Hande

Sprint Goal (Definition of Done for the week):
Fix errors in the Transaction, Bank, and Account classes.

Create new classes: SavingsAccount and CheckingAccount.

Modify some List collections to include Dictionary and HashSet in at least one instance each.

Implement at least three LINQ queries (Where, OrderBy, GroupBy).

Add error handling for empty lists, null values, and invalid input.

Have a working local demo without crashes.

Role Distribution:
Hande – Merge manager
Sepideh – Reporter (with support from Sami and Vivienne)
Vivienne – Code owner
Sami – General support and positive energy

What We Did:
We continued working on the code started in week 39.
Initially, the program was not runnable, but Vivienne, Hande, and Sami worked in parallel to make it functional.
The code had minor errors — a few issues in the output and a missing Transaction class.
Once all errors were fixed and Sepideh added the Transaction class, the code finally worked as expected.
We then merged all working parts successfully.

Afterward, we began planning for week 40.
We realized that most of the week 40 tasks were already implemented, except for adding a Dictionary to the Bank class and creating two subclasses that inherit from Account: SavingsAccount and CheckingAccount.
Once those were completed, we proceeded with the week 41 plan.
Vivienne and Hande refactored several classes to use different data structures (previously we mostly used List<>).

Problems / Blockers:
We were slightly behind schedule, so we needed to catch up.

Week 42
Reporter and Date: Vivienne – 14/10

Participants (attendance): Sami, Sepideh, Hande, Vivienne

Sprint Goal (Definition of Done for the week):
Refactor Program.cs, Bank.cs, and User.cs. Create Admin class. Implement login system with role check (admin vs. user) for differentiated menus.

Role Distribution:
Vivienne – Lead refactor and integration
Hande – Bank and Admin class owner
Sepideh – User class owner
Sami – Account class owner

What We Did:
Refactored core classes and added login/role-based menus. Sepideh added Password and Role to User. Hande integrated Dictionary/HashSet into Bank, moved admin funcs to new Admin class with ShowLargestTransaction() and TotalBalanceSummary(). Sami explored LINQ in Account. Vivienne updated Bank.OpenAccount to boolean return for error handling; added UserMenu/AdminMenu and login in Program.cs.

Problems / Blockers:
Time constraints limited full implementation of all planned methods.

Week 43

Reporter and Date: Hande -21/10

Participants (attendance): Vivienne, Sami, Sepideh and Hande

Sprint Goal (Definition of Done for the week):
Refactor Program.cs, Bank.cs, and Admin.cs to separate business logic from UI, create AccountRepository for transaction and account management with filtering and last-N functionality, implement login with role-based menus, restrict Admin and User access appropriately. Refactor classes SavingsAccount and CheckingAccount and implement SavingsAccount with 3 free withdrawals then fees, and CheckingAccount allowing overdraft up to a set limit.

Role Distribution:
Vivienne – Responsible for code
Hande – Responsible for merging and reporter
Sepideh – Demo 
Sami – Demo

What We Did:
Hande:Refactored Bank.cs, and Admin.cs.Created AccountRepository for transaction and account management, including last-N transactions and type-based filtering.
Vivienne:Implemented SavingsAccount with 3 free withdrawals then fees, and CheckingAccount allowing overdraft up to a set limit.Modified the Withdraw function in Account class to support different account types.Refactored the whole menu for main in order to match the changes made to other classes.
Sami och Sepideh: Worked on demonstrating the project and preparing the demo presentation.

Problems / Blockers:
Hande: Uncertainty about the SearchAccount method: instructions said “Search account by account number or username and display in table format,” and it was unclear whether to show all users with the same name or just one, so implemented it to display all users with matching names in case there are any. Unsure where to create the Repositories folder for AccountRepository, since there are multiple folders like ConsoleUI (Program.cs) and Core (all classes). Was unclear if it should go inside Core or as a separate folder. Ended up creating it under Core folder. Lack of time.
Vivienne: The Account class was not flexible enough for all inherited classes. Our code didn’t fully follow SOLID principles, which caused issues as the project grew and now requires refactoring. Lack of time; too much to do in a short period.
