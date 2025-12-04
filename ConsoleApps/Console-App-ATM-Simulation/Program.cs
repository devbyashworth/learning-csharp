/*
* ** ATM Simulation**

**Scenario:**
A console ATM app where a user can deposit, withdraw, and check balance.

**Requirements:**

* **Class:** `ATMAccount`

  * Properties: `AccountNumber`, `Balance` (private)
  * Methods: `Deposit(decimal amount)`, `Withdraw(decimal amount)`, `GetBalance()`

* **Main Program:**

  * Menu:

    ```
    1. Create Account
    2. Deposit
    3. Withdraw
    4. Check Balance
    5. Exit
    ```

**Concepts covered:** encapsulation (balance only changed via methods), error handling, loops, switch.

**Extra Challenge:**

* Add PIN-based login for accounts.
* Allow transfers between accounts.
* Save/load accounts to a file.

*/

using System.Text.Json;

Console.WriteLine("\n=== ATM Simulation ===\n");

List<ATMAccount> accounts = new List<ATMAccount>();

while (true)
{
    ShowMenu();
    if (!int.TryParse(Console.ReadLine(), out var choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1: CreateAccount(); break;
        case 2: Deposit(); break;
        case 3: Withdraw(); break;
        case 4: Transfer(); break;
        case 5: CheckBalance(); break;
        case 6: SaveAccounts(); break;
        case 7: LoadAccounts(); break;
        case 8: Console.WriteLine("Existing... Goodbye!"); return;
        default: Console.WriteLine("Invalid choice, try again."); break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\nWhat Would You Like To Do?:");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Deposit");
    Console.WriteLine("3. Withdraw");
    Console.WriteLine("4. Transfer");
    Console.WriteLine("5. Check Balance");
    Console.WriteLine("6. Save Accounts");
    Console.WriteLine("7. Load Accounts");
    Console.WriteLine("8. Exit\n");
}

void CreateAccount()
{
    Console.Write("Enter Full Name: ");
    string name = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Name field can't be empty");
        return;
    }

    Console.Write("Enter 16-digit Account Number: ");
    string account = Console.ReadLine()?.Trim() ?? "";
    if (account.Length != 16 || !account.All(char.IsDigit))
    {
        Console.WriteLine("Invalid account number. Must be exactly 16 digits.");
        return;
    }

    // Ensure uniqueness
    if (accounts.Any(acc => acc.AccountNumber == account))
    {
        Console.WriteLine("An account with this number already exists.");
        return;
    }

    Console.Write("Enter 4-digit Pin Number: ");
    string pin = Console.ReadLine()?.Trim() ?? "";
    if (pin.Length != 4 || !pin.All(char.IsDigit))
    {
        Console.WriteLine("Invalid pin number. Must be exactly 4 digits.");
        return;
    }

    Console.Write("Enter Initial Balance: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal balance) || balance < 0)
    {
        Console.WriteLine("Invalid balance amount.");
        return;
    }

    accounts.Add(new ATMAccount(name, account, pin, balance));
    Console.WriteLine($"Bank Account Created. Thank you {name}, your account number is {account}.");
}

void Deposit()
{
    var account = FindAccount();
    if (account == null) return;

    if (!ValidatePin(account)) return;

    Console.Write("Enter amount to deposit: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    account.Deposit(amount);
    Console.WriteLine($"Deposit successful. New Balance: {account.GetBalance():C}");
}

void Withdraw()
{
    var account = FindAccount();
    if (account == null) return;

    if (!ValidatePin(account)) return;

    Console.Write("Enter amount to withdraw: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    if (!account.Withdraw(amount))
    {
        Console.WriteLine("Insufficient funds.");
        return;
    }

    Console.WriteLine($"Withdrawal successful: {amount:C}. New Balance: {account.GetBalance():C}");
}

void Transfer()
{
    Console.Write("Enter Your Account Number: ");
    var senderId = FindAccount();
    if (senderId == null || !ValidatePin(senderId)) return;

    Console.Write("Enter Recipient Account Number: ");
    var receiverId = FindAccount();
    if (receiverId == null || senderId.AccountNumber == receiverId.AccountNumber)
    {
        Console.WriteLine("Invalid recipient account.");
        return;
    }

    Console.Write("Enter amount to transfer: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    if (!senderId.Withdraw(amount))
    {
        Console.WriteLine("Insufficient funds.");
        return;
    }

    receiverId.Deposit(amount);
    Console.WriteLine($"Transfer successful. Your new balance: {senderId.GetBalance():C}");
}

void CheckBalance()
{
    var account = FindAccount();
    if (account == null) return;

    if (!ValidatePin(account)) return;

    Console.WriteLine($"Your current balance is: {account.GetBalance():C}");
}

void SaveAccounts()
{
    var json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("accounts.json", json);
    Console.WriteLine("Accounts saved successfully!");
}

void LoadAccounts()
{
    if (File.Exists("accounts.json"))
    {
        var json = File.ReadAllText("accounts.json");
        accounts = JsonSerializer.Deserialize<List<ATMAccount>>(json) ?? new List<ATMAccount>();
        Console.WriteLine("Accounts loaded successfully!");
    }
    else
    {
        Console.WriteLine("No accounts file found. Starting fresh.");
    }
}

ATMAccount FindAccount()
{
    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine()?.Trim() ?? "";

    var account = accounts.FirstOrDefault(acc =>
    acc.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

    if (account == null)
    {
        Console.WriteLine($"Account Number: {accountNumber} not found.");
    }
    return account;
}

bool ValidatePin(ATMAccount account)
{
    int attempts = 0;
    while (attempts < 3)
    {
        Console.Write("Enter Pin Number: ");
        string pinNumber = Console.ReadLine()?.Trim() ?? "";
        if (account.ValidatePin(pinNumber))
        {
            Console.WriteLine($"Welcome {account.FullName}");
            return true;
        }
        Console.WriteLine("Invalid Pin Number.");
        attempts++;
    }
    Console.WriteLine("Too many failed attempts.");
    return false;
}

class ATMAccount
{
    public string FullName { get; }
    public string AccountNumber { get; }
    private string PinHash { get; }
    private decimal Balance { get; set; }

    public ATMAccount(string name, string accountNumber, string pinNumber, decimal balance)
    {
        FullName = name;
        AccountNumber = accountNumber;
        PinHash = ComputeHash(pinNumber);
        Balance = balance;
    }

    private string ComputeHash(string input)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));
    }

    public bool ValidatePin(string pin) => PinHash == ComputeHash(pin);

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }

    public decimal GetBalance() => Balance;

}
