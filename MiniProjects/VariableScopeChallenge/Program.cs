Console.Clear();
Console.WriteLine("=== Variable Scope Challenge ===");

int[] numbers = [4, 8, 15, 16, 23, 42];

int total = 0; // Declare outside the loop

foreach (int number in numbers)
{
    total += number; // Add each number to total

    if (number == 42)
    {
        bool found = true;

        if (found)
        {
            Console.WriteLine("Set contains 42");
        }
    }

}

Console.WriteLine($"Total: {total}");

// int[] numbers = { 4, 8, 15, 16, 23, 42 };
// int total = 0;
// bool found = false;

// foreach (int number in numbers)
// {
//     total += number;
//     if (number == 42)
//         found = true;
// }

// if (found)
//     Console.WriteLine("Set contains 42");

// Console.WriteLine($"Total: {total}");