using System;
using System.Collections.Generic;

public class BankAccount
{
    public string AccountNumber { get; set; }
    public string PIN { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(string accountNumber, string pin, decimal balance)
    {
        AccountNumber = accountNumber;
        PIN = pin;
        Balance = balance;
    }
}

public class ATM
{
    private Dictionary<string, BankAccount> accounts;

    public ATM()
    {
        accounts = new Dictionary<string, BankAccount>();
        accounts.Add("123456789", new BankAccount("123456789", "1234", 143.00m));
        accounts.Add("987654321", new BankAccount("987654321", "4321", 130.00m));
    }

    public BankAccount AuthenticateUser(string accountNumber, string pin)
    {
        // checks to see if the account number exists within the dictionary
        if(accounts.ContainsKey(accountNumber))
        {
            // fetch the account
            BankAccount account = accounts[accountNumber];
            if(account.PIN == pin)
            {
                return account; // if the pin matches return the authenticated account
            }
        }

        return null; // otherwise return null
    }

    public decimal CheckBalance(BankAccount account)
    {
        return account.Balance;
    }

    public void DepositFunds(BankAccount account, decimal amount)
    {
        account.Balance += amount;
    }

    public bool WithdrawFunds(BankAccount account, decimal amount)
    {
        if (amount <= account.Balance)
        {
            account.Balance -= amount;
            return true; // if withdraw is successful (has enough funds)
        }
        else
        {
            return false; // shows message to say otherwise
        }
}

public class Program
{
    public static void Main(string[] args)
    {
        ATM atm = new ATM(); // create atm instance
        StartATM(atm); // then run it

    }

    public static void StartATM(ATM atm)
    {
        while (true)
        {
            Console.WriteLine("\nPlease Enter account number: ");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("\nPlease Enter PIN: ");
            string accountPin = Console.ReadLine();

            BankAccount currentUser = atm.AuthenticateUser(accountNumber, accountPin);

            if(currentUser != null)
            {
                Console.WriteLine("\nAuthentication Successful");
                ShowMenu(atm, currentUser);

            }
            else
            {
                Console.WriteLine("\nAccount Number or PIN is incorrect...");
            }
        }
    }

    public static void ShowMenu(ATM atm, BankAccount currentUser)
    {
            while (true)
            {
                Console.WriteLine("\nMain Menu: ");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit Funds");
                Console.WriteLine("3. Withdraw Funds");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Please Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        decimal balance = atm.CheckBalance(currentUser);
                        Console.WriteLine($"\nYour current balance is: {balance:C}");
                        break;
                    case "2":
                        Console.WriteLine("Enter Amount to deposit: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine()); // parse string to int
                        atm.DepositFunds(currentUser, depositAmount);
                        Console.WriteLine("\nDeposit successful");
                        break;
                    case "3":
                        Console.WriteLine("Enter Amount to Withdraw: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        bool withdrawResult = atm.WithdrawFunds(currentUser, withdrawAmount);
                        if (withdrawResult)
                        {
                            Console.WriteLine("\nWithdraw Successful");
                        }
                        else
                        {
                            Console.WriteLine("\nInsufficient Funds! Withdraw Failed!");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Thank You for using the ATM!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Try Again...");
                        break;
                }
            }
        }
    }
}