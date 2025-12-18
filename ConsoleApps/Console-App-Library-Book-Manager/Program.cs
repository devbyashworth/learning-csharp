// See https://aka.ms/new-console-template for more information
/*
* Library Book Manager *

* Scenario:
Manage a small library system where you can add books, lend books, return books, and view all available books.

**Requirements:**

* **Class:** `Book`

  * Properties: `Title`, `Author`, `IsAvailable`
  * Methods: `Borrow()`, `ReturnBook()`

* **Main Program:**

  * Store books in a `List<Book>`.
  * Menu:

    ```
    1. Add Book
    2. Borrow Book
    3. Return Book
    4. View All Books
    5. Exit
    ```

**Concepts covered:** classes, lists, methods, switch, encapsulation, error handling.

**Extra Challenge:**

* Track which student borrowed the book.
* Show only available books when borrowing.
* Save/load library inventory to a file.
*/

using System.Text;
using System.Text.Json;

Console.WriteLine("=== Library Book Manager ===");

List<Book> books = new List<Book>();

// books.Add(new Book("To Kill a Mockingbird", "Harper Lee", Book.GenerateIsbn13())); ISBN as an argument
books.Add(new Book("To Kill a Mockingbird", "Harper Lee"));
books.Add(new Book("1984", "George Orwell", false));
books.Add(new Book("The Pragmatic Programmer", "Andrew Hunt & David Thomas"));
books.Add(new Book("Sapiens: A Brief History of Humankind", "Yuval Noah Harari", false));
books.Add(new Book("Atomic Habits", "James Clear"));

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
        case 1:
            AddBook(books);
            break;
        case 2:
            BorrowBook(books);
            break;
        case 3:
            ReturnBook(books);
            break;
        case 4:
            ViewAllBooks(books);
            break;
        case 5:
            ShowAvailableBooks(books);
            break;
        case 6:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("\n1.Add Book");
    Console.WriteLine("2.Borrow Book");
    Console.WriteLine("3.Return Book");
    Console.WriteLine("4.View All Books");
    Console.WriteLine("5.Show All Available Books");
    Console.WriteLine("6.Exit\n");
}

static void AddBook(List<Book> books)
{
    Console.WriteLine("Enter Book Title: ");
    string title = Console.ReadLine()?.Trim() ?? "";
    Console.WriteLine("Enter Book Author/s: ");
    string author = Console.ReadLine()?.Trim() ?? "";
    Console.WriteLine("Is Book Available (true / false): ");
    bool isAvailable = Convert.ToBoolean(Console.ReadLine()?.Trim() ?? "");

    if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
    {
        Console.WriteLine("Title and Author field/s can't be empty");
        return;
    }

    books.Add(new Book(title, author, isAvailable));
}

static void BorrowBook(List<Book> books)
{
    ViewAllBooks(books);

    Console.Write("Enter Book ISBN: ");
    string bookISBN = Console.ReadLine()?.Trim() ?? "";
    var book = books.FirstOrDefault(b => b.ISBN.Equals(bookISBN, StringComparison.OrdinalIgnoreCase));

    if (book == null)
    {
        Console.WriteLine("Book not found.");
        return;
    }
    if (!book.IsAvailable)
    {
        Console.WriteLine($"{book.Title} is already borrowed by {book.CurrentBorrow?.Borrower}.");
        return;
    }

    Console.Write("Enter your name: ");
    string borrower = Console.ReadLine()?.Trim() ?? "";
    Console.Write("Enter due date (yyyy-mm-dd): ");
    DateTime dueDate = DateTime.TryParse(Console.ReadLine(), out var date) ? date : DateTime.Now.AddDays(14);

    if (book.Borrow(borrower, dueDate))
        Console.WriteLine($"{book.Title} successfully borrowed by {borrower} until {dueDate:d}.");

    bool isOverdue = !book.IsAvailable && book.CurrentBorrow?.DueDate < DateTime.Now;
    if (isOverdue)
    {
        Console.WriteLine($"⚠️ {book.Title} is overdue! Due on {book.CurrentBorrow?.DueDate:d}");
    }
}


