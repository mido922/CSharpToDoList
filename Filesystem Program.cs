using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using System.ComponentModel;

string filePath = @"E:\test.txt";

if (!File.Exists(filePath)) {
    File.Create(filePath);
}

string userInput;
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

            taskList = readJsonFromFile(filePath);

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("Here is a list of all tasks: \n");
            displayTasks();
            break;
        case "2":
            readJsonFromFile(filePath);

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("Which task would you like to change the status of? " +
                                "Type -1 to exit.");
            displayTasks();

            userInput = ValidateUserIntegerInput();

            if (userInput == "false")
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

            writeJsonToFile(taskList);

            break;
        case "3":
            readJsonFromFile(filePath);

            Console.WriteLine("What would you like to add to your To-Do List?" +
                "Type -1 to exit.\n");

            while (true)
            {
                userInput = ValidateUserStringInput();

                if (userInput == "false")
                {
                    break;
                }
                else
                {
                    toDoTask asdf = new toDoTask
                    {
                        Name = userInput,
                        Status = "Unfinished."
                    };

                    taskList.Add(asdf);

                    Console.WriteLine(asdf.Name);
                    break;
                }
            }

            writeJsonToFile(taskList);

            break;
        case "4":
            readJsonFromFile(filePath);

            if (taskList.Count == 0)
            {
                Console.WriteLine("You have no tasks.");
                break;
            }

            Console.WriteLine("What would you like to remove from your To-Do List? Type -1 to exit.\n");
            displayTasks();

            userInput = ValidateUserIntegerInput();

            if (userInput == "false")
                break;

            taskList.RemoveAt(Convert.ToInt32(userInput));

            writeJsonToFile(taskList);
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
    taskList = JsonSerializer.Deserialize<List<toDoTask>>(File.ReadAllText(filePath));

    for (int i = 0; i < taskList.Count; i = i + 1)
        Console.WriteLine($"{i}) {taskList[i].Name}: {taskList[i].Status}");
}

List<toDoTask> readJsonFromFile(string fileToGet)
{
    string newText = File.ReadAllText(fileToGet);
    if (newText.Length > 0)
        return (JsonSerializer.Deserialize<List<toDoTask>>(newText));
    else
        return (new List<toDoTask>());
}

void writeJsonToFile(List<toDoTask> jsonifyList)
{
    File.WriteAllText(filePath, JsonSerializer.Serialize<List<toDoTask>>(jsonifyList));
}

string ValidateUserStringInput()
{
    string inputtedUserInput;
    while (true)
    {
        inputtedUserInput = Console.ReadLine();
        
        if (inputtedUserInput == "-1")
            return ("false");
            
        else if (inputtedUserInput == "" ||
                int.TryParse(inputtedUserInput, out a))
            {
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
            }
        else
            return (inputtedUserInput);
    }
}

string ValidateUserIntegerInput()
{
    string inputtedUserInput;
    while (true)
    {
        inputtedUserInput = Console.ReadLine();

        if (inputtedUserInput == "-1")
            return ("false");

        else if (inputtedUserInput == "" ||
                !int.TryParse(inputtedUserInput, out a) ||
                Convert.ToInt32(inputtedUserInput) < 0 ||
                Convert.ToInt32(inputtedUserInput) >= taskList.Count)
        {
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
        }
        else
            return (inputtedUserInput);
    }
}

public class toDoTask
{
    public string Name { get; set; }
    public string Status { get; set; }
    public toDoTask()
    {
    }
}