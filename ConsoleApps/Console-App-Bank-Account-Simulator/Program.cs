/*
* Bank Account Simulator
Goal: Simulate a user managing a bank account (deposit, withdraw, check balance).

Core Concepts: Classes, encapsulation, methods, error handling

How to Approach:

Create a BankAccount class with properties: Balance, AccountNumber.

Methods: Deposit(), Withdraw(), GetBalance().

Add input validation: Cannot withdraw more than the balance.

Go Further:

Add multiple accounts.

Track transactions using a List<string> log.

Generate simple reports.
*/

Console.WriteLine("=== Bank Account Simulator ===");

List<Account> accountsList = new List<Account>();

while (true)
{
    ShowMenu();

    Console.Write("Enter Number to be Serverd: ");
    if (!int.TryParse(Console.ReadLine(), out var choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            AddAccount();
            break;
        case 2:
            ViewAccounts();
            break;
        case 3:
            Deposit();
            break;
        case 4:
            Withdraw();
            break;
        case 5:
            Balance();
            break;
        case 6:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Account");
    Console.WriteLine("2. View Accounts");
    Console.WriteLine("3. Deposit");
    Console.WriteLine("4. Withdraw");
    Console.WriteLine("5. Check Balance");
    Console.WriteLine("6. Exit\n");
}

void AddAccount()
{
    Console.Write("Enter Account Name: ");
    string name = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Enter Account Phone: ");
    string phone = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Enter Account Email: ");
    string email = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Enter 16-digit Account Number: ");
    string account = Console.ReadLine()?.Trim() ?? "";

    if (account.Length != 16)
    {
        Console.WriteLine("Invalid account number. Must be exactly 16 digits.");
        return;
    }

    Console.Write("Enter Initial Balance: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal balance) || balance < 0)
    {
        Console.WriteLine("Invalid balance amount.");
        return;
    }

    accountsList.Add(new Account(name, phone, email, account, balance));
    Console.WriteLine($"Bank Account Created. Thank you {name}, your account number is {account}.");
}

void ViewAccounts()
{
    if (accountsList.Count == 0)
    {
        Console.WriteLine("No Bank Accounts Found");
        return;
    }

    Console.WriteLine("Bank Accounts:");
    foreach (var acc in accountsList)
    {
        Console.WriteLine($"{acc.Name} | {acc.Phone} | {acc.Email} | {acc.AccountNumber} | Balance: {acc.Balance:C}");
    }
}

void Deposit()
{
    var acc = FindAccount();
    if (acc == null) return;

    Console.Write("Enter amount to deposit: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    acc.Balance += amount;
    Console.WriteLine($"Deposit successful. New Balance: {acc.Balance:C}");
}

void Withdraw()
{
    var acc = FindAccount();
    if (acc == null) return;

    Console.Write("Enter amount to withdraw: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    if (amount > acc.Balance)
    {
        Console.WriteLine("Insufficient funds.");
        return;
    }

    acc.Balance -= amount;
    Console.WriteLine($"Withdrawal successful. New Balance: {acc.Balance:C}");
}

void Balance()
{
    var acc = FindAccount();
    if (acc == null) return;

    Console.WriteLine($"Current Balance: {acc.Balance:C}");
}

Account FindAccount()
{
    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine()?.Trim() ?? "";

    var acc = accountsList.FirstOrDefault(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

    if (acc == null)
    {
        Console.WriteLine("Account not found.");
    }

    return acc;
}

class Account
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public Account(string name, string phone, string email, string account, decimal balance)
    {
        Name = name;
        Phone = phone;
        Email = email;
        AccountNumber = account;
        Balance = balance;
    }
}
