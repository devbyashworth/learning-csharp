/*
* 🛒 Shopping Cart Console App — Project Outline

### 🎯 **Project Goal**
Create a console-based shopping cart system where users can browse products, add/remove items from their cart, view totals, apply discounts, and checkout. Data should be saved and loaded from a file.

---

### 🧩 **Core Components**

#### 1. **Class: `Product`**
Represents an item available for purchase.

- **Properties:**
  - `Id` (int)
  - `Name` (string)
  - `Price` (decimal)
  - `StockQuantity` (int)

#### 2. **Class: `CartItem`**
Represents a product added to the cart.

- **Properties:**
  - `Product` (Product)
  - `Quantity` (int)

#### 3. **Class: `ShoppingCart`**
Manages the user's cart.

- **Properties:**
  - `Items` (List<CartItem>)

- **Methods:**
  - `AddItem(Product product, int quantity)`
  - `RemoveItem(int productId)`
  - `GetTotal()`
  - `ApplyDiscount(string code)`
  - `Checkout()`

#### 4. **Class: `Store`**
Manages product inventory and handles user interaction.

- **Properties:**
  - `Products` (List<Product>)
  - `Cart` (ShoppingCart)

- **Methods:**
  - `DisplayProducts()`
  - `FindProductById(int id)`
  - `LoadProductsFromFile()`
  - `SaveProductsToFile()`

---

### 📋 **Main Menu Options**

```text
1. View Products
2. Add Product to Cart
3. Remove Product from Cart
4. View Cart
5. Apply Discount Code
6. Checkout
7. Save Cart
8. Load Cart
9. Exit
```

---

### 💾 **File Persistence**

Use `System.Text.Json` to save/load:
- Product inventory (`products.json`)
- Cart contents (`cart.json`)

---

### 🧠 **Extra Features (Optional)**
- Discount codes (e.g., "SAVE10" = 10% off)
- Stock validation (don’t allow adding more than available)
- Receipt generation on checkout
- Multiple users with login
- Product categories and filtering

*/

using System.Text.Json;
using ShoppingCartApp;

Store store = new Store();
store.LoadProducts(); // auto-load if available

store.AddProduct("Wireless Mouse", 299.99m, 25);
store.AddProduct("Mechanical Keyboard", 899.50m, 15);
store.AddProduct("USB-C Hub", 499.00m, 30);
store.AddProduct("27\" Monitor", 3299.00m, 10);
store.AddProduct("Noise-Cancelling Headphones", 1499.00m, 20);

bool running = true;

while (running)
{
    Console.WriteLine("\n--- Shopping Cart Menu ---");
    Console.WriteLine("1. View Products");
    Console.WriteLine("2. Add Product");
    Console.WriteLine("3. Add to Cart");
    Console.WriteLine("4. Remove from Cart");
    Console.WriteLine("5. View Cart");
    Console.WriteLine("6. Apply Discount Code");
    Console.WriteLine("7. Checkout");
    Console.WriteLine("8. Save Products");
    Console.WriteLine("9. Load Products");
    Console.WriteLine("10. Save Cart");
    Console.WriteLine("11. Load Cart");
    Console.WriteLine("0. Exit\n");

    Console.Write("Choose an option: \n");
    string choice = Console.ReadLine()?.Trim() ?? "";

    switch (choice)
    {
        case "1":
            store.ViewProducts();
            break;
        case "2":
            Console.Write("Enter product name: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter price: "); //  if (!ReadDecimal("Enter price: ", out var price)) break;
            if (!decimal.TryParse(Console.ReadLine()?.Trim(), out decimal price))
            {
                Console.WriteLine("Invalid price entered. Please enter a valid decimal number.");
                return;
            }
            Console.Write("Enter stock: "); //  if (!ReadInt("Enter stock: ", out var stock)) break;
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int stock))
            {
                Console.WriteLine("Invalid price entered. Please enter a valid number.");
                return;
            }
            store.AddProduct(name, price, stock);
            break;
        case "3":
            Console.Write("Enter product ID: ");
            string addId = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int qty))
            {
                Console.WriteLine("Invalid price entered. Please enter a valid number.");
                return;
            }
            store.AddToCart(addId, qty);
            break;
        case "4":
            Console.Write("Enter product ID: ");
            string removeId = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int removeQty))
            {
                Console.WriteLine("Invalid quantity entered. Please enter a valid number.");
                return;
            }
            store.RemoveFromCart(removeId, removeQty);
            break;
        case "5":
            store.ViewCart();
            break;
        case "6":
            Console.Write("Enter discount code: ");
            string code = Console.ReadLine()?.Trim() ?? "";
            store.ApplyDiscount(code);
            break;
        case "7":
            store.Checkout();
            break;
        case "8":
            store.SaveProducts();
            break;
        case "9":
            store.LoadProducts();
            break;
        case "10":
            store.SaveCart();
            break;
        case "11":
            store.LoadCart();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

