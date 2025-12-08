string message = "...Center Text in Termnal...";

// Get console dimensions
int windowWidth = Console.WindowWidth;
int windowHeight = Console.WindowHeight;

// Calculate horizontal and vertical positions
int leftPadding = (windowWidth - message.Length) / 2;
int topPadding = windowHeight / 2;

// Move cursor to vertical center
Console.SetCursorPosition(0, topPadding);

// Write spaces to center horizontally, then the message
Console.WriteLine(new string(' ', leftPadding) + message);

Console.ReadKey();