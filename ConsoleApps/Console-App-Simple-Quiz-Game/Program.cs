/*
## **4️⃣ Simple Quiz Game**

**Scenario:**
A console-based quiz app where the teacher can add questions, and students can answer them.

**Requirements:**

* **Class:** `Question`

  * Properties: `Text`, `Options` (List<string>), `CorrectAnswer`
  * Method: `CheckAnswer(int choice)`

* **Main Program:**

  * Menu:

    ```
    1. Add Question
    2. Take Quiz
    3. View Questions
    4. Exit
    ```

**Concepts covered:** lists, loops, conditional logic, encapsulation, switch.

**Extra Challenge:**

* Randomize question order.
* Show score at the end.
* Support multiple correct answers.
*/


Console.WriteLine("=== Simple Quiz Game ===");

List<Question> questions = new List<Question>();

questions.Add(CreateQuestion("What does HTML stand for?",
    ("HyperText Markup Language", true),
    ("HighText Machine Language", false),
    ("Hyperloop Transfer Mode", false),
    ("Hyperlinks and Text Markup Language", false)
));

questions.Add(CreateQuestion("Which of the following is a NoSQL database?",
    ("MySQL", false),
    ("PostgreSQL", false),
    ("MongoDB", true),
    ("Oracle", false)
));

questions.Add(CreateQuestion("In Git, which command is used to upload local repository content to a remote repository?",
    ("git push", true),
    ("git commit", false),
    ("git pull", false),
    ("git merge", false)
));

questions.Add(CreateQuestion("What does CSS primarily control?",
    ("The structure of a web page", false),
    ("The content of a web page", false),
    ("The styling and layout of a web page", true),
    ("The database queries", false)
));

questions.Add(CreateQuestion("Which HTTP status code means \"Not Found\"?",
    ("200", false),
    ("301", false),
    ("404", true),
    ("500", false)
));

questions.Add(CreateQuestion("(Frontend) Which of the following is true about the useEffect hook in React?",
    ("It can only run once when the component mounts", true),
    ("It replaces setState", false),
    ("It runs after every render by default", false),
    ("It cannot have cleanup functions", false)
));

questions.Add(CreateQuestion("(Frontend) In CSS, what does flex: 1 0 auto; mean?",
    ("Grow 1, shrink 0, use automatic basis", true),
    ("Grow 0, shrink 1, fixed basis", false),
    ("Set font size automatically", false),
    ("Align items at center", false)
));

questions.Add(CreateQuestion("(Backend) Which HTTP method is idempotent?",
    ("POST", false),
    ("PATCH", false),
    ("PUT", true),
    ("CONNECT", false)
));

questions.Add(CreateQuestion("(Backend) In Node.js, which module allows you to create a simple web server?",
    ("fs", false),
    ("http", true),
    ("path", false),
    ("url", false)
));

questions.Add(CreateQuestion("(Fullstack) What is the main difference between SQL and NoSQL databases?",
    ("SQL is unstructured, NoSQL is structured", false),
    ("SQL uses tables and schemas; NoSQL is schema - less and uses documents, key - value, or graphs", true),
    ("NoSQL cannot handle large data", false),
    ("SQL is only for frontend apps", false)
));

questions.Add(CreateQuestion("(React) You have a component that fetches data from an API. Which useEffect dependency array is correct if you only want to fetch data once when the component mounts?",
    ("[]", true),
    ("[data]", false),
    ("[fetchData]", false),
    ("No dependency array", false)
));

questions.Add(CreateQuestion("(React) What is the main difference between controlled and uncontrolled components in React?",
    ("Controlled components handle state internally; uncontrolled use props", false),
    ("Controlled components have their value controlled via React state; uncontrolled components use the DOM to manage their value", true),
    ("Uncontrolled components can never update", false),
    ("Controlled components cannot use refs", false)
));

questions.Add(CreateQuestion("(Node.js) In Express.js, which middleware is used to parse incoming JSON payloads?",
    ("express.()", false),
    ("express.json()", true),
    ("body-parser.urlencoded()", false),
    ("express.urlencoded()", false)
));

questions.Add(CreateQuestion("(Node.js / Backend) What is the difference between require() and import in Node.js?",
    ("require() is asynchronous; import is synchronous", false),
    ("require() is CommonJS syntax; import is ES6 module syntax", true),
    ("There is no difference", false),
    ("require() can only import JSON", false)
));
questions.Add(CreateQuestion("(Databases) You want to store hierarchical JSON data in a database. Which is the best choice?",
    ("MySQL", false),
    ("MongoDB", true),
    ("PostgreSQL without JSONB", false),
    ("SQLite", false)
));

questions.Add(CreateQuestion("(Fullstack) In a REST API, which status code should you return when a request succeeds but the server has no content to return?",
    ("200", false),
    ("201", false),
    ("204", true),
    ("404", false)
));

