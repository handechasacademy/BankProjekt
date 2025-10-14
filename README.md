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
