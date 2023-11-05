using DO;
using DalApi;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using Dal;

namespace DalTest;
class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    public static void Main(string[] args)
    {

        try
        {

            Initialization.Do(s_dalTask,s_dalDependency,s_dalEngineer);
        }
        catch
        {
            Console.WriteLine("error in parameters");
        }
        Console.WriteLine("for product press 1");
        Console.WriteLine("for order press 2");
        Console.WriteLine("for item in order press 3");
        Console.WriteLine("for exit press 0");
        int select = int.Parse(Console.ReadLine());
        char x;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for add a product press a");
                    Console.WriteLine("for read a product press b");
                    Console.WriteLine("for read all products press c");
                    Console.WriteLine("for update a product press d");
                    Console.WriteLine("for delete a product press e");
                    x = char.Parse(Console.ReadLine());
                    InfoOfProduct(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an order press a");
                    Console.WriteLine("for read an order press b");
                    Console.WriteLine("for read all orders press c");
                    Console.WriteLine("for update an order press d");
                    Console.WriteLine("for delete an order press e");
                    x = char.Parse(Console.ReadLine());
                    InfoOfOrder(x); //doing this function 
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


