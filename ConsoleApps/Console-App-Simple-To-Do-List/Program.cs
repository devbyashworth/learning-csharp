/*
* Simple To-Do List (Console App)
Goal: Create a console app where the user can add, view, mark as done, and remove tasks.

Core Concepts: Lists, loops, methods, input/output

How to Approach:

Use a List<string> to store tasks.

Show a menu with choices: Add, View, Mark Done, Remove, Exit.

Use a loop and switch to handle the choices.

Mark tasks with "[x]" or "[ ]" to show status.

Go Further:

Use a class Task with Title and IsDone.

Save/load tasks from a .txt file.
*/

Console.WriteLine("\n=== Simple To-Do List (Console App) ===\n");

List<TaskItem> todoList = new List<TaskItem>();

while (true)
{
    ShowMenu();

    Console.Write("Choose an option (1-5): ");
    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            AddTask();
            break;
        case 2:
            ViewTasks();
            break;
        case 3:
            MarkTaskAsDone();
            break;
        case 4:
            RemoveTask();
            break;
        case 5:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. View Tasks");
    Console.WriteLine("3. Mark Task as Done");
    Console.WriteLine("4. Remove Task");
    Console.WriteLine("5. Exit\n");
}

void AddTask()
{
    Console.Write("Enter task title: ");
    string title = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrEmpty(title))
    {
        Console.WriteLine("Task cannot be empty.");
        return;
    }
    todoList.Add(new TaskItem(title));
    Console.WriteLine("Task added.");
}

void ViewTasks()
{
    if (todoList.Count == 0)
    {
        Console.WriteLine("No tasks found.");
        return;
    }
    Console.WriteLine("\nYour To-Do List:");
    for (int i = 0; i < todoList.Count; i++)
    {
        string status = todoList[i].IsDone ? "[x]" : "[ ]";
        Console.WriteLine($"{i + 1}. {status} {todoList[i].Title}");
    }
}

void MarkTaskAsDone()
{
    ViewTasks();
    if (todoList.Count == 0) return;

    Console.Write("Enter the task number to mark as done: ");
    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= todoList.Count)
    {
        todoList[index - 1].IsDone = true;
        Console.WriteLine($"Task '{todoList[index - 1].Title}' marked as done.");
    }
    else
    {
        Console.WriteLine("Invalid task number.");
    }
}

void RemoveTask()
{
    ViewTasks();
    if (todoList.Count == 0) return;

    Console.Write("Enter the task number to remove: ");
    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= todoList.Count)
    {
        Console.WriteLine($"Task '{todoList[index - 1].Title}' removed.");
        todoList.RemoveAt(index - 1);
    }
    else
    {
        Console.WriteLine("Invalid task number.");
    }
}

class TaskItem
{
    public string Title { get; set; }
    public bool IsDone { get; set; }

    public TaskItem(string title)
    {
        Title = title;
        IsDone = false;
    }
}