static void ReturnBook(List<Book> books)
{
    Console.Write("Enter Book ISBN to return: ");
    string bookISBN = Console.ReadLine()?.Trim() ?? "";
    var book = books.FirstOrDefault(b => b.ISBN.Equals(bookISBN, StringComparison.OrdinalIgnoreCase));

    if (book == null)
    {
        Console.WriteLine("Book not found.");
        return;
    }

    if (book.IsAvailable)
    {
        Console.WriteLine($"{book.Title} is already available in the library.");
        return;
    }

    var borrower = book.CurrentBorrow?.Borrower ?? "Unknown";

    if (book.ReturnBook())
    {
        Console.WriteLine($"{book.Title} has been returned successfully by {borrower}.");
    }
    else
    {
        Console.WriteLine($"Failed to return {book.Title}.");
    }

    bool isOverdue = !book.IsAvailable && book.CurrentBorrow?.DueDate < DateTime.Now;
    if (isOverdue)
    {
        Console.WriteLine($"⚠️ {book.Title} is overdue! Due on {book.CurrentBorrow?.DueDate:d}");
    }
}

static void ViewAllBooks(List<Book> books)
{
    if (books.Count == 0)
    {
        Console.WriteLine("No Books Found!");
        return;
    }

    Console.WriteLine("Books:");
    Console.WriteLine($"{"Title",-45} {"Author",-30} {"ISBN",-15} {"Available",-10} {"Borrower",-30}");
    foreach (var book in books)
    {
        string borrowerInfo = book.IsAvailable ? "—" : book.CurrentBorrow?.Borrower ?? "Unknown";
        Console.WriteLine($"{book.Title,-45} {book.Author,-30} {book.ISBN,-15} {book.IsAvailable,-10} {borrowerInfo,-30}");
    }
}

static void ShowAvailableBooks(List<Book> books)
{
    var availableBooks = books.Where(b => b.IsAvailable).ToList();

    if (availableBooks.Count == 0)
    {
        Console.WriteLine("No available books to borrow.");
        return;
    }

    Console.WriteLine("Available Books:");
    foreach (var book in availableBooks)
    {
        Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
    }
}


static void SaveLibrary(List<Book> books)
{
    var options = new JsonSerializerOptions { WriteIndented = true };
    string json = JsonSerializer.Serialize(books, options);
    File.WriteAllText("library.json", json);
}

static List<Book> LoadLibrary()
{
    if (!File.Exists("library.json")) return new List<Book>();
    string json = File.ReadAllText("library.json");
    return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
}

class BorrowRecord
{
    public required string Borrower { get; set; }
    public DateTime BorrowedOn { get; set; }
    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"{Borrower} (Borrowed: {BorrowedOn:d}, Due: {DueDate:d})";
    }
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
    public string ISBN { get; }
    private static readonly Random Rng = new Random();

    public BorrowRecord? CurrentBorrow { get; private set; }

    public Book(string title, string author, bool isAvailable = true)
    {
        Title = title;
        Author = author;
        ISBN = GenerateIsbn13();
        IsAvailable = isAvailable;
    }

    public static string GenerateIsbn13()
    {
        const string isbnPrefix = "978"; // Common ISBN-13 prefix
        var digits = new StringBuilder();

        for (int i = 0; i < 9; i++)
        {
            digits.Append(Rng.Next(10)); // Append a random digit (0–9)
        }

        digits.Append(Rng.Next(10)); // Add a final digit to complete 13 digits

        return isbnPrefix + digits.ToString();
    }


    public bool Borrow(string borrower, DateTime dueDate)
    {
        if (!IsAvailable) return false;
        IsAvailable = false;
        CurrentBorrow = new BorrowRecord
        {
            Borrower = borrower,
            BorrowedOn = DateTime.Now,
            DueDate = dueDate
        };
        return true;
    }

    public bool ReturnBook()
    {
        if (IsAvailable) return false;
        IsAvailable = true;
        CurrentBorrow = null;
        return true;
    }
}
