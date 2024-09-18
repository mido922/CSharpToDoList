using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

string filePath = @"E:\test.txt";

if (!File.Exists(filePath)) {
    File.Create(filePath);
}

//string myJson = JsonConvert.SerializeObject(taskList);
//Console.Write(myJson);
//Console.Write(JsonConvert.DeserializeObject(myJson));

string userInput;
string jsonList;
int a;

List<toDoTask> taskList = new List<toDoTask>();

while (true)
{
    Console.WriteLine("----------------\n" +
        "Welcome to the Blackstone To-Do List management app. Please select a choice: \n" +
        "1) View all To-Do tasks. \n" +
        "2) Mark or Unmark a task. \n" +
        "3) Add a task. \n" +
        "4) Remove a task. \n" +
        "5) Close this program.");

    userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":

            readJsonFromFile();

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("Here is a list of all tasks: \n");
            displayTasks();
            break;
        case "2":
            readJsonFromFile();

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("Which task would you like to change the status of? " +
                                "Type -1 to exit.");
            displayTasks();

            if(!ValidateUserInput())
            {
                break;
            }

            if (taskList[Convert.ToInt32(userInput)].Status == "Unfinished.")
                    {
                        taskList[Convert.ToInt32(userInput)].Status = "Finished.";
                    }
            else
                    {
                        taskList[Convert.ToInt32(userInput)].Status = "Unfinished.";
                    }

            writeJsonToFile();

            break;
        case "3":
            readJsonFromFile();

            Console.WriteLine("What would you like to add to your To-Do List? Type -1 to exit.\n");

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
                    taskList.Add(new toDoTask(userInput, "Unfinished."));
                    break;
                }
            }

            writeJsonToFile();

            break;
        case "4":
            readJsonFromFile();

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("What would you like to remove from your To-Do List? Type -1 to exit.\n");
            displayTasks();

            if (!ValidateUserInput())
            {
                break;
            }

            taskList.RemoveAt(Convert.ToInt32(userInput));

            writeJsonToFile();
            break;
        case "5":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
    }
}

void displayTasks()
{
    taskList = JsonConvert.DeserializeObject<List<toDoTask>>(File.ReadAllText(filePath));

    for (int i = 0; i < taskList.Count; i = i + 1)
    {
        Console.WriteLine($"{i}) {taskList[i].Name}: {taskList[i].Status}");
    }
}

void readJsonFromFile()
{
    taskList = JsonConvert.DeserializeObject<List<toDoTask>>(File.ReadAllText(filePath));
}

void writeJsonToFile()
{
    jsonList = JsonConvert.SerializeObject(taskList);
    File.WriteAllText(filePath, jsonList);
}

Boolean ValidateUserInput()
{
    while (true)
    {
        userInput = Console.ReadLine();

        if (userInput == "-1")
        {
            return (false);
        }
        else if (!Int32.TryParse(userInput, out a) ||
                Convert.ToInt32(userInput) >= taskList.Count ||
                Convert.ToInt32(userInput) < 0 ||
                userInput == ""
                )
        {
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
        }
        else
        {
            return true;
        }
    }
}

public class toDoTask
{
    public string Name;
    public string Status;
    public toDoTask(string newName, string newStatus)
    {
        Name = newName;
        Status = newStatus;
    }
}
