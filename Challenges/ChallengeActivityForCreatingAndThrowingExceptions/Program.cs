/*
* In this challenge, you start with a sample application that uses a series of method calls to process data. 
* The top-level statements create an array of user input values and call a method named Workflow1. 
* Workflow1 represents a high-level workflow that loops through the array and passes user input values to a method named Process1. Process1 uses the user input data to calculate a value.

*Currently, when Process1 encounters an issue or error, it returns a string describing the issue rather than throwing an exception. Your challenge is to implement exception handling in the sample application.

* All methods must be converted from static string methods to static void methods.
* The Process1 method must throw exceptions for each type of issue encountered.
* The Workflow1 method must catch and handle the FormatException exceptions.
* The top-level statements must catch and handle the DivideByZeroException exceptions.
* The Message property of the exception must be used to notify the user of the issue.
*/

Console.WriteLine("Challenge activity for creating and throwing exceptions");

string[][] userEnteredValues = new string[][]
{
    new string[] { "1", "2", "3" },
    new string[] { "1", "two", "3" },
    new string[] { "0", "1", "2" }
};

bool success = false;

try
{
    Workflow1(userEnteredValues);
    success = true;
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("A fatal error occurred in 'Workflow1':");
    Console.WriteLine(ex.Message);
}

if (success)
{
    Console.WriteLine("'Workflow1' completed successfully.");
}
else
{
    Console.WriteLine("An error occurred during 'Workflow1'.");
}

static void Workflow1(string[][] userEnteredValues)
{
    foreach (string[] userEntries in userEnteredValues)
    {
        try
        {
            Process1(userEntries);
            Console.WriteLine("'Process1' completed successfully.\n");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("'Process1' encountered a formatting issue.");
            Console.WriteLine(ex.Message + "\n");
        }
    }
}

static void Process1(string[] userEntries)
{
    foreach (string userValue in userEntries)
    {
        if (!int.TryParse(userValue, out int valueEntered))
        {
            throw new FormatException("Invalid data. User input values must be valid integers.");
        }

        if (valueEntered == 0)
        {
            throw new DivideByZeroException("Invalid data. User input values must be non-zero values.");
        }

        // Perform calculation safely
        checked
        {
            int calculatedValue = 4 / valueEntered;
        }
    }
}

/*
string[][] userEnteredValues = new string[][]
{
            new string[] { "1", "2", "3"},
            new string[] { "1", "two", "3"},
            new string[] { "0", "1", "2"}
};

try
{
    Workflow1(userEnteredValues);
    Console.WriteLine("'Workflow1' completed successfully.");

}
catch (DivideByZeroException ex)
{
    Console.WriteLine("An error occurred during 'Workflow1'.");
    Console.WriteLine(ex.Message);
}

static void Workflow1(string[][] userEnteredValues)
{
    foreach (string[] userEntries in userEnteredValues)
    {
        try
        {
            Process1(userEntries);
            Console.WriteLine("'Process1' completed successfully.");
            Console.WriteLine();
        }
        catch (FormatException ex)
        {
            Console.WriteLine("'Process1' encountered an issue, process aborted.");
            Console.WriteLine(ex.Message);
            Console.WriteLine();
        }
    }
}

static void Process1(String[] userEntries)
{
    int valueEntered;

    foreach (string userValue in userEntries)
    {
        bool integerFormat = int.TryParse(userValue, out valueEntered);

        if (integerFormat == true)
        {
            if (valueEntered != 0)
            {
                checked
                {
                    int calculatedValue = 4 / valueEntered;
                }
            }
            else
            {
                throw new DivideByZeroException("Invalid data. User input values must be non-zero values.");
            }
        }
        else
        {
            throw new FormatException("Invalid data. User input values must be valid integers.");
        }
    }
}
*/
