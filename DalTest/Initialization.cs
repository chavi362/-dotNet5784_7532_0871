

namespace DalTest;
using DO;
using DalApi;
public static class Initialization
{
    private static ITask? s_dalTask; 
    private static IDependency? s_dalDependency; 
    private static IEngineer? s_dalEngineer; 

    private static readonly Random s_rand = new();
    private static void createTasks()
    {
       
    }
    private static void createEngineers()
    {

        string[] engineerNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof",
        "Chavi Chaimson",
        "Avigail Catz",
        "Chani Levi"
    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);

            bool? _b = (_id % 2) == 0 ? true : false;
            Year _year =
            (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));

            Engineer newStu = new(_id, _name, null, _b, _year, _bdt);

            s_dalEngineer!.Create(newStu);
        }

    }
    private static void createDependencies()
    {

    }

}