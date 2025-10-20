// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Book book = new Book("LEO", "Conn Iggilden");
Book book = new("LEO", "Conn Iggilden");
Console.WriteLine("The book name is: " + book.name + " by " + book.author);


class Book(string name, string author)
{
    public string name = name;
    public string author = author;
}