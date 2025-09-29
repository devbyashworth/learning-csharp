// using System;

// namespace HelloWorl// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Hello, World!");
//         }
//     }
// }

using HelloWorld;

// Print a header
Console.WriteLine("=== HelloWorldApp ===");
Console.WriteLine("=== Commit-Line Args ===");

// Check command-line args
if (args.Length > 0)
{
    Console.WriteLine($"Hello, {args[0]}!");
}
else
{
    Console.WriteLine("Hello, World!");
}

// Call a method from another class
Helper.SayGoodbye();
