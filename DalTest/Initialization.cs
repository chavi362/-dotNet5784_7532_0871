
namespace DalTest;
using DO;
using DalApi;
const int MIN_ID = 0;
public static class Initialization
{
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;
    private static IEngineer? s_dalEngineer;
    private static readonly Random s_rand = new();
    private static void createTasks()
    {
        (string description, string Alias)[] engineerTasks =
        {
    ("Design the project architecture", "design"),
    ("Develop the backend logic","Develop"),
    ("Implement the user interface","Implement"),
    ("Perform testing and debugging","debug"),
    ("Optimize the code for performance","Optimizing"),
    ("Document the project specifications","Doucument"),
    ("Collaborate with team members","coolubrating"),
    ("Attend project meetings","meetings"),
    ("Review and provide feedback on code","code review"),
    ("Review and provide feedback on code","code review"),
    ("Deploy the project to production","production"),                                                                                                    "
    ("Maintain and support the project","support")
        };
        foreach (var taskData in engineerTasks)
        {
            while (s_dalTask!.Read(taskId) != null);

            bool isMilestone = s_rand.Next(2) == 0;

            DateTime startDate = DateTime.Now.AddDays(s_rand.Next(1, 3));
            DateTime estimatedCompletionDate = startDate.AddDays(s_rand.Next(5, 15));
            DateTime finalCompletionDate = estimatedCompletionDate.AddDays(s_rand.Next(1, 5));

            Task newTask = new Task(
                taskData.description,
                taskData.Alias,
                isMilestone,
                DateTime.Now,
                startDate,
                estimatedCompletionDate,
                finalCompletionDate,
                null,
                null,
                0,
                null
            );

            s_dalTask!.Create(newTask);
        }
    }
        private static void createEngineers()
        {

            (string name, string email)[] engineerNamesEmails =
            {
        ("Dani Levi","DaniLevi@gmail.com"),
        ("Eli Amar","EliAmar@gmail.com"),
        ("Yair Cohen","YairCohen@gmail.com"),
        ("Ariela Levin", "ArielaLevin@gmail.com"),
        ("Dina Klein","DinaKlein@gmail.com"),
        ("Shira Israelof","ShiraIsraelof@gmail.com"),
        ("Chavi Chaimson","ChaviChaimson@gmail.com"),
        ("Avigail Catz","AvigailCatz@gmail.com"),
        ("Chani Levi","ChaniLevi@gmail.com"),
        ("Chani Lev","ChaniLev@gmail.com")
        };

            foreach (var _name in engineerNamesEmails)
            {
                EngineerExperience level;
                int _id;
                do
                    _id = s_rand.Next(200000000, 400000000);
                while (s_dalEngineer!.Read(_id) != null);
                Enum.TryParse<EngineerExperience>((s_rand.Next(0, 3)).ToString(), out level);
                double cost = s_rand.Next(100000, 2000000);

                Engineer newEng = new(_id, _name.name, _name.email, level, cost);

                s_dalEngineer!.Create(newEng);
            }

        }
        private static void createDependencies()
        {
           // Dependency d = new(0, )
        }

    }