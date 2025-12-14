/*
* File Encryption/Decryption Tool
Goal: Read a text file, encrypt its content using a simple cipher, save it.

Core Concepts: File I/O, string manipulation, basic algorithms

How to Approach:

Ask the user to choose a .txt file path.

Use a Caesar Cipher or similar to encode text.

Write the encrypted text to another file.

Provide option to decrypt too.

Go Further:

Implement stronger encryption (e.g., XOR cipher).

Create a basic GUI using WinForms/WPF.
*/


Console.WriteLine("=== File Encryption/Decryption Tool ===\n");

while (true)
{
    Console.WriteLine("1) Encrypt a file");
    Console.WriteLine("2) Decrypt a file");
    Console.WriteLine("0) Exit");
    Console.Write("Choose an option: ");
    string choice = Console.ReadLine()?.Trim() ?? "";

    if (choice == "0") break;
    if (choice != "1" && choice != "2")
    {
        Console.WriteLine("Invalid option.\n");
        continue;
    }

    bool encrypt = choice == "1";
    string path = Prompt("Enter path to a .txt file:").Trim('"');

    if (!File.Exists(path) || !path.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("File not found or not a .txt file.\n");
        continue;
    }

    Console.WriteLine("\nChoose method:");
    Console.WriteLine("1) Caesar cipher");
    Console.WriteLine("2) XOR cipher");
    string method = Prompt("Method:");

    try
    {
        if (method == "1") RunCaesar(path, encrypt);
        else if (method == "2") RunXor(path, encrypt);
        else Console.WriteLine("Invalid method.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n");
    }
}

static void RunCaesar(string path, bool encrypt)
{
    if (!int.TryParse(Prompt("Enter shift (e.g., 3):"), out int shift))
    {
        Console.WriteLine("Invalid shift.\n");
        return;
    }

    if (!encrypt) shift = -shift;
    string input = File.ReadAllText(path);
    string output = new string(input.Select(c => ShiftChar(c, shift)).ToArray());

    string outPath = MakeOutputPath(path, encrypt ? "_enc_caesar" : "_dec_caesar");
    File.WriteAllText(outPath, output);
    Console.WriteLine($"Done. Output: {outPath}\n");
}

static char ShiftChar(char c, int shift)
{
    shift = ((shift % 26) + 26) % 26;
    if (char.IsUpper(c)) return (char)('A' + (c - 'A' + shift) % 26);
    if (char.IsLower(c)) return (char)('a' + (c - 'a' + shift) % 26);
    return c;
}

static void RunXor(string path, bool encrypt)
{
    string key = Prompt("Enter key:");
    if (string.IsNullOrEmpty(key))
    {
        Console.WriteLine("Key cannot be empty.\n");
        return;
    }

    byte[] input = File.ReadAllBytes(path);
    byte[] output = input.Select((b, i) => (byte)(b ^ key[i % key.Length])).ToArray();

    string outPath = MakeOutputPath(path, encrypt ? "_enc_xor" : "_dec_xor");
    File.WriteAllBytes(outPath, output);
    Console.WriteLine($"Done. Output: {outPath}\n");
}

static string Prompt(string message)
{
    Console.Write(message + " ");
    return Console.ReadLine() ?? "";
}

static string MakeOutputPath(string inputPath, string suffix)
{
    string dir = Path.GetDirectoryName(inputPath) ?? ".";
    string name = Path.GetFileNameWithoutExtension(inputPath);
    string ext = Path.GetExtension(inputPath);
    return Path.Combine(dir, $"{name}{suffix}{ext}");
}
