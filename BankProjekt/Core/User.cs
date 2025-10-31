using BankProjekt.Core;
using BankProjekt.Core.Accounts;
using System;
using System.Collections.Generic;

public class User
{
    private string v1;
    private string v2;
    private string v3;

    public string Id { get; set; }           
    public string Name { get; set; }
    public string Role { get; set; }
    public string Password { get; private set; }
    public bool IsAdmin { get; set; }

    public List<Account> Accounts { get; set; } = new List<Account>();

    public User(string id, string name, string password, string role)
    {
        Id = id;
        Name = name;
        SetPassword(password);
        IsAdmin = false;
        Role = role;
    }

    public User(string v1, string v2, string v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
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
