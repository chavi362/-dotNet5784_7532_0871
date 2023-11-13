
using DalApi;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using Dal;
using DO;

namespace DalTest;
class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    public static void InfoOfTask(char x)
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

                    int result = s_dalTask!.Create(task);
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
                    Console.WriteLine(s_dalTask?.Read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'd'://read all
                Console.WriteLine("all the tasks:");
                List<DO.Task> arrReadAllTasks = s_dalTask!.ReadAll();
                foreach (var item in arrReadAllTasks)
                    Console.WriteLine(item);
                break;
            case 'e'://update
                Console.WriteLine("enter id of task to update");
                int idUpdate = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalTask?.Read(idUpdate));
                    Console.WriteLine("enter task's description");
                    string tName = Console.ReadLine()!;
                    Console.WriteLine("enter task's alias");
                    string tAlias = Console.ReadLine()!;
                    DO.Task t = new(idUpdate, tName, tAlias);
                    s_dalTask?.Update(t);

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
                    s_dalTask?.Delete(idDelete);
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
                DO.Engineer myEngineer = new(id, name, email);
                try
                {
                    int result = s_dalEngineer!.Create(myEngineer);
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
                    Console.WriteLine(s_dalEngineer!.Read(idRead));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'd'://read all
                Console.WriteLine("all the engineers:");
                List<DO.Engineer> arrReadAllEngineers = s_dalEngineer!.ReadAll();
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
                    s_dalEngineer!.Update(upEngineer);
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
                    s_dalEngineer!.Delete(idDelete);
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

                    int result = s_dalDependency!.Create(d);
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
                    Console.WriteLine(s_dalDependency?.Read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'd'://read all
                Console.WriteLine("all the dependencies:");
                List<DO.Dependency> arrReadAllDepdndencies = s_dalDependency!.ReadAll();
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
                    s_dalDependency?.Update(depend);

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
                    s_dalDependency?.Delete(idDelete);
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

        try
        {

            Initialization.Do(s_dalTask, s_dalDependency, s_dalEngineer);
        }
        catch
        {
            Console.WriteLine("error in parameters");
        }
        Console.WriteLine("for tasks press 1");
        Console.WriteLine("for engeeners press 2");
        Console.WriteLine("for dependencies 3");
        Console.WriteLine("for exit press 0");
        int select = int.Parse(Console.ReadLine()!);
        char x;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for add a task press a");
                    Console.WriteLine("for read a task press b");
                    Console.WriteLine("for read all tasks press c");
                    Console.WriteLine("for update a task press d");
                    Console.WriteLine("for delete a task press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfTask(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an engeener press a");
                    Console.WriteLine("for read an engeener press b");
                    Console.WriteLine("for read all engeeners press c");
                    Console.WriteLine("for update an engeener press d");
                    Console.WriteLine("for delete an engeener press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfEngineers(x);
                    //doing this function 
                    break;
                case 3:
                    Console.WriteLine("for add a dependency befor another press a");
                    Console.WriteLine("for read dependency befor another press b");
                    Console.WriteLine("for read all dependencies befor anothers press c");
                    Console.WriteLine("for update an dependency befor another press d");
                    Console.WriteLine("for delete an dependency befor another press e");
                    x = char.Parse(Console.ReadLine()!);
                    InfoOfDependencies(x);

                    //doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine()!);
        }
    }
}


