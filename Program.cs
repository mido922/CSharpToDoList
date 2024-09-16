// Console app to create a to-do list

// Refactoring to be filebased

using System.IO;

string filePath = @"E:\test.txt";
string filePath2 = @"E:\test2.txt";

if (!File.Exists(filePath)) {
    File.Create(filePath);
}

if (!File.Exists(filePath2))
{
    File.Create(filePath2);
}

string userInput = "";
string[] tempArray = [];
string[] tempArray2 = [];
string[] taskListFile;
string[] taskStatusFile;
int a;

while (true)
{
    Console.WriteLine("Welcome to the Blackstone To-Do List management app. Please select a choice: \n" +
        "1) View all To-Do tasks. \n" +
        "2) Mark or Unmark a task. \n" +
        "3) Add a task. \n" +
        "4) Remove a task. \n" +
        "5) Close this program.");

    userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            taskListFile = File.ReadAllLines(filePath);
            taskStatusFile = File.ReadAllLines(filePath2);

            if (taskListFile.Length == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }
            Console.WriteLine("\nHere is a list of all tasks: \n");
            for (int i = 0; i < (taskListFile.Length); i = i + 1)
            {
                Console.WriteLine($"{i}) {taskListFile[i]}: {taskStatusFile[i]} ");
            }
            break;

        case "2":
            taskListFile = File.ReadAllLines(filePath);
            taskStatusFile = File.ReadAllLines(filePath2);

            if (taskListFile.Length == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("Which task would you like to change the status of? Type -1 to exit.");

            for (int i = 0; i < taskListFile.Length; i = i + 1)
            {
                Console.WriteLine($"{i}) {taskListFile[i]}: {taskStatusFile[i]}");
            }
            while (true)
            {
                userInput = Console.ReadLine();

                if (userInput == "-1")
                {
                    break;
                }
                else if (!Int32.TryParse(userInput, out a) ||
                        Convert.ToInt32(userInput) >= taskListFile.Length ||
                        Convert.ToInt32(userInput) < 0 ||
                        userInput == ""
                        )
                {
                    Console.WriteLine("That's not a valid input. Please try again.");
                    continue;
                }
                else
                {
                    if (taskStatusFile[Convert.ToInt32(userInput)] == "Unfinished.")
                    {
                        taskStatusFile[Convert.ToInt32(userInput)] = "Finished.";
                    }
                    else
                    {
                        taskStatusFile[Convert.ToInt32(userInput)] = "Unfinished.";
                    }
                    File.WriteAllLines(filePath2, taskStatusFile);
                    break;
                }
            }
            break;
        case "3":
            Console.WriteLine("What would you like to add to your To-Do List? Type -1 to exit.\n");


            taskListFile = File.ReadAllLines(filePath);
            taskStatusFile = File.ReadAllLines(filePath2);

            while (true)
            {
                userInput = Console.ReadLine();

                if (userInput == "-1")
                {
                    break;
                }
                else if (Int32.TryParse(userInput, out a) ||
                    userInput == ""
                    )
                {
                    Console.WriteLine("That's not a valid input. Please try again.");
                    continue;
                }
                else
                {
                    taskListFile = taskListFile.Append(userInput).ToArray();
                    taskStatusFile = taskStatusFile.Append("Unfinished.").ToArray();


                    File.WriteAllLines(filePath, taskListFile);
                    File.WriteAllLines(filePath2, taskStatusFile);
                    break;
                }
            }
            break;
        case "4":

            taskListFile = File.ReadAllLines(filePath);
            taskStatusFile = File.ReadAllLines(filePath2);

            if (taskListFile.Length == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }
            Console.WriteLine("What would you like to remove from your To-Do List? Type -1 to exit.\n");
            for (int i = 0; i < taskListFile.Length; i = i + 1)
            {
                Console.WriteLine($"{i}) {taskListFile[i]}: {taskStatusFile[i]} ");
            }

            while (true)
            {
                userInput = Console.ReadLine();

                if (userInput == "-1")
                {
                    break;
                }
                if (!Int32.TryParse(userInput, out a) ||
                    Convert.ToInt32(userInput) >= taskListFile.Length ||
                    Convert.ToInt32(userInput) < 0 ||
                    userInput == ""
                    )
                {
                    Console.WriteLine("That's not a valid input. Please try again.");
                    continue;
                }
                else
                {

                    taskListFile[Convert.ToInt32(userInput)] = "";
                    taskStatusFile[Convert.ToInt32(userInput)] = "";

                    for (int i = 0; i < (taskStatusFile.Length); i = i + 1)
                    {
                        if (taskStatusFile[i] != "")
                        {
                            tempArray = tempArray.Append(taskListFile[i]).ToArray();
                            tempArray2 = tempArray2.Append(taskStatusFile[i]).ToArray();
                        }
                    }

                    taskListFile = tempArray;
                    taskStatusFile = tempArray2;

                    File.WriteAllLines(filePath, taskListFile);
                    File.WriteAllLines(filePath2, taskStatusFile);

                    break;
                }
            }
            break;
        case "5":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
    }
}
