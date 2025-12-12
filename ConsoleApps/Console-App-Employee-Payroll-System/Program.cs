/*
* Employee Payroll System

**Scenario:**
A company wants a console program to manage employees and calculate salaries.

**Requirements:**

* **Class:** `Employee`

  * Properties: `Name`, `HourlyRate`, `HoursWorked`
  * Methods: `CalculatePay()`

* **Main Program:**

  * Menu:

    ```
    1. Add Employee
    2. Record Hours Worked
    3. Calculate Salary for Employee
    4. View All Employees
    5. Exit
    ```

**Concepts covered:** encapsulation (private fields for hours), error handling for hours, switch, loops, lists.

**Extra Challenge:**

* Support different employee types (full-time, part-time → inheritance).
* Track monthly salary reports.
* Bonus system for overtime.

*/

Console.WriteLine("\n=== Employee Payroll System ===\n");

List<Employee> employees = new List<Employee>();

while (true)
{
    ShowMenu();

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            AddEmployee(employees);
            break;
        case 2:
            RecordHoursWorked(employees);
            break;
        case 3:
            CalculateSalary(employees);
            break;
        case 4:
            ViewEmployees(employees);
            break;
        case 5:
            Console.WriteLine("Exiting Payroll System... Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("\n1.Add Employee");
    Console.WriteLine("2.Record Hours Worked");
    Console.WriteLine("3.Calculate Salary for Employee");
    Console.WriteLine("4.View All Employees");
    Console.WriteLine("5.Exit\n");
}

static void AddEmployee(List<Employee> employees)
{
    Console.Write("Enter First Name: ");
    string firstName = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Enter Last Name: ");
    string lastName = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
    {
        Console.WriteLine("First Name and Last Name field/s can't be empty");
        return;
    }

    Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine()?.Trim(), out DateTime dob))
    {
        Console.WriteLine("Invalid date format.");
        return;
    }

    Console.Write("Enter Hourly Rate: ");
    if (!decimal.TryParse(Console.ReadLine()?.Trim() ?? "0", out decimal hourlyRate))
    {
        Console.WriteLine("Invalid hourly rate.");
        return;
    }

    Console.Write("Employment Type (1 - Full Time, 2 - Part Time, 3 - Contract): ");
    string typeChoice = Console.ReadLine()?.Trim() ?? "1";

    bool added = false;

    switch (typeChoice)
    {
        case "1":
            employees.Add(new FullTime(firstName, lastName, dob, hourlyRate));
            added = true;
            break;
        case "2":
            employees.Add(new PartTime(firstName, lastName, dob, hourlyRate));
            added = true;
            break;
        case "3":
            employees.Add(new Contract(firstName, lastName, dob, hourlyRate));
            added = true;
            break;
        default:
            Console.WriteLine("Invalid employment type.");
            break;
    }

    if (added)
    {
        Console.WriteLine("Employee Added Successfully.");
    }
    else
    {
        Console.WriteLine("Invalid employment type.");
    }
}

static void RecordHoursWorked(List<Employee> employees)
{
    if (employees.Count == 0)
    {
        Console.WriteLine("No Employees found.");
        return;
    }

    Console.Write("Enter Employee First or Last Name: ");
    string query = Console.ReadLine()?.Trim() ?? "";

    var employee = employees.FirstOrDefault(e =>
        e.FirstName.Equals(query, StringComparison.OrdinalIgnoreCase) ||
        e.LastName.Equals(query, StringComparison.OrdinalIgnoreCase));

    if (employee == null)
    {
        Console.WriteLine($"No employee found with name '{query}'.");
        return;
    }

    Console.WriteLine("Enter hours for the week:");
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    foreach (string day in days)
    {
        Console.Write($"Enter hours for {day} (or leave empty to skip): ");
        string input = Console.ReadLine()?.Trim() ?? "";
        if (decimal.TryParse(input, out decimal hours) && hours >= 0 && hours <= 24)
        {
            employee.AddHours(hours);
        }
        else
        {
            Console.WriteLine("Invalid input. Hours not recorded for this day.");
        }
    }

    Console.WriteLine("Hours Recorded Successfully.");
    // decimal totalHours = employee.GetHours().Sum();
    // Console.WriteLine($"Total Hours Worked: {totalHours}");
    Console.WriteLine($"Hours Updated for {employee.FirstName} {employee.LastName}: {string.Join(", ", employee.GetHours())}: Total Hours Worked: {employee.TotalHours}");

}

static void CalculateSalary(List<Employee> employees)
{
    if (employees.Count == 0)
    {
        Console.WriteLine("No Employees found.");
        return;
    }

    Console.Write("Enter Employee First or Last Name: ");
    string query = Console.ReadLine()?.Trim() ?? "";

    var employee = employees.FirstOrDefault(e =>
        e.FirstName.Equals(query, StringComparison.OrdinalIgnoreCase) ||
        e.LastName.Equals(query, StringComparison.OrdinalIgnoreCase));

    if (employee == null)
    {
        Console.WriteLine($"No employee found with name '{query}'.");
        return;
    }

    decimal pay = employee.CalculatePay();
    Console.WriteLine($"{employee.FirstName} {employee.LastName} earned {pay:C} this period (Total Hours: {employee.TotalHours}).");
}

static void ViewEmployees(List<Employee> employees)
{
    if (employees.Count == 0)
    {
        Console.WriteLine("No Employees found.");
        return;
    }

    foreach (var employee in employees)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName} | Type: {employee.EmploymentType} | Rate: {employee.HourlyRate:C} | Hours Worked: {employee.TotalHours} | Pay So Far: {employee.CalculatePay():C}");
    }
}

abstract class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public decimal HourlyRate { get; set; }
    private List<decimal> HoursWorked { get; } = new();
    // private List<(DateTime Date, decimal Hours) > HoursWorked { get; } = new();
    public abstract string EmploymentType { get; }

    public Employee(string firstName, string lastName, DateTime dob, decimal hourlyRate)
    {
        FirstName = firstName;
        LastName = lastName;
        DOB = dob;
        HourlyRate = hourlyRate;
    }

    public void AddHours(decimal hours) => HoursWorked.Add(hours);
    // public void AddHours(decimal hours) => HoursWorked.Add((DateTime.Now, hours)); // log hours with dates

    public decimal TotalHours => HoursWorked.Sum();

    public virtual decimal CalculatePay()
    {
        return TotalHours * HourlyRate;
    }

    public List<decimal> GetHours() => new(HoursWorked);
}


class FullTime : Employee
{
    public override string EmploymentType => "Full Time";

    public FullTime(string firstName, string lastName, DateTime dob, decimal hourlyRate)
        : base(firstName, lastName, dob, hourlyRate) { }

    public override decimal CalculatePay()
    {
        decimal regularHours = Math.Min(40, TotalHours);
        decimal overtime = Math.Max(0, TotalHours - 40);
        decimal pay = (regularHours * HourlyRate) + (overtime * HourlyRate * 1.5m);

        // Bonus for extreme overtime
        if (TotalHours > 50)
            pay += 100;

        return pay;
    }
}

class PartTime : Employee
{
    public override string EmploymentType => "Part Time";

    public PartTime(string firstName, string lastName, DateTime dob, decimal hourlyRate)
        : base(firstName, lastName, dob, hourlyRate) { }
}

class Contract : Employee
{
    public override string EmploymentType => "Contract";

    public Contract(string firstName, string lastName, DateTime dob, decimal hourlyRate)
        : base(firstName, lastName, dob, hourlyRate) { }

    public override decimal CalculatePay() => TotalHours * HourlyRate;
}
