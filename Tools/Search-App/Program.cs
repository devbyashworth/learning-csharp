using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string path = @"C:\"; // safer with verbatim string
        List<string> results = new List<string>();

        Console.Write("What Are You Looking For? ");
        string term = Console.ReadLine() ?? "";

        Search(path, term, results);

        Console.WriteLine("\nResults:");
        foreach (var file in results)
        {
            Console.WriteLine(file);
        }
    }

    static void Search(string path, string term, List<string> results)
    {
        try
        {
            // Search directories
            string[] dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs)
            {
                Search(dir, term, results);
            }

            // Search files
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.Contains(term, StringComparison.CurrentCultureIgnoreCase))
                {
                    results.Add(file);
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip directories you don't have access to
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error accessing {path}: {ex.Message}");
        }
    }
}
