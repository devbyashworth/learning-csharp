/*
* Project: Student Grade Manager

Scenario:
A console program where a teacher can add students, assign grades, view all students, and calculate averages.

Requirements:

Class: Student

Properties: Name, Grades (List<int>)

Method: AddGrade(int grade)

Method: GetAverageGrade() → returns double

Main Program

Store students in a List<Student>.

Menu (switch statement):

1. Add Student
2. Add Grade to Student
3. View All Students
4. Exit


Use loops to keep menu running until Exit.

Error Handling for invalid menu options, grade inputs, or student not found.

Approach:

First, create the Student class with encapsulated fields and public methods.

In Main, create a menu loop with switch.

Use List<Student> to store data dynamically.

Use try...catch for number conversions and invalid inputs.

Extra Challenge:

Allow deleting a student.

Show top-performing student.

Save/load data to a text file.
*/

Console.WriteLine("=== Student Grade Manager ===");

List<Student> students = new List<Student>();

while (true)
{
    ShowMenu();

    if (!int.TryParse(Console.ReadLine(), out var choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            AddStudent();
            break;
        case 2:
            AddStudentGrades();
            break;
        case 3:
            ViewStudents();
            break;
        case 4:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\n1.Add Student");
    Console.WriteLine("2.Add Grade to Student");
    Console.WriteLine("3.View All Students");
    Console.WriteLine("4.Exit\n");
}

void AddStudent()
{
    Console.Write("Enter First Name: ");
    string firstName = Console.ReadLine()?.Trim() ?? "";
    Console.Write("Enter Last Name: ");
    string lastName = Console.ReadLine()?.Trim() ?? "";
    Console.Write("Enter Gender: ");
    string gender = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(gender))
    {
        Console.WriteLine("First Name, Last Name or Gender can't be empty");
        return;
    }

    students.Add(new Student(firstName, lastName, gender));
    Console.WriteLine($"Student {firstName} {lastName} successfully added.");

}
;
void AddStudentGrades()
{
    if (students.Count == 0)
    {
        Console.WriteLine("No Students Found.");
        return;
    }

    Console.Write("Enter Student First or Last Name: ");
    string query = Console.ReadLine()?.Trim() ?? "";

    var student = students.FirstOrDefault(s =>
        s.FirstName.Equals(query, StringComparison.OrdinalIgnoreCase) ||
        s.LastName.Equals(query, StringComparison.OrdinalIgnoreCase));

    if (student == null)
    {
        Console.WriteLine($"No student found with name '{query}'.");
        return;
    }

    Console.WriteLine("Enter grades for subjects (0 - 100):");

    student.AddGrade(ReadGrade("Computer Science"));
    student.AddGrade(ReadGrade("Business"));
    student.AddGrade(ReadGrade("Engineering"));
    student.AddGrade(ReadGrade("Medicine"));
    student.AddGrade(ReadGrade("Law"));

    Console.WriteLine($"Grades updated for {student.FirstName} {student.LastName}: {string.Join(", ", student.GetGrades())}");
    Console.WriteLine($"Average: {student.GetAverageGrade():F2}");
}

int ReadGrade(string subject)
{
    while (true)
    {
        Console.Write($"{subject}: ");
        if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 0 && grade <= 100)
        {
            return grade;
        }
        Console.WriteLine("Invalid grade. Enter a number between 0 and 100.");
    }
}

void ViewStudents()
{
    if (students.Count == 0)
    {
        Console.WriteLine("No Students Found.");
        return;
    }

    Console.WriteLine("All Students: ");
    foreach (var s in students)
    {
        Console.WriteLine($"{s.FirstName} {s.LastName} ({s.Gender}) - Avg: {s.GetAverageGrade():F2}");
    }
}


class Student
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Gender { get; }
    private List<int> Grades { get; } = new List<int>();

    public Student(string firstName, string lastName, string gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
    }

    public void AddGrade(int grade) => Grades.Add(grade);

    public double GetAverageGrade() => Grades.Count > 0 ? Grades.Average() : 0;

    public List<int> GetGrades() => new List<int>(Grades); // Return a copy for safety
}