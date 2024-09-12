// Console App to create a to-do list.

string userInput = "";
string[] toDoTasks = [];
string[] toDoTaskStatus = [];
string[] tempArray = [];
string[] tempArray2 = [];

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
            Console.WriteLine("\n Here is a list of all tasks: \n");
            for (int i = 0; i < (toDoTasks.Length); i=i+1)
            {
                Console.WriteLine($"{i}) {toDoTasks[i]}: {toDoTaskStatus[i]} ");
            }
            Console.WriteLine("-------------------------------");
            break;
        case "2":
            Console.WriteLine("Which task would you like to change the status of?");
            for (int i = 0; i < toDoTasks.Length; i=i+1)
            {
                Console.WriteLine($"{i}) {toDoTasks[i]}: {toDoTaskStatus[i]} ");
            }
            while (true)
            {
                userInput = Console.ReadLine();
                if (Convert.ToInt32(userInput) > toDoTasks.Length || Convert.ToInt32(userInput) < 0)
                {
                    Console.WriteLine("That's not a valid input. Please try again.");
                    continue;
                }
                else
                {
                    if (toDoTaskStatus[Convert.ToInt32(userInput)] == "Unfinished.")
                    {
                        toDoTaskStatus[Convert.ToInt32(userInput)] = "Finished.";
                    }
                    else if (toDoTaskStatus[Convert.ToInt32(userInput)] == "Finished.")
                    {
                        toDoTaskStatus[Convert.ToInt32(userInput)] = "Unfinished.";
                    }
                    break;
                }
            }
            break;
        case "3":
            Console.WriteLine("What would you like to add to your To-Do List?\n");
            userInput = Console.ReadLine();
            toDoTasks = toDoTasks.Append(userInput).ToArray();
            toDoTaskStatus = toDoTaskStatus.Append("Unfinished.").ToArray();
            break;
        case "4":
            Console.WriteLine("What would you like to remove from your To-Do List?\n");
            for (int i = 0; i < (toDoTasks.Length); i = i + 1)
            {
                Console.WriteLine($"{i}) {toDoTasks[i]}: {toDoTaskStatus[i]} ");
            }
            userInput = Console.ReadLine();
            toDoTasks[Convert.ToInt32(userInput)] = "";
            toDoTaskStatus[Convert.ToInt32(userInput)] = "";

            for(int i = 0; i < (toDoTasks.Length); i = i + 1)
            {
                if (toDoTasks[i] != "")
                {
                    tempArray = tempArray.Append(toDoTasks[i]).ToArray();
                    tempArray2 = tempArray2.Append(toDoTaskStatus[i]).ToArray();
                }    
            }

            Console.WriteLine(tempArray);
            toDoTasks = tempArray;
            toDoTaskStatus = tempArray2;

            break;
        case "5":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("That's not a valid input. Please try again.");
            continue;
    }
}