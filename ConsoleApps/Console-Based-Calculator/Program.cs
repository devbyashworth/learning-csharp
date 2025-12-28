/*
* Console - Based Calculator

Goal: Create a calculator that supports addition, subtraction, multiplication, and division.

Core Concepts: Console input/output, conditional logic, switch statements, basic arithmetic

How to Approach:

Prompt the user to input two numbers.

Ask which operation they want to perform (+, -, *, /).

Use a switch statement to decide what calculation to perform.

Display the result.

Go Further:

Add square root, exponentiation, or modulo operations.

Handle division by zero errors.
*/

Console.WriteLine("=== Console-Based Calculator ===");

while (true)
{
    double num1 = GetNumber("Enter your first number: ");
    string operand = GetOperand();
    double num2 = 0;

    // Only ask for second number if operation requires it
    if (operand != "sqrt")
    {
        num2 = GetNumber("Enter your second number: ");
    }

    try
    {
        double result = Calculate(num1, num2, operand);
        Console.WriteLine($"\nResult: {FormatOutput(num1, num2, operand, result)}\n");
    }
    catch (DivideByZeroException)
    {
        Console.WriteLine("\nError: Cannot divide by zero.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nError: {ex.Message}\n");
    }

    Console.Write("Do you want to calculate again? (y/n): ");
    if (Console.ReadLine()?.Trim().ToLower() != "y")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    Console.WriteLine();
}

// Get a valid number from the user
static double GetNumber(string prompt)
{
    double number;
    Console.Write(prompt);
    while (!double.TryParse(Console.ReadLine(), out number))
    {
        Console.Write("Invalid number, try again: ");
    }
    return number;
}

// Get a valid operand
static string GetOperand()
{
    Console.Write("Enter Operand (+, -, *, /, %, **, sqrt): ");
    string[] validOperands = { "+", "-", "*", "/", "%", "**", "sqrt" };
    string operand;
    while (true)
    {
        operand = Console.ReadLine()?.Trim() ?? "";
        if (Array.Exists(validOperands, op => op == operand))
            break;
        Console.Write("Invalid operand, try again: ");
    }
    return operand;
}

// Perform calculation
static double Calculate(double a, double b, string operand)
{
    switch (operand)
    {
        case "+":
            return a + b;
        case "-":
            return a - b;
        case "*":
            return a * b;
        case "/":
            if (b == 0)
                throw new DivideByZeroException();
            return a / b;
        case "%":
            if (b == 0)
                throw new DivideByZeroException();
            return a % b;
        case "**":
            return Math.Pow(a, b);
        case "sqrt":
            return Math.Sqrt(a);
        default:
            throw new InvalidOperationException("Unknown operation");
    }
}

// Format output for display
static string FormatOutput(double a, double b, string operand, double result)
{
    if (operand == "sqrt")
        return $"√{a} = {result}";
    return $"{a} {operand} {b} = {result}";
}

