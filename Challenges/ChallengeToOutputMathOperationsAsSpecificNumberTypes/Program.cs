// Enter the following "starter" code:
// int value1 = 11;
// decimal value2 = 6.2m;
// float value3 = 4.3f;

// Your code here to set result1
// Hint: You need to round the result to nearest integer (don't just truncate)
// Console.WriteLine($"Divide value1 by value2, display the result as an int: {result1}");

// Your code here to set result2
// Console.WriteLine($"Divide value2 by value3, display the result as a decimal: {result2}");

// Your code here to set result3
// Console.WriteLine($"Divide value3 by value1, display the result as a float: {result3}");
// Replace the code comments in the starter code with your own code to solve the challenge:

// Solve for result1: Divide value1 by value2, display the result as an int
// Solve for result2: Divide value2 by value3, display the result as a decimal
// Solve for result3: Divide value3 by value1, display the result as a float
// Solve the challenge so that your output resembles:

// Output
// Divide value1 by value2, display the result as an int: 2
// Divide value2 by value3, display the result as a decimal: 1.4418604651162790697674418605
// Divide value3 by value1, display the result as a float: 0.3909091

Console.WriteLine("Challenge to output math operations as specific number types");

int value1 = 11;
decimal value2 = 6.2m;
float value3 = 4.3f;

// Your code here to set result1
// Hint: You need to round the result to nearest integer (don't just truncate)
int result1 = Convert.ToInt32(value1 / value2);
Console.WriteLine($"Divide value1 by value2, display the result as an int: {result1}");

// Your code here to set result2
decimal result2 = value2 / (decimal)value3;
Console.WriteLine($"Divide value2 by value3, display the result as a decimal: {result2}");

// Your code here to set result3
float result3 = value3 / value1;
Console.WriteLine($"Divide value3 by value1, display the result as a float: {result3}");
