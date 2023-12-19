
using DalApi;
using Dal;
using DO;

namespace DalTest
{
    internal class Program
    {
        //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        //static readonly IDal s_dal = new DalList(); //stage 2
       /* static readonly IDal s_dal = new Dal.DalXml();*/ //stage 3
        static readonly IDal s_dal = Factory.Get;
        static void InfoOfTask(char x)
        {
            switch (x)
            {
                case 'a': break;
                case 'b'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
                    Console.WriteLine("enter task alias");
                    string alias = Console.ReadLine()!;
                    Console.WriteLine("enter task's name");
                    string description = Console.ReadLine()!;
                    DO.Task task = new(4, description, alias);
                    try
                    {

                        int result = s_dal!.Task.Create(task);
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
                        Console.WriteLine(s_dal.Task.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd'://read all
                    Console.WriteLine("all the tasks:");
                    var arrReadAllTasks = s_dal.Task.ReadAll();
                    foreach (var item in arrReadAllTasks)
                        Console.WriteLine(item);
                    break;
                case 'e'://update
                    Console.WriteLine("enter id of task to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine(s_dal.Task.Read(idUpdate));
                        Console.WriteLine("enter task's description");
                        string tName = Console.ReadLine()!;
                        Console.WriteLine("enter task's alias");
                        string tAlias = Console.ReadLine()!;
                        DO.Task t = new(idUpdate, tName, tAlias);
                        s_dal.Task.Update(t);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'f'://delete a task
                    Console.WriteLine("enter id of task to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal.Task.Delete(idDelete);
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
                    DO.Engineer myEngineer = new(id, name, email,null,null);
                    try
                    {
                        int result = s_dal.Engineer.Create(myEngineer);
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
                        Console.WriteLine(s_dal.Engineer.Read(idRead));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd'://read all
                    Console.WriteLine("all the engineers:");
                    var arrReadAllEngineers = s_dal.Engineer.ReadAll();
                    foreach (var item in arrReadAllEngineers)
                        Console.WriteLine(item);
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
                        Console.WriteLine("enter engineer's level from 0- to 2");
                        int? level = int.Parse(Console.ReadLine()!);
                        EngineerExperience enLevel;
                        bool b = Enum.TryParse<EngineerExperience>(level.ToString(), out enLevel);
                        if (!b)
                            throw new Exception("I tell you to put between 0 to 2");
                        enLevel = (EngineerExperience)level;
                        Console.WriteLine("enter engineer's cost");
                        double cost = double.Parse(Console.ReadLine()!);
                        DO.Engineer upEngineer = new(idUpdate, uname, uemail, enLevel, cost);
                        s_dal.Engineer.Update(upEngineer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'f'://delete a product
                    Console.WriteLine("enter id of engineer to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal.Engineer.Delete(idDelete);
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
        public static void InfoOfDependencies(char x)
        {
            switch (x)
            {
                case 'a': break;
                case 'b'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
                    Console.WriteLine("enter  dependent task id");
                    int tId = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task befor id");
                    int tIdBefor = int.Parse(Console.ReadLine()!);
                    DO.Dependency d = new(4, tId, tIdBefor);
                    try
                    {

                        int result = s_dal!.Dependency.Create(d);
                        Console.WriteLine("the dependency was added");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'c'://read by id
                    Console.WriteLine("enter dependency id to read");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        Console.WriteLine(s_dal.Dependency.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'd'://read all
                    Console.WriteLine("all the dependencies:");
                    var arrReadAllDepdndencies = s_dal.Dependency.ReadAll();
                    foreach (var item in arrReadAllDepdndencies)
                        Console.WriteLine(item);
                    break;
                case 'e'://update
                    Console.WriteLine("enter id of dependency to update");
                    int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                    try
                    {
                        Console.WriteLine("enter  dependent task id");
                        int dId = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("enter task befor id");
                        int dIdBefor = int.Parse(Console.ReadLine()!);
                        DO.Dependency depend = new(idUpdate, dId, dIdBefor);
                        //Console.WriteLine("enter created date");
                        //Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                        //pUpdate._category = (ECategory)int.Parse(Console.ReadLine());
                        //Console.WriteLine("enter product's instock");
                        //pUpdate._inStock = int.Parse(Console.ReadLine());
                        //Console.WriteLine("enter product's parve(0/1)");
                        //pUpdate._parve = int.Parse(Console.ReadLine());
                        //dalProduct.update(pUpdate);
                        s_dal.Dependency.Update(depend);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 'f'://delete a dependency
                    Console.WriteLine("enter id of dependency to delete");
                    int idDelete = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal.Dependency.Delete(idDelete);
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
        public static void Main(string[] args)
        {
            //the Main Menu
            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                                                                           //string? ans = "Y";
            string? ans=Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y") //stage 3

                try
                {
                    s_dal.Reset();
                    //Initialization.Do(s_dal);
                  	Initialization.Do(); //stage 4
                }
                catch(Exception ex)
                {
                    Console.WriteLine("error in parameters"+ex);
                }
            //Internal menu for every class and sending to funtion that treat this class

            Console.WriteLine("for tasks press 1");
            Console.WriteLine("for engineers press 2");
            Console.WriteLine("for dependencies 3");
            Console.WriteLine("for exit press 0");
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
                        InfoOfDependencies(x);


                        break;
                    default:
                        break;
                }
                Console.WriteLine("enter a number");
                select = int.Parse(Console.ReadLine()!);
            }
        }
    }
}


