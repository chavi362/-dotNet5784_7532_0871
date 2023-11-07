
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

            case 'a'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
                Console.WriteLine("enter alias's id to add");
                string alias = Console.ReadLine();
                Console.WriteLine("enter task's name");
                string description = Console.ReadLine();
                DO.Task task = new(4, description, alias);
                try
                {

                    int result = ITask.create(p);
                    Console.WriteLine("the task was added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter product's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalProduct.read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the tasks:");
                Product[] arrReadAllTasks = dalProduct.readAll();
                foreach (var item in arrReadAllProducts)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of product to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalProduct.read(idUpdate));
                    Product pUpdate = new Product();
                    pUpdate._id = idUpdate;
                    Console.WriteLine("enter product's name");
                    pUpdate._name = Console.ReadLine();
                    Console.WriteLine("enter product's price");
                    pUpdate._price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                    pUpdate._category = (ECategory)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's instock");
                    pUpdate._inStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's parve(0/1)");
                    pUpdate._parve = int.Parse(Console.ReadLine());
                    dalProduct.update(pUpdate);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete a product
                Console.WriteLine("enter id of product to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalProduct.delete(idDelete);
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

            case 'a'://add                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            t = new Task();
                Console.WriteLine("enter engineer's id to add");
                int id = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter engineer's name");
                string name = Console.ReadLine()!;
                Console.WriteLine("enter engineer's email");
                string email = Console.ReadLine()!;
                DO.Engineer myEngineer = new(id, name, email);
                try
                {
                    int result = s_dalEngineer.Create(myEngineer);
                    Console.WriteLine("the engineer was added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter engineer's id to read");
                int idRead = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(s_dalEngineer.Read(idRead));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the engineers:");
                List<DO.Engineer> arrReadAllEngineers = s_dalEngineer.ReadAll();
                foreach (var item in arrReadAllEngineers)
                    Console.WriteLine(item);
                break;
        }


            case 'd'://update
            Console.WriteLine("enter id of engineer to update");
            int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
            try
            {
                Console.WriteLine("enter engineer's id to update");
                int id = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter engineer's name");
                string name = Console.ReadLine()!;
                Console.WriteLine("enter engineer's email");
                string email = Console.ReadLine()!;
                Console.WriteLine("enter engineer's level from 0- to 2");
                int? level=int.Parse(Console.ReadLine()!);
                EngineerExperience? enLevel;
                try
                {
                    enLevel = (EngineerExperience)level;
                }
                catch
                {
                    Console.WriteLine("I tell you to put between 0 to 2");
                }
                Console.WriteLine("enter engineer's cost");
                double cost=double.Parse(Console.ReadLine()!);
                DO.Engineer myEngineer = new(id, name, email, enLevel, cost);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            break;
        case 'e'://delete a product
            Console.WriteLine("enter id of product to delete");
            int idDelete = int.Parse(Console.ReadLine());
            try
            {
                dalProduct.delete(idDelete);
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
        Console.WriteLine("for dependencies press 2");
        Console.WriteLine("for engeeners 3");
        Console.WriteLine("for exit press 0");
        int select = int.Parse(Console.ReadLine());
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
                    x = char.Parse(Console.ReadLine());
                    InfoOfTask(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an order press a");
                    Console.WriteLine("for read an order press b");
                    Console.WriteLine("for read all orders press c");
                    Console.WriteLine("for update an order press d");
                    Console.WriteLine("for delete an order press e");
                    x = char.Parse(Console.ReadLine());
                    InfoOfEngineers(x); //doing this function 
                    break;
                case 3:
                    Console.WriteLine("for add an item in order press a");
                    Console.WriteLine("for read item in order press b");
                    Console.WriteLine("for read all items in orders press c");
                    Console.WriteLine("for update an item in order press d");
                    Console.WriteLine("for delete an item in order press e");
                    Console.WriteLine("for read an item in order by id of order and product press f");
                    Console.WriteLine("for read an items in order press g");
                    x = char.Parse(Console.ReadLine());
                    InfoOfOrderItem(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine());
        }
    }
}


