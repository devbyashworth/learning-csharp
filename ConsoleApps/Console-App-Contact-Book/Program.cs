/*
* Contact Book(Console or WinForms)
Goal: Build an app to store, update, delete, and search contacts.

Core Concepts: Object - Oriented Programming, Lists, CRUD operations

How to Approach:

Create a Contact class with Name, Phone, Email.

Use a List<Contact> to store data.

Build a menu-based system to add/edit/delete/search contacts.

Go Further:

Save / load to a CSV file.

Use Dictionary<string, Contact> for faster search.

Build a simple WinForms UI.
*/

// Call LoadFromCSV("contacts.csv") at the start of your program and SaveToCSV("contacts.csv") before exiting.


using System.Linq;

Console.WriteLine("=== Console App Contact Book ===");

List<Contact> contactBook = new List<Contact>();

while (true)
{
    ShowMenu();

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            AddContact();
            break;
        case 2:
            ViewContacts();
            break;
        case 3:
            EditContact();
            break;
        case 4:
            DeleteContact();
            break;
        case 5:
            SearchContact();
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
    Console.WriteLine("1. Add Contact");
    Console.WriteLine("2. View Contacts");
    Console.WriteLine("3. Edit Contact");
    Console.WriteLine("4. Delete Contact");
    Console.WriteLine("5. Search Contact");
    Console.WriteLine("6. Exit\n");
}

void AddContact()
{
    Console.Write("Enter Contact Name: ");
    string name = Console.ReadLine()?.Trim() ?? "";
    Console.Write("Enter Contact Phone: ");
    string phone = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
    {
        Console.WriteLine("Name and Phone cannot be empty.");
        return;
    }

    contactBook.Add(new Contact(name, phone));
    Console.WriteLine("Contact Added.");
}

void ViewContacts()
{
    if (contactBook.Count == 0)
    {
        Console.WriteLine("No contacts found.");
        return;
    }

    Console.WriteLine("\nAll Contacts:");
    for (int i = 0; i < contactBook.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {contactBook[i].Name} {contactBook[i].Phone}");
    }
}

void EditContact()
{
    ViewContacts();
    if (contactBook.Count == 0) return;

    Console.Write("Enter Contact Name to Edit: ");
    string name = Console.ReadLine()?.Trim() ?? "";

    var contact = contactBook.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    if (contact != null)
    {
        Console.Write("Enter New Name (leave blank to keep current): ");
        string newName = Console.ReadLine()?.Trim() ?? "";
        if (!string.IsNullOrEmpty(newName)) contact.Name = newName;

        Console.Write("Enter New Phone (leave blank to keep current): ");
        string newPhone = Console.ReadLine()?.Trim() ?? "";
        if (!string.IsNullOrEmpty(newPhone)) contact.Phone = newPhone;

        Console.WriteLine("Contact updated successfully.");
    }
    else
    {
        Console.WriteLine("Contact not found.");
    }
}

void DeleteContact()
{
    ViewContacts();
    if (contactBook.Count == 0) return;

    Console.Write("Enter Contact Name to Delete: ");
    string name = Console.ReadLine()?.Trim() ?? "";

    var contact = contactBook.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    if (contact != null)
    {
        Console.Write($"Are you sure you want to delete '{contact.Name}'? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
        {
            contactBook.Remove(contact);
            Console.WriteLine("Contact deleted.");
        }
    }
    else
    {
        Console.WriteLine("Contact not found.");
    }
}

void SearchContact()
{
    if (contactBook.Count == 0)
    {
        Console.WriteLine("No contacts to search.");
        return;
    }

    Console.Write("Enter name or phone to search: ");
    string query = Console.ReadLine()?.Trim() ?? "";

    var results = contactBook
        .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                 || c.Phone.Contains(query, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (results.Any())
    {
        Console.WriteLine("\nSearch Results:");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i].Name} {results[i].Phone}");
        }
    }
    else
    {
        Console.WriteLine("No contacts found.");
    }
}


void SaveToCSV(string filePath)
{
    var lines = contactBook.Select(c => $"{c.Name},{c.Phone},{c.Email}");
    File.WriteAllLines(filePath, lines);
    Console.WriteLine("Contacts saved to file.");
}

void LoadFromCSV(string filePath)
{
    if (!File.Exists(filePath)) return;

    var lines = File.ReadAllLines(filePath);
    contactBook = lines
        .Select(line => line.Split(','))
        .Where(parts => parts.Length >= 2)
        .Select(parts => new Contact(parts[0], parts[1], parts.Length > 2 ? parts[2] : ""))
        .ToList();

    Console.WriteLine("Contacts loaded from file.");
}

class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public object Email { get; internal set; }

    public Contact(string name, string phone, string email = "your@emailaddress.com")
    {
        Name = name;
        Phone = phone;
        Email = email;
    }
}