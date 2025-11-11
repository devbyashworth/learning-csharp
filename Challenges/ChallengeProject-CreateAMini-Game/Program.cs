// ** Project specification

/*
- The code declares the following variables:
    -Variables to determine the size of the Terminal window.
    - Variables to track the locations of the player and food.
    - Arrays `states` and `foods` to provide available player and food appearances
    - Variables to track the current player and food appearance

- The code provides the following methods:
    -A method to determine if the Terminal window was resized.
    - A method to display a random food appearance at a random location.
    - A method that changes the player appearance to match the food consumed.
    - A method that temporarily freezes the player movement.
    - A method that moves the player according to directional input.
    - A method that sets up the initial game state.

- The code doesn't call the methods correctly to make the game playable. The following features are missing:
    - Code to determine if the player has consumed the food displayed.
    - Code to determine if the food consumed should freeze player movement.
    - Code to determine if the food consumed should increase player movement.
    - Code to increase movement speed.
    - Code to redisplay the food after it's consumed by the player.
    - Code to terminate execution if an unsupported key is entered.
    - Code to terminate execution if the terminal was resized.
*/

Console.WriteLine("Challenge project - Create a mini-game");

int windowWidth = Console.WindowWidth;
int windowHeight = Console.WindowHeight;

int playerX = windowWidth / 2;
int playerY = windowHeight / 2;

int foodX = 0;
int foodY = 0;

string[] states = { "@", "#", "&", "X" };
string[] foods = { "*", "$", "+", "%" };

int currentStateIndex = 0;
int currentFoodIndex = 0;

int moveDelay = 200; // in milliseconds
bool frozen = false;

bool WindowResized()
{
    return Console.WindowWidth != windowWidth || Console.WindowHeight != windowHeight;
}

void SpawnFood()
{
    Random rand = new Random();
    foodX = rand.Next(0, windowWidth);
    foodY = rand.Next(0, windowHeight);
    currentFoodIndex = rand.Next(foods.Length);
}

void MatchFood()
{
    currentStateIndex = currentFoodIndex;
}

void FreezePlayer()
{
    frozen = true;
    Task.Delay(1000).ContinueWith(_ => frozen = false);
}

void MovePlayer(ConsoleKey key)
{
    switch (key)
    {
        case ConsoleKey.UpArrow: if (playerY > 0) playerY--; break;
        case ConsoleKey.DownArrow: if (playerY < windowHeight - 1) playerY++; break;
        case ConsoleKey.LeftArrow: if (playerX > 0) playerX--; break;
        case ConsoleKey.RightArrow: if (playerX < windowWidth - 1) playerX++; break;
    }
}

void InitializeGame()
{
    Console.Clear();
    Console.CursorVisible = false;
    SpawnFood();
}

InitializeGame();

while (true)
{
    if (WindowResized())
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Window resized. Game terminated.");
        break;
    }

    Console.SetCursorPosition(playerX, playerY);
    Console.Write(states[currentStateIndex]);

    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[currentFoodIndex]);

    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true).Key;

        if (key != ConsoleKey.UpArrow && key != ConsoleKey.DownArrow &&
            key != ConsoleKey.LeftArrow && key != ConsoleKey.RightArrow)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Unsupported key. Game terminated.");
            break;
        }

        if (!frozen)
        {
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(" "); // Clear old player position

            MovePlayer(key);
        }
    }

    // Check collision
    if (playerX == foodX && playerY == foodY)
    {
        MatchFood();

        // Behavior: Freeze if special food consumed
        if (foods[currentFoodIndex] == "$")
        {
            FreezePlayer();
        }

        // Behavior: Increase speed
        if (foods[currentFoodIndex] == "+")
        {
            moveDelay = Math.Max(50, moveDelay - 20);
        }

        SpawnFood();
    }

    Thread.Sleep(moveDelay);
}
