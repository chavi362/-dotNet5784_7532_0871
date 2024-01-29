using System;
using BO;
using BlApi;

namespace BlTest
{
    internal class Program
    {
        static readonly IBl s_bl = Factory.Get();

        static void InfoOfTask(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    Console.WriteLine("enter task alias");
                    string alias = Console.ReadLine()!;
                    Console.WriteLine("enter task's description");
                    string description = Console.ReadLine()!;
                    Console.WriteLine("enter task's Deliverables");
                    string deliverables = Console.ReadLine()!;
                    Console.WriteLine("enter task's Remarks");
                    string remarks = Console.ReadLine()!;
                    Console.WriteLine("enter task's EngineerExperience required");
                    Console.WriteLine("enter engineer's level from 0- to 4");
                    int? level = int.Parse(Console.ReadLine()!);
                    BO.EngineerExperience enLevel;
                    bool b = Enum.TryParse<BO.EngineerExperience>(level.ToString(), out enLevel);
                    if (!b)
                        throw new Exception("I tell you to put between 0 to 4");

                    BO.Task task = new BO.Task()
                    {
                        Id = 4,
                        Description = description,
                        Alias = alias,
                        CreatedAtDate = DateTime.Now,
                        Status = 0,
                        Deliverables = deliverables,
                        Remarks = remarks,
                        ComplexityLevel = enLevel
                    };

                    try
                    {
                        int result = s_bl.Task.Create(task);
                        Console.WriteLine("the task was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c':
                    Console.WriteLine("enter tasks's id to read");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_bl.Task.Read(id)!.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd':
                    Console.WriteLine("all the tasks:");
                    var arrReadAllTasks = s_bl.Task.ReadAll();
                    foreach (var item in arrReadAllTasks)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e':
                    Console.WriteLine("enter id of task to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);

                    try
                    {
                        Console.WriteLine("enter task alias");
                        string ualias = Console.ReadLine()!;
                        Console.WriteLine("enter task's description");
                        string udescription = Console.ReadLine()!;
                        Console.WriteLine("enter task's Deliverables");
                        string udeliverables = Console.ReadLine()!;
                        Console.WriteLine("enter task's Remarks");
                        string uremarks = Console.ReadLine()!;
                        Console.WriteLine("enter task's EngineerExperience required");
                        Console.WriteLine("enter engineer's level from 0- to 4");
                        int? ulevel = int.Parse(Console.ReadLine()!);
                        BO.EngineerExperience uenLevel;
                        bool bo = Enum.TryParse<BO.EngineerExperience>(ulevel.ToString(), out uenLevel);
                        if (!bo)
                            throw new Exception("I tell you to put between 0 to 4");

                        BO.Task utask = new BO.Task()
                        {
                            Id = idUpdate,
                            Description = udescription,
                            Alias = ualias,
                            CreatedAtDate = DateTime.Now,
                            Status = 0,
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

        static void InfoOfEngineers(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    Console.WriteLine("enter engineer's id to add");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter engineer's name");
                    string name = Console.ReadLine()!;
                    Console.WriteLine("enter engineer's email");
                    string email = Console.ReadLine()!;
                    Console.WriteLine("enter engineer's level 0- to 4");
                    BO.EngineerExperience engineerLevel;
                    int? level = int.Parse(Console.ReadLine()!);
                    bool b = Enum.TryParse<BO.EngineerExperience>(level.ToString(), out engineerLevel);
                    if (!b)
                        throw new Exception("error in parameter");
                    Console.WriteLine("enter engineer's cost");
                    double cost = double.Parse(Console.ReadLine()!);
                    BO.Engineer myEngineer = new BO.Engineer() { Id = id, Name = name, Email = email, Level = engineerLevel, Cost = cost };
                    try
                    {
                        int result = s_bl.Engineer.Create(myEngineer);
                        Console.WriteLine("the engineer was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c':
                    Console.WriteLine("enter engineer's id to read");
                    int idRead = int.Parse(Console.ReadLine()!);
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
                    Console.WriteLine("all the engineers:");
                    var arrReadAllEngineers = s_bl.Engineer.ReadAll();
                    foreach (var item in arrReadAllEngineers)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e':
                    Console.WriteLine("enter id of engineer to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);

                    try
                    {
                        Console.WriteLine("enter engineer's name");
                        string uname = Console.ReadLine()!;
                        Console.WriteLine("enter engineer's email");
                        string uemail = Console.ReadLine()!;
                        Console.WriteLine("enter engineer's level from 0- to 4");
                        int? ulevel = int.Parse(Console.ReadLine()!);
                        BO.EngineerExperience uenLevel;
                        bool bo = Enum.TryParse<BO.EngineerExperience>(ulevel.ToString(), out uenLevel);
                        if (!bo)
                            throw new Exception("I tell you to put between 0 to 4");

                        Console.WriteLine("enter engineer's cost");
                        double ucost = double.Parse(Console.ReadLine()!);
                        BO.Engineer upEngineer = new BO.Engineer() { Id = idUpdate, Name = uname, Email = uemail, Level = uenLevel, Cost = ucost };
                        s_bl.Engineer.Update(upEngineer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'f':
                    Console.WriteLine("enter id of engineer to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
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

        static void InfoOfMilestone(char x)
        {
            switch (x)
            {
                case 'a':
                    break;
                case 'b':
                    Console.WriteLine("Enter milestone's id for reading:\n");
                    bool succesTryParse;
                    int _id;
                    succesTryParse = int.TryParse(Console.ReadLine(), out _id);
                    if (!succesTryParse || _id < 0)
                        throw new BlInvalidPropertyException("Invalid id number.\n");
                    try
                    {
                        Console.WriteLine(s_bl.Milestone.Read(_id)!.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c':
                    bool succesTryParse = true;
                    Console.WriteLine("enter date of starting project");
                    DateTime _start;
                    succesTryParse = DateTime.TryParse(Console.ReadLine(), out _start);
                    if (!succesTryParse)
                        throw new BlInvalidInput("Invalid input.\n");

                    Console.WriteLine("enter date of ending project");
                    DateTime _end;
                    succesTryParse = DateTime.TryParse(Console.ReadLine(), out _end);
                    if (!succesTryParse || _end < _start)
                        throw new BlInvalidInput("Invalid input.\n");
                    s_bl.Milestone.SetDates(_start, _end);
                    s_bl.Milestone.CreateProjectsSchedule();
                    Console.WriteLine("Projects schedule created successfully");
                    break;
                case 'd':
                    string? userInput;
                    Console.WriteLine("Enter id of milestone to update");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Milestone baseMil = s_bl.Milestone.Read(id) ?? throw new BlDoesNotExistException($"Milestone with id {id} does not exist");
                    Console.WriteLine(baseMil);
                    Console.WriteLine("Enter milestone's details to update. If you don't want to change, press enter.\n");

                    Console.WriteLine("descriptions:");
                    userInput = Console.ReadLine();
                    string? _descriptions = string.IsNullOrEmpty(userInput) ? baseMil.Description : userInput;

                    Console.WriteLine("alias:");
                    userInput = Console.ReadLine();
                    string? _alias = string.IsNullOrEmpty(userInput) ? baseMil.Alias : userInput;

                    Console.WriteLine("remarks:");
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
                        CompleteDate = baseMil.CompleteDate,
                        CompletionPercentage = baseMil.CompletionPercentage,
                        Remarks = _remarks
                    };

                    s_bl.Milestone.Update(updateMil);
                    break;
                default:
                    break;
            }
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
                    Console.WriteLine("error in parameters" + ex);
                }
            }

            Console.WriteLine("for exit press 0");
            Console.WriteLine("for tasks press 1");
            Console.WriteLine("for engineers press 2");
            Console.WriteLine("for Milestone press 3");

            int select = int.Parse(Console.ReadLine()!);
            char x;

            while (select != 0)
            {
                switch (select)
                {
                    case 1:
                        Console.WriteLine("for exit press a");
                        Console.WriteLine("for add a task press b");
                        Console.WriteLine("for read a task press c");
                        Console.WriteLine("for read all tasks press d");
                        Console.WriteLine("for update a task press e");
                        Console.WriteLine("for delete a task press f");
                        x = char.Parse(Console.ReadLine()!);
                        InfoOfTask(x);
                        break;
                    case 2:
                        Console.WriteLine("for exit press a");
                        Console.WriteLine("for add an engineer press b");
                        Console.WriteLine("for read an engineer press c");
                        Console.WriteLine("for read all engineer press d");
                        Console.WriteLine("for update an engineer press e");
                        Console.WriteLine("for delete an engineer press f");
                        x = char.Parse(Console.ReadLine()!);
                        InfoOfEngineers(x);
                        break;
                    case 3:
                        Console.WriteLine("Choose the method that you want to execute:\n a to exit\n b to Create Projects Schedule \n c to Read\n d Update\n ");
                        x = char.Parse(Console.ReadLine()!);
                        InfoOfMilestone(x);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("for exit press 0");
                Console.WriteLine("for tasks press 1");
                Console.WriteLine("for engineers press 2");
                Console.WriteLine("for Milestone press 3");
                select = int.Parse(Console.ReadLine()!);
            }
        }
    }
}