// static bool ReadInt(string prompt, out int value)
// {
//     Console.Write(prompt);
//     var s = Console.ReadLine();
//     if (int.TryParse(s, out value) && value >= 0) return true;
//     Console.WriteLine("Invalid number.");
//     return false;
// }

// static bool ReadDecimal(string prompt, out decimal value)
// {
//     Console.Write(prompt);
//     var s = Console.ReadLine();
//     if (decimal.TryParse(s, out value) && value >= 0) return true;
//     Console.WriteLine("Invalid amount.");
//     return false;
// }

namespace ShoppingCartApp
{
    public class Product
    {
        // public string Id { get; set; }
        // public string Name { get; set; }
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class CartItem
    {
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public decimal GetTotal() => Product.Price * Quantity;
    }

    public class Store
    {
        private List<Product> products = new List<Product>();
        private List<CartItem> cart = new List<CartItem>();
        private decimal discountPercent = 0;
        private static readonly Random rand = new Random();


        // === Product Management ===
        public void ViewProducts()
        {
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name} - {product.Price:C} (Stock: {product.StockQuantity})");
            }
        }

        public static string GenerateRandomCode()
        {
            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code;
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[rand.Next(chars.Length)];
            }
            code = new string(result);
            return code;
        }

        public void AddProduct(string name, decimal price, int stock)
        {
            products.Add(new Product { Id = GenerateRandomCode(), Name = name, Price = price, StockQuantity = stock });
            Console.WriteLine($"{name} added successfully.");
        }

        // === Cart Management ===
        public void AddToCart(string productId, int quantity)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            if (quantity > product.StockQuantity)
            {
                Console.WriteLine($"Not enough stock. Available: {product.StockQuantity}");
                return;
            }

            var item = cart.FirstOrDefault(c => c.Product.Id == productId);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = quantity });
            }

            Console.WriteLine($"{quantity} x {product.Name} added to cart.");
        }

        public void RemoveFromCart(string productId, int quantity)
        {
            var item = cart.FirstOrDefault(c => c.Product.Id == productId);
            if (item == null)
            {
                Console.WriteLine("Item not in cart.");
                return;
            }

            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                cart.Remove(item);

            Console.WriteLine($"{item.Product.Name} updated. Remaining in cart: {item.Quantity}");
        }

        public void ViewCart()
        {
            if (cart.Count == 0) // !cart.Any()
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("Shopping Cart:");
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = {item.GetTotal():C}");
            }
            Console.WriteLine($"Subtotal: {cart.Sum(c => c.GetTotal()):C}");
            Console.WriteLine($"Discount: {discountPercent:P0}");
            Console.WriteLine($"Total (with discount): {GetTotal():C}");
        }

        // === Discounts ===
        public void ApplyDiscount(string code)
        {
            code = code.Trim().ToUpperInvariant();
            if (code == "SAVE10") discountPercent = 0.10m;
            else if (code == "SAVE20") discountPercent = 0.20m;
            else
            {
                Console.WriteLine("Invalid discount code.");
                return;
            }
            Console.WriteLine($"Discount applied: {discountPercent:P0}");
        }

        private decimal GetTotal()
        {
            decimal subtotal = cart.Sum(c => c.GetTotal());
            decimal discount = subtotal * discountPercent;
            decimal total = subtotal - discount;
            return total - (subtotal * discountPercent);
        }

        // === Checkout ===
        public void Checkout()
        {
            if (cart.Count == 0) // !cart.Any()
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("=== Receipt ===");
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = {item.GetTotal():C}");
                item.Product.StockQuantity -= item.Quantity; // reduce stock
            }
            Console.WriteLine($"TOTAL: {GetTotal():C}");
            cart.Clear();
        }

        // === File Persistence ===
        public void SaveProducts(string path = "products.json")
        {
            File.WriteAllText(path, JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Products saved to file.");
        }

        public void LoadProducts(string path = "products.json")
        {
            if (File.Exists(path))
            {
                products = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(path)) ?? new List<Product>();
                Console.WriteLine("Products loaded from file.");
            }
        }

        public void SaveCart(string path = "cart.json")
        {
            File.WriteAllText(path, JsonSerializer.Serialize(cart, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Cart saved to file.");
        }

        public void LoadCart(string path = "cart.json")
        {
            if (File.Exists(path))
            {
                cart = JsonSerializer.Deserialize<List<CartItem>>(File.ReadAllText(path)) ?? new List<CartItem>();
                Console.WriteLine("Cart loaded from file.");
            }
        }
    }
}
