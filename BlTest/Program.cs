using DalApi;
using DalTest;


namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        static void InfoOfTask(char x)
        {
            switch (x)
            {
                case 'a': break;
                case 'b'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
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
                        DependenceList = null,
                        Milestone = null,
                        BaselineStartDate = null,
                        StartDate = null,
                        ForecastDate = null,
                        DeadlineDate = null,
                        CompleteDate = null,
                        Deliverables = deliverables,
                        Remarks = remarks,
                        Engineer = null,
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
                case 'c'://read by id
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
                case 'd'://read all
                    Console.WriteLine("all the tasks:");
                    var arrReadAllTasks = s_bl.Task.ReadAll();
                    foreach (var item in arrReadAllTasks)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e'://update
                    Console.WriteLine("enter id of task to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
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
                            Id = 4,
                            Description = udescription,
                            Alias = ualias,
                            CreatedAtDate = DateTime.Now,
                            Status = 0,
                            //DependenceList = null,
                            //Milestone = null,
                            //BaselineStartDate = null,
                            //StartDate = null,
                            //ForecastDate = null,
                            //DeadlineDate = null,
                            //CompleteDate = null,
                            Deliverables = udeliverables,
                            Remarks = uremarks,
                            //Engineer = null,
                            ComplexityLevel = uenLevel
                        };
                        s_bl.Task.Update(utask);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                //case 'f'://delete a task
                //    Console.WriteLine("enter id of task to delete");
                //    int idDelete = int.Parse(Console.ReadLine()!);
                //    try
                //    {
                //        s_bl.Task.Delete(idDelete);
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex);
                //    }
                //    break;
                default:
                    break;
            }
        }


        public static void InfoOfEngineers(char x)
        {
            switch (x)
            {
                case 'a': break;
                case 'b'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
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
                case 'c'://read by id
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
                case 'd'://read all
                    Console.WriteLine("all the engineers:");
                    var arrReadAllEngineers = s_bl.Engineer.ReadAll();
                    foreach (var item in arrReadAllEngineers)
                        Console.WriteLine(item.ToString());
                    break;
                case 'e'://update
                    Console.WriteLine("enter id of engineer to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
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
                        // enLevel = (EngineerExperience)level;
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
                case 'f'://delete a engineer
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
        //public static void InfoOfDependencies(char x)
        //{
        //    switch (x)
        //    {
        //        case 'a': break;
        //        case 'b'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
        //            Console.WriteLine("enter  dependent task id");
        //            int tId = int.Parse(Console.ReadLine()!);
        //            Console.WriteLine("enter task befor id");
        //            int tIdBefor = int.Parse(Console.ReadLine()!);
        //            DO.Dependency d = new(4, tId, tIdBefor);
        //            try
        //            {

        //                int result = s_dal!.Dependency.Create(d);
        //                Console.WriteLine("the dependency was added");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //            }
        //            break;
        //        case 'c'://read by id
        //            Console.WriteLine("enter dependency id to read");
        //            int id = int.Parse(Console.ReadLine()!);
        //            try
        //            {
        //                Console.WriteLine(s_dal.Dependency.Read(id));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //            }
        //            break;
        //        case 'd'://read all
        //            Console.WriteLine("all the dependencies:");
        //            var arrReadAllDepdndencies = s_dal.Dependency.ReadAll();
        //            foreach (var item in arrReadAllDepdndencies)
        //                Console.WriteLine(item);
        //            break;
        //        case 'e'://update
        //            Console.WriteLine("enter id of dependency to update");
        //            int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
        //            try
        //            {
        //                Console.WriteLine("enter  dependent task id");
        //                int dId = int.Parse(Console.ReadLine()!);
        //                Console.WriteLine("enter task befor id");
        //                int dIdBefor = int.Parse(Console.ReadLine()!);
        //                DO.Dependency depend = new(idUpdate, dId, dIdBefor);
        //                //Console.WriteLine("enter created date");
        //                //Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
        //                //pUpdate._category = (ECategory)int.Parse(Console.ReadLine());
        //                //Console.WriteLine("enter product's instock");
        //                //pUpdate._inStock = int.Parse(Console.ReadLine());
        //                //Console.WriteLine("enter product's parve(0/1)");
        //                //pUpdate._parve = int.Parse(Console.ReadLine());
        //                //dalProduct.update(pUpdate);
        //                s_dal.Dependency.Update(depend);

        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //            }
        //            break;
        //        case 'f'://delete a dependency
        //            Console.WriteLine("enter id of dependency to delete");
        //            int idDelete = int.Parse(Console.ReadLine()!);
        //            try
        //            {
        //                s_dal.Dependency.Delete(idDelete);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex);
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");                                                              //string? ans = "Y";
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y") //stage 3

                try
                {
                    //Initialization.Do(s_dal);
                    
                    DalTest.Initialization.Do(); //stage 4
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error in parameters" + ex);
                }
            //Internal menu for every class and sending to funtion that treat this class
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
                        InfoOfTask(x);//doing this function 
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
                        Console.WriteLine("for exit press a");
                        Console.WriteLine("for add a dependency befor another press b");
                        Console.WriteLine("for read dependency befor another press c");
                        Console.WriteLine("for read all dependencies befor anothers press d");
                        Console.WriteLine("for update an dependency befor another press e");
                        Console.WriteLine("for delete an dependency befor another press f");
                        x = char.Parse(Console.ReadLine()!);
                        //InfoOfDependencies(x);


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

