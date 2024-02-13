using System;
using BO;
using BlApi;

namespace BlTest
{
    internal class Program
    {
        static readonly IBl s_bl = Factory.Get();
        // Method for handling task-related information
        static void InfoOfTask(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    // Task creation
                    Console.WriteLine("Enter task alias");
                    string alias;
                    while (!TryReadInput("task alias", out alias)) ;

                    Console.WriteLine("Enter task's description");
                    string description;
                    while (!TryReadInput("task description", out description)) ;

                    Console.WriteLine("Enter task's Deliverables");
                    string deliverables;
                    while (!TryReadInput("task deliverables", out deliverables)) ;

                    Console.WriteLine("Enter task's Remarks");
                    string remarks;
                    while (!TryReadInput("task remarks", out remarks)) ;

                    Console.WriteLine("Enter task's EngineerExperience required");
                    Console.WriteLine("Enter engineer's level from 0 to 4");
                    int level;
                    while (!TryReadInput("engineer's level", out level) || level < 0 || level > 4)
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                    BO.EngineerExperience enLevel;
                    while (!Enum.TryParse<BO.EngineerExperience>(level.ToString(), out enLevel))
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                    BO.Task task = new BO.Task()
                    {
                        Id = 4, // Assuming a constant value for Id, you may adjust this as needed
                        Description = description,
                        Alias = alias,
                        CreatedAtDate = DateTime.Now,
                        Status = 0, // Assuming a default value for Status, you may adjust this as needed
                        Deliverables = deliverables,
                        Remarks = remarks,
                        ComplexityLevel = enLevel
                    };
                    try
                    {
                        int result = s_bl.Task.Create(task);
                        Console.WriteLine("The task was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c':
                    // Task reading
                    Console.WriteLine("Enter task's id to read");
                    int idRead;
                    while (!TryReadInput("task id", out idRead))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");

                    try
                    {
                        Console.WriteLine(s_bl.Task.Read(idRead)!.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd':
                    // Read all tasks
                    Console.WriteLine("All the tasks:");
                    var arrReadAllTasks = s_bl.Task.ReadAll();
                    foreach (var item in arrReadAllTasks)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e':
                    // Task updating
                    Console.WriteLine("Enter id of task to update");
                    int idUpdate;
                    while (!TryReadInput("task id", out idUpdate))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");

                    try
                    {
                        Console.WriteLine("Enter task alias");
                        string ualias;
                        while (!TryReadInput("task alias", out ualias)) ;

                        Console.WriteLine("Enter task's description");
                        string udescription;
                        while (!TryReadInput("task description", out udescription)) ;

                        Console.WriteLine("Enter task's Deliverables");
                        string udeliverables;
                        while (!TryReadInput("task deliverables", out udeliverables)) ;

                        Console.WriteLine("Enter task's Remarks");
                        string uremarks;
                        while (!TryReadInput("task remarks", out uremarks)) ;

                        Console.WriteLine("Enter task's EngineerExperience required");
                        Console.WriteLine("Enter engineer's level from 0 to 4");
                        int ulevel;
                        while (!TryReadInput("engineer's level", out ulevel) || ulevel < 0 || ulevel > 4)
                            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                        BO.EngineerExperience uenLevel;
                        while (!Enum.TryParse<BO.EngineerExperience>(ulevel.ToString(), out uenLevel))
                            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                        BO.Task utask = new BO.Task()
                        {
                            Id = idUpdate,
                            Description = udescription,
                            Alias = ualias,
                            CreatedAtDate = DateTime.Now,
                            Status = 0, // Assuming a default value for Status, you may adjust this as needed
                            Deliverables = udeliverables,
                            Remarks = uremarks,
                            ComplexityLevel = uenLevel
                        };

                        s_bl.Task.Update(utask);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }
        }

        // Method for handling engineer-related information
        static void InfoOfEngineers(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    // Engineer addition
                    Console.WriteLine("Enter engineer's id to add");
                    int id;
                    while (!TryReadInput("engineer id", out id))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    Console.WriteLine("Enter engineer's name");
                    string name;
                    while (!TryReadInput("engineer name", out name)) ;
                    Console.WriteLine("Enter engineer's email");
                    string email;
                    while (!TryReadInput("engineer email", out email)) ;
                    Console.WriteLine("Enter engineer's level from 0 to 4");
                    BO.EngineerExperience engineerLevel;
                    int level;
                    while (!TryReadInput("engineer level", out level) || level < 0 || level > 4)
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                    bool b = Enum.TryParse<BO.EngineerExperience>(level.ToString(), out engineerLevel);
                    if (!b)
                        throw new Exception("Error in parameter");
                    Console.WriteLine("Enter engineer's cost");
                    double cost;
                    while (!TryReadInput("engineer cost", out cost))
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    BO.Engineer myEngineer = new BO.Engineer() { Id = id, Name = name, Email = email, Level = engineerLevel, Cost = cost };
                    try
                    {
                        int result = s_bl.Engineer.Create(myEngineer);
                        Console.WriteLine("The engineer was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c':
                    // Engineer reading
                    Console.WriteLine("Enter engineer's id to read");
                    int idRead;
                    while (!TryReadInput("engineer id", out idRead))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    try
                    {
                        Console.WriteLine(s_bl.Engineer.Read(idRead)!.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd':
                    // Read all engineers
                    Console.WriteLine("All the engineers:");
                    var arrReadAllEngineers = s_bl.Engineer.ReadAll();
                    foreach (var item in arrReadAllEngineers)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e':
                    // Engineer updating
                    Console.WriteLine("Enter id of engineer to update");
                    int idUpdate;
                    while (!TryReadInput("engineer id", out idUpdate))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");

                    try
                    {
                        Console.WriteLine("Enter engineer's name");
                        string uname;
                        while (!TryReadInput("engineer name", out uname)) ;

                        Console.WriteLine("Enter engineer's email");
                        string uemail;
                        while (!TryReadInput("engineer email", out uemail)) ;

                        Console.WriteLine("Enter engineer's level from 0 to 4");
                        int ulevel;
                        while (!TryReadInput("engineer level", out ulevel) || ulevel < 0 || ulevel > 4)
                            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                        BO.EngineerExperience uenLevel;
                        while (!Enum.TryParse<BO.EngineerExperience>(ulevel.ToString(), out uenLevel))
                            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

                        Console.WriteLine("Enter engineer's cost");
                        double ucost;
                        while (!TryReadInput("engineer cost", out ucost))
                            Console.WriteLine("Invalid input. Please enter a valid number.");

                        BO.Engineer upEngineer = new BO.Engineer() { Id = idUpdate, Name = uname, Email = uemail, Level = uenLevel, Cost = ucost };
                        s_bl.Engineer.Update(upEngineer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'f':
                    // Engineer deletion
                    Console.WriteLine("Enter id of engineer to delete");
                    int idDelete;
                    while (!TryReadInput("engineer id", out idDelete))
                        Console.WriteLine("Invalid input. Please enter a valid integer.");

                    try
                    {
                        s_bl.Engineer.Delete(idDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }
        }

        // Method for handling milestone-related information
        static void InfoOfMilestone(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    bool successTryParse;
                    bool successToTryParse = true;
                    Console.WriteLine("Enter date of starting project");
                    DateTime _start;
                    successTryParse = DateTime.TryParse(Console.ReadLine(), out _start);
                    if (!successToTryParse)
                        Console.WriteLine("Invalid input");
                    Console.WriteLine("Enter date of ending project");
                    DateTime _end;
                    successToTryParse = DateTime.TryParse(Console.ReadLine(), out _end);
                    if (!successTryParse || _end < _start)
                        Console.WriteLine("Invalid input");
                    s_bl.Milestone.SetStartDate(_start);
                    s_bl.Milestone.CreateProjectSchedule();
                    Console.WriteLine("Projects schedule created successfully");
                    break;
                case 'c':
                    // Read milestone
                    // Create Projects Schedule
                    Console.WriteLine("Enter milestone's id for reading:\n");
                    int _id;
                    successTryParse = int.TryParse(Console.ReadLine(), out _id);
                    if (!successTryParse || _id < 0)
                        Console.WriteLine("Invalid id number");
                    try
                    {
                        Console.WriteLine(s_bl.Milestone.Read(_id)!.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd':
                    // Update milestone
                    string? userInput;
                    Console.WriteLine("Enter id of milestone to update");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Milestone baseMil = s_bl.Milestone.Read(id) ?? throw new BO.BlDoesNotExistException($"Milestone with id {id} does not exist", null!);
                    Console.WriteLine(baseMil);
                    Console.WriteLine("Enter milestone's details to update. If you don't want to change, press enter.\n");

                    Console.WriteLine("Descriptions:");
                    userInput = Console.ReadLine();
                    string? _descriptions = string.IsNullOrEmpty(userInput) ? baseMil.Description : userInput;

                    Console.WriteLine("Alias:");
                    userInput = Console.ReadLine();
                    string? _alias = string.IsNullOrEmpty(userInput) ? baseMil.Alias : userInput;

                    Console.WriteLine("Remarks:");
                    userInput = Console.ReadLine();
                    string? _remarks = string.IsNullOrEmpty(userInput) ? baseMil.Remarks : userInput;

                    Milestone updateMil = new()
                    {
                        Id = id,
                        Description = _descriptions,
                        Alias = _alias,
                        Status = baseMil.Status,
                        CreatedAtDate = baseMil.CreatedAtDate,
                        ForecastAtDate = baseMil.ForecastAtDate,
                        DeadlineDate = baseMil.DeadlineDate,
                        Complete = baseMil.Complete,
                        ProgressPercentage = baseMil.ProgressPercentage,
                        Remarks = _remarks,
                    };

                    s_bl.Milestone.Update(updateMil);
                    break;
                default:
                    break;
            }
        }

        // Method for reading input with try-parse validation
        static bool TryReadInput<T>(string inputName, out T result)
        {
            Console.WriteLine($"Enter {inputName}:");
            string userInput = Console.ReadLine()!;
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(userInput, out int intValue))
                {
                    result = (T)(object)intValue;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid input for {inputName}. Please enter a valid integer.");
                }
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(userInput, out double doubleValue))
                {
                    result = (T)(object)doubleValue;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid input for {inputName}. Please enter a valid number.");
                }
            }
            else if (typeof(T) == typeof(string))
            {
                result = (T)(object)userInput!;
                return true;
            }
            else if (typeof(T) == typeof(char))
            {
                if (char.TryParse(userInput, out char charValue))
                {
                    result = (T)(object)charValue;
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid input for {inputName}. Please enter a valid character.");
                }
            }
            result = default(T)!;
            return false;
        }

        public static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
            {
                try
                {
                    DalTest.Initialization.Do();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in parameters: " + ex);
                }
            }
            Console.WriteLine("For exit press 0");
            Console.WriteLine("For tasks press 1");
            Console.WriteLine("For engineers press 2");
            Console.WriteLine("For Milestone press 3");

            int select;
            while (!TryReadInput("selection", out select) || (select < 0 || select > 3))
                Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");

            char x;
            while (select != 0)
            {
                switch (select)
                {
                    case 1:
                        Console.WriteLine("For exit press a");
                        Console.WriteLine("For add a task press b");
                        Console.WriteLine("For read a task press c");
                        Console.WriteLine("For read all tasks press d");
                        Console.WriteLine("For update a task press e");
                        Console.WriteLine("For delete a task press f");
                        while (!TryReadInput("selection", out x)) ;
                        InfoOfTask(x);
                        break;
                    case 2:
                        Console.WriteLine("For exit press a");
                        Console.WriteLine("For add an engineer press b");
                        Console.WriteLine("For read an engineer press c");
                        Console.WriteLine("For read all engineer press d");
                        Console.WriteLine("For update an engineer press e");
                        Console.WriteLine("For delete an engineer press f");
                        while (!TryReadInput("selection", out x)) ;
                        InfoOfEngineers(x);
                        break;
                    case 3:
                        Console.WriteLine("Choose the method that you want to execute:\n a to exit\n b to Create Projects Schedule \n c to Read\n d Update\n ");
                        while (!TryReadInput("selection", out x)) ;
                        InfoOfMilestone(x);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("For exit press 0");
                Console.WriteLine("For tasks press 1");
                Console.WriteLine("For engineers press 2");
                Console.WriteLine("For Milestone press 3");
                while (!TryReadInput("selection", out select) || (select < 0 || select > 3))
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
            }
        }
    }
}
