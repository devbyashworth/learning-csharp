// Code challenge - write code to implement the game rules
// Here are the rules for the battle game that you need to implement in your code project:

// - You must use either the do-while statement or the while statement as an outer game loop.
// - The hero and the monster start with 10 health points.
// - All attacks are a value between 1 and 10.
// - The hero attacks first.
// - Print the amount of health the monster lost and their remaining health.
// - If the monster's health is greater than 0, it can attack the hero.
// - Print the amount of health the hero lost and their remaining health.
// - Continue this sequence of attacking until either the monster's health or hero's health is zero or less.
// - Print the winner.

Console.WriteLine("Role Playing Game Battle Challenge");

Random random = new Random();

int heroHealth = 10;
int monsterHealth = 10;

do
{
    // Hero attacks first
    int heroAttack = random.Next(1, 11);
    monsterHealth -= heroAttack;
    Console.WriteLine($"Monster was damaged and lost {heroAttack} health and now has {monsterHealth} health.");

    if (monsterHealth <= 0)
    {
        Console.WriteLine("Hero wins!");
        break;
    }

    // Monster attacks if still alive
    int monsterAttack = random.Next(1, 11);
    heroHealth -= monsterAttack;
    Console.WriteLine($"Hero was damaged and lost {monsterAttack} health and now has {heroHealth} health.");

    if (heroHealth <= 0)
    {
        Console.WriteLine("Monster wins!");
        break;
    }

} while (true);



// int hero = 10;
// int monster = 10;

// Random dice = new Random();

// do
// {
//     int roll = dice.Next(1, 11);
//     monster -= roll;
//     Console.WriteLine($"Monster was damaged and lost {roll} health and now has {monster} health.");

//     if (monster <= 0) continue;

//     roll = dice.Next(1, 11);
//     hero -= roll;
//     Console.WriteLine($"Hero was damaged and lost {roll} health and now has {hero} health.");

// } while (hero > 0 && monster > 0);

// Console.WriteLine(hero > monster ? "Hero wins!" : "Monster wins!");