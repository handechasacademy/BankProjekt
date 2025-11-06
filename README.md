```markdown
# Bank Application - .NET 8 Team Project

A console-based banking application developed in C# with .NET 8, following object-oriented principles. This project was built iteratively by a student team using a shared backlog in Notion.

---

## Project Overview

This application simulates a secure and functional banking system with separate interfaces for users and administrators. It supports core banking operations including account management, transactions, multi-currency support, loans, savings interest, and scheduled batch processing.

The project emphasizes:
- Clean code & OOP principles
- Robust error handling & logging
- Menu-driven console UI with ASCII art branding
- Iterative development via backlog prioritization

---

## Core Features (Required for Passing Grade)

| Feature | Status |
|-------|--------|
| Secure login with username & password | Completed |
| Admin and User views | Completed |
| View accounts and balances | Completed |
| Transfer between own accounts | Completed |
| Transfer to other customers | Completed |
| Open new accounts | Completed |
| Proper error handling & logging | Completed |
| Clean menu-based UI | Completed |

---

## Implemented User Stories

```
As a system owner, I want all users to log in with a unique username and password.
As an admin, I want to create new users in the system.
As a system owner, I want users who fail login 3 times to be locked out.
As a user, I want to see a list of all my bank accounts and their balances.
As a user, I want to transfer money between two of my accounts.
As a user, I want to transfer money to other bank customers.
As a user, I want to open new accounts.
As a user, I want to have accounts in different currencies.
As a bank owner, I want transfers between different currencies to use the correct daily exchange rate (updated by admin).
As a user, I want to open a savings account and see projected interest on deposits.
As a user, I want to take a loan and immediately see the interest I will pay.
As a bank owner, I want to limit loans to 5x the customer's current balance.
As a user, I want to view a transaction log for all my accounts.
As a bank owner, I want the app to look polished with clear menus, colors, and an ASCII logo on login.
As a bank owner, I want transactions to process every 15 minutes in batches, not instantly.
```

All required features are fully implemented. Advanced features like multi-currency, loans, partial interest calculation, and batch processing are included.

---

## Screenshots

### Program Startup
![Startup Screen](https://github.com/user-attachments/assets/4557ab65-d37d-49d6-a7ef-d0855044eb7f)

### Admin Dashboard
![Admin View](https://github.com/user-attachments/assets/730ae4dc-c9ba-45c7-a76e-12a7efeae191)

### User Dashboard
![User View](https://github.com/user-attachments/assets/329a044d-b577-4d08-9542-93b54d33dd2b)

### Account Management
![Account Management](https://github.com/user-attachments/assets/08b84e4b-bc70-4c2d-b32e-7ce352a1c21b)

---

## Tech Stack

- Language: C# 12
- Framework: .NET 8
- Architecture: Layered (Console UI to Services to Data)
- Task Management: Notion (Backlog & Sprint Planning)
- Logging: Custom console logging
- Scheduling: Timer for 15-minute batch transaction processing

---

## Security & Reliability

- Login lockout after 3 failed attempts
- Input validation on all user entries
- Transaction safety via batch processing
- Audit trail with full transaction history
- Role-based access control

---

## Future Enhancements (Backlog Ideas)

- Persistent storage with SQL Server / EF Core
- Web API + Blazor frontend
- Real-time exchange rate API integration
- Deposit maturity & compound interest
- Email/SMS notifications
- Unit tests with xUnit

---

## Team & Process

- Team Size: 3 members
- Methodology: Agile-inspired (sprints, backlog grooming, retros)
- Tools: Git, Notion, VS Code / Visual Studio
- Code Reviews: Pull requests & pair programming

---

## How to Run

1. Clone the repository
2. Open in Visual Studio or VS Code
3. Restore NuGet packages
4. Run `dotnet run` in the project directory

Default admin: `admin` / `admin123`  
Sample user: `john` / `pass123`

---

## License

Educational project not for production use.

---

Developed as part of a .NET 8 course Fall 2025
```
```
