/*
File Encryption/Decryption Tool
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

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileCryptoTool
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== File Encryption/Decryption Tool ===\n");

            while (true)
            {
                Console.WriteLine("1) Encrypt a file");
                Console.WriteLine("2) Decrypt a file");
                Console.WriteLine("0) Exit");
                Console.Write("Choose an option: ");
                var mainChoice = Console.ReadLine()?.Trim();

                if (mainChoice == "0") return;

                if (mainChoice != "1" && mainChoice != "2")
                {
                    Console.WriteLine("Invalid option.\n");
                    continue;
                }

                bool isEncrypt = mainChoice == "1";

                Console.Write("\nEnter path to a .txt file: ");
                var path = (Console.ReadLine() ?? "").Trim().Trim('"');

                if (!File.Exists(path) || !path.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("File not found or not a .txt file.\n");
                    continue;
                }

                Console.WriteLine("\nChoose method:");
                Console.WriteLine("1) Caesar cipher (letters A-Z/a-z rotated)");
                Console.WriteLine("2) XOR cipher (byte-wise with a key)");
                Console.Write("Method: ");
                var method = Console.ReadLine()?.Trim();

                try
                {
                    switch (method)
                    {
                        case "1":
                            RunCaesar(path, isEncrypt);
                            break;
                        case "2":
                            RunXor(path, isEncrypt);
                            break;
                        default:
                            Console.WriteLine("Invalid method.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Operation failed: {ex.Message}\n");
                }
            }
        }

        // ========= Caesar =========
        static void RunCaesar(string path, bool isEncrypt)
        {
            Console.Write("Enter shift (e.g., 3): ");
            if (!int.TryParse(Console.ReadLine(), out int shift))
            {
                Console.WriteLine("Invalid shift.\n");
                return;
            }

            if (!isEncrypt) shift = -shift; // reverse direction for decryption

            string input = File.ReadAllText(path, Encoding.UTF8);
            string output = CaesarTransform(input, shift);

            string outPath = MakeOutputPath(path, isEncrypt ? "_enc_caesar" : "_dec_caesar");
            File.WriteAllText(outPath, output, Encoding.UTF8);

            Console.WriteLine($"Done. Output: {outPath}\n");
        }

        static string CaesarTransform(string text, int shift)
        {
            // Normalize shift to 0..25
            shift = ((shift % 26) + 26) % 26;

            char ShiftChar(char c, int s)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    int pos = c - 'A';
                    return (char)('A' + (pos + s) % 26);
                }
                if (c >= 'a' && c <= 'z')
                {
                    int pos = c - 'a';
                    return (char)('a' + (pos + s) % 26);
                }
                return c; // leave non-letters unchanged
            }

            var buffer = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
                buffer[i] = ShiftChar(text[i], shift);

            return new string(buffer);
        }

        // ========= XOR =========
        static void RunXor(string path, bool isEncrypt)
        {
            Console.Write("Enter key (any non-empty string): ");
            string key = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(key))
            {
                Console.WriteLine("Key cannot be empty.\n");
                return;
            }

            // XOR is symmetric (same operation for encrypt/decrypt)
            byte[] inputBytes = File.ReadAllBytes(path);
            byte[] outputBytes = XorTransform(inputBytes, Encoding.UTF8.GetBytes(key));

            string outPath = MakeOutputPath(path, isEncrypt ? "_enc_xor" : "_dec_xor");
            File.WriteAllBytes(outPath, outputBytes);

            Console.WriteLine($"Done. Output: {outPath}\n");
        }

        static byte[] XorTransform(byte[] data, byte[] key)
        {
            var result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            return result;
        }

        // ========= Helpers =========
        static string MakeOutputPath(string inputPath, string suffix)
        {
            string dir = Path.GetDirectoryName(inputPath) ?? Environment.CurrentDirectory;
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string ext = Path.GetExtension(inputPath); // ".txt"
            return Path.Combine(dir, $"{name}{suffix}{ext}");
        }
    }
}
