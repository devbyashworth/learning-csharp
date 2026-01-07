Console.WriteLine("******************************************************************************");
Console.WriteLine("****************************** Pyramid Drawing App! **************************");
Console.WriteLine("******************************************************************************");

Console.WriteLine("How High Do You Want Your Pyramid To Be? ");
int size = Convert.ToInt32(Console.ReadLine());

DrawPyramid(size);


void DrawPyramid(int size)
{

    for (int y = 1; y <= size; y++)
    {

        for (int h = 0; h < size - y; h++)
        {
            Console.Write("-");
        }

        for (int p = 0; p < ((y * 2) - 1); p++)
        {
            Console.Write("#");
        }

        for (int h = 0; h < size - y; h++)
        {
            Console.Write("-");
        }

        Console.WriteLine();
    }

}

/*
void DrawPyramid(int size)
{
    for (int row = 1; row <= size; row++)
    {
        // Left padding
        for (int space = 0; space < size - row; space++)
        {
            Console.Write(" ");
        }

        // Pyramid blocks
        for (int hash = 0; hash < (row * 2) - 1; hash++)
        {
            Console.Write("#");
        }

        Console.WriteLine();
    }
}
*/