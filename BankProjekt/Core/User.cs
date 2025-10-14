using BankProjekt.Core;
using System;
using System.Collections.Generic;

public class User
{
    
    public string Id { get; set; }           
    public string Name { get; set; }
    public string Role { get; set; }
    public string Password { get; private set; } 
    public List<Account> Accounts { get; set; } = new List<Account>();

    public User(string id, string name, string password, string role)
    {
        Id = id;
        Name = name;
        SetPassword(password);
        Role = role;
    }

    public bool SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long.");
            return false;
        }

        Password = password;
        return true;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"User: {Name} (ID: {Id}) - Accounts: {Accounts.Count}");
    }
}