questions.Add(CreateQuestion("(Fullstack) Which of these is a valid way to prevent SQL injection in a Node.js app using PostgreSQL?",
    ("Concatenating user input into queries", false),
    ("Using parameterized queries / prepared statements", true),
    ("Filtering special characters manually", false),
    ("Using string replace on all input", false)
));


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
            AddQuestion();
            break;
        case 2:
            TakeQuiz();
            break;
        case 3:
            ViewQuestions();
            break;
        case 4:
            Console.WriteLine("Exiting Quiz Game... Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("\n1.Add Question");
    Console.WriteLine("2.Take Quiz");
    Console.WriteLine("3.View Questions");
    Console.WriteLine("4.Exit\n");
}

// Generate unique question id's
int GenerateQuestionId()
{
    return questions.Count + 1;
}

Question CreateQuestion(string text, params (string answer, bool isCorrect)[] answers)
{
    var question = new Question(GenerateQuestionId(), text);
    foreach (var ans in answers)
    {
        question.AddAnswer(ans.answer, ans.isCorrect);
    }
    return question;
}

void AddQuestion()
{
    Console.WriteLine("Enter Question: ");
    string question = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrEmpty(question))
    {
        Console.WriteLine("Question field can't be empty");
        return;
    }

    questions.Add(new Question(GenerateQuestionId(), question));
    Console.WriteLine("Question successfull added.");

    Console.WriteLine("Would you like to add answer for the questin? {y/n}");
    string respond = Console.ReadLine()?.Trim().ToLower() ?? "";

    if (respond == "y" || respond == "yes")
    {
        AddAnswers();
    }

}

void AddAnswers()
{
    ViewQuestions();

    Console.Write("Enter Question Id: ");
    string questionId = Console.ReadLine()?.Trim() ?? "";

    var question = questions.FirstOrDefault(q => q.Id.ToString() == questionId);

    if (question == null)
    {
        Console.WriteLine($"No question found with the id of '{questionId}'.");
        return;
    }

    Console.WriteLine("Enter Question Options: ");
    string[] options = { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" };
    foreach (var option in options)
    {
        Console.Write($"Enter {option} (or leave empty to skip): ");
        string input = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Skipped.");
            continue;
        }

        if (question.GetAnswers().Any(a => a.Text.Equals(input, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("This answer already exists. Skipping.");
            continue;
        }

        bool isCorrect;
        Console.Write("Is that the correct answer (true/false): ");
        while (!bool.TryParse(Console.ReadLine()?.Trim(), out isCorrect))
        {
            Console.Write("Invalid input. Please enter 'true' or 'false': ");
        }

        question.AddAnswer(input, isCorrect);
    }

    Console.WriteLine("\nUpdated Answers:");
    foreach (var answer in question.GetAnswers())
    {
        Console.WriteLine($"- {answer.Text} {(answer.IsCorrect ? "(Correct)" : "")}");
    }

}


void TakeQuiz()
{
    if (questions.Count == 0)
    {
        Console.WriteLine("No questions found!");
        return;
    }

    int score = 0;

    // Shuffle questions (extra challenge)
    var rnd = new Random();
    var randomized = questions.OrderBy(q => rnd.Next()).ToList();

    for (int i = 0; i < randomized.Count; i++)
    {
        var q = randomized[i];
        Console.WriteLine($"\nQ{i + 1}: {q.Text}");

        var answers = q.GetAnswers().ToList();

        // Shuffle answers too
        answers = answers.OrderBy(a => rnd.Next()).ToList();

        for (int j = 0; j < answers.Count; j++)
        {
            Console.WriteLine($"{j + 1}. {answers[j].Text}");
        }

        Console.Write("Your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= answers.Count)
        {
            if (answers[choice - 1].IsCorrect)
            {
                Console.WriteLine("✅ Correct!");
                score++;
            }
            else
            {
                Console.WriteLine("❌ Incorrect.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Moving on...");
        }
    }

    Console.WriteLine($"\n🎯 Quiz Finished! You scored {score}/{randomized.Count}.");
}

void ViewQuestions()
{
    if (questions.Count == 0)
    {
        Console.WriteLine("No questions found!");
        return;
    }
    for (int i = 0; i < questions.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {questions[i].Text}");
        foreach (var answer in questions[i].GetAnswers())
        {
            Console.WriteLine($"- {answer.Text} {(answer.IsCorrect ? "(Correct)" : "")}");
        }
    }
}


public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    private List<Answer> Answers { get; set; } = new();

    public Question(int id, string question)
    {
        Id = id;
        Text = question;
    }
    public void AddAnswer(string text, bool isCorrect)
    {
        Answers.Add(new Answer { Text = text, IsCorrect = isCorrect });
    }

    public IReadOnlyList<Answer> GetAnswers() => Answers.AsReadOnly();
}

public class Answer
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }

}
