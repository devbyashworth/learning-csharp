
/*
* Inventory Management System**

**Scenario:**
A store needs to keep track of its products — add, sell, restock, and view inventory.

**Requirements:**

* **Class:** `Product`

  * Properties: `Name`, `Price`, `Stock`
  * Methods: `Sell(int quantity)`, `Restock(int quantity)`

* **Main Program:**

  * Menu:

    ```
    1. Add Product
    2. Sell Product
    3. Restock Product
    4. View Inventory
    5. Exit
    ```

**Concepts covered:** error handling (don’t sell more than stock), loops, lists, encapsulation.

**Extra Challenge:**

* Track sales history (List of transactions).
* Apply discounts on bulk sales.
* Save/load inventory from a file.

*/

using System.Text;

Console.WriteLine("=== Inventory Management System ===");

List<Product> products = new List<Product>();

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
            AddProduct(products);
            break;
        case 2:
            SellProduct(products);
            break;
        case 3:
            RestockProduct(products);
            break;
        case 4:
            ViewProducts(products);
            break;
        case 5:
            Console.WriteLine("Exiting Payroll System... Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("\n1.Add Product");
    Console.WriteLine("2.Sell Product");
    Console.WriteLine("3.Restock Product");
    Console.WriteLine("4.View Inventory");
    Console.WriteLine("5.Exit\n");
}

static void AddProduct(List<Product> products)
{
    Console.WriteLine("Enter Product Name: ");
    string productName = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrEmpty(productName))
    {
        Console.WriteLine("Product name field/s can't be empty");
        return;
    }

    Console.WriteLine("Enter Quantity: ");
    if (!int.TryParse(Console.ReadLine()?.Trim() ?? "1", out int quantity))
    {
        Console.WriteLine("Invalid Quantity.");
        return;
    }

    Console.WriteLine("Enter Purchase Price: ");
    if (!decimal.TryParse(Console.ReadLine()?.Trim() ?? "", out decimal purchasePrice))
    {
        Console.WriteLine("Invalid Purchase Price.");
        return;
    }

    Console.WriteLine("Enter Sale Price: ");
    if (!decimal.TryParse(Console.ReadLine()?.Trim() ?? "", out decimal salePrice))
    {
        Console.WriteLine("Invalid Sale Price.");
        return;
    }

    products.Add(new Product(productName, quantity, purchasePrice, salePrice));
    Console.WriteLine($"{"Product Id",-15} {"Product Name",-50} {"Stock",-8} {"Purchase Price",-6} {"Sale Price",-6} {"Total Price",-6}");
    foreach (var product in products)
    {
        Console.WriteLine($"{product.ProductId,-15} {product.ProductName,-50} {product.Stock,-8} {product.PurchasePrice,-15} {product.SalePrice,-15} {product.CalculateTotalPrice(),-15}");
    }
}

// Sell product
static void SellProduct(List<Product> products)
{
    Console.Write("Enter Product ID to sell: ");
    string productId = Console.ReadLine()?.Trim() ?? "";

    var product = products.Find(p => p.ProductId == productId);

    if (product != null)
    {
        Console.Write($"Enter quantity to sell (Available: {product.Stock}): ");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            if (product.Sell(quantity))
            {
                Console.WriteLine($"{quantity} units of {product.ProductName} sold successfully.");
                Console.WriteLine($"Remaining Stock: {product.Stock}");
            }
            else
            {
                Console.WriteLine("❌ Not enough stock or invalid quantity.");
            }
        }
        else
        {
            Console.WriteLine("❌ Invalid input. Please enter a number.");
        }
    }
    else
    {
        Console.WriteLine("❌ Product not found.");
    }
}

// Restock product
static void RestockProduct(List<Product> products)
{
    Console.Write("Enter Product ID to restock: ");
    string productId = Console.ReadLine()?.Trim() ?? "";

    var product = products.Find(p => p.ProductId == productId);

    if (product != null)
    {
        Console.Write("Enter quantity to restock: ");
        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
        {
            product.Restock(quantity);
            Console.WriteLine($"{quantity} units added to {product.ProductName}.");
            Console.WriteLine($"New Stock: {product.Stock}");
        }
        else
        {
            Console.WriteLine("❌ Invalid input. Please enter a positive number.");
        }
    }
    else
    {
        Console.WriteLine("❌ Product not found.");
    }
}

// View all products in a table format with total inventory value & low stock alerts
static void ViewProducts(List<Product> products)
{
    if (products.Count == 0)
    {
        Console.WriteLine("\n⚠️ No products available.\n");
        return;
    }

    Console.WriteLine("\n📦 Inventory List:");
    Console.WriteLine("--------------------------------------------------------------------------------------------------");
    Console.WriteLine($"{"Product ID",-12} {"Name",-20} {"Stock",-8} {"Purchase",-12} {"Sale",-12} {"Value",-12} {"Alert",-10}");
    Console.WriteLine("--------------------------------------------------------------------------------------------------");

    decimal grandTotal = 0;

    foreach (var product in products)
    {
        decimal totalValue = product.Stock * product.PurchasePrice;
        grandTotal += totalValue;

        // Add alert message if stock < 5
        string alert = product.Stock < 5 ? "⚠️ Low" : "";

        Console.WriteLine($"{product.ProductId,-12} {product.ProductName,-20} {product.Stock,-8} {product.PurchasePrice,-12:C} {product.SalePrice,-12:C} {totalValue,-12:C} {alert,-10}");
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------------");
    Console.WriteLine($"{"",-66} {"TOTAL:",-12} {grandTotal,-12:C}");
    Console.WriteLine("--------------------------------------------------------------------------------------------------\n");
}



class Product
{
    public string ProductId { get; }
    public string ProductName { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public int Stock { get; private set; }  // keep track of stock

    private static readonly Random Rng = new Random();

    public Product(string productName, int quantity, decimal purchasePrice, decimal salePrice)
    {
        ProductId = GenerateProductCode();
        ProductName = productName;
        Stock = quantity;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
    }

    // Sell product (with error handling)
    public bool Sell(int quantity)
    {
        if (quantity <= 0) return false;
        if (quantity > Stock) return false;

        Stock -= quantity;
        return true;
    }

    // Restock product
    public void Restock(int quantity)
    {
        if (quantity > 0)
            Stock += quantity;
    }

    // Total value of stock on hand (purchase cost * stock)
    public decimal CalculateTotalPrice()
    {
        return PurchasePrice * Stock;
    }

    // Generate unique product code
    public static string GenerateProductCode()
    {
        string[] productCodePrefix = { "ABE", "WES", "COS", "COD", "GFH", "TRE" };
        string prefix = productCodePrefix[Rng.Next(productCodePrefix.Length)];
        return prefix + Rng.Next(100, 999); // prefix + 3 random digits
    }
}
