
namespace DalTest;
using DO;
using DalApi;
//const int MIN_ID = 0;
public static class Initialization
{
    private static IDal? s_dal; //stage 2
    private static readonly Random s_rand = new();
    private static void createTasks()
    {//tamples with description & alias
        (string description, string Alias)[] engineerTasks =
        {
            ("Design the project architecture", "design"),
            ("Develop the backend logic","Develop"),
            ("Implement the user interface","Implement"),
            ("Perform testing and debugging","debug"),
            ("Optimize the code for performance","Optimizing"),
            ("Document the project specifications","Doucument"),
            ("Collaborate with team members","collaborating"),
            ("Attend project meetings","meetings"),
            ("Review and provide feedback on code","code review"),
            ("Deploy the project to production","production"),
            ("Maintain and support the project","support")
        };
        foreach (var taskData in engineerTasks)
        {

            bool isMilestone = s_rand.Next(2) == 0;//lottery true or false
            DateTime startDate = DateTime.Now.AddDays(s_rand.Next(1, 3));//lottery start
            DateTime estimatedCompletionDate = startDate.AddDays(s_rand.Next(5, 15));//adding days to the start
            DateTime deadlineDate = startDate.AddDays(s_rand.Next(5, 10)); //adding days to the start
            DateTime finalCompletionDate = startDate.AddDays(s_rand.Next(5, 35)); //adding days to the start

            Task newTask = new Task(
                0,
                taskData.description,
                taskData.Alias,
                isMilestone,
                TimeSpan.FromDays(1),
            DateTime.Now,
                startDate,
                estimatedCompletionDate,
                deadlineDate,
                finalCompletionDate,
                null,
                null,
                0,
                null
            );

            s_dal!.Task.Create(newTask);//creating task
        }
    }
    private static void createEngineers()
    {

        (string name, string email)[] engineerNamesEmails =
        {//tamles for names&emails
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
                _id = s_rand.Next(200000000, 400000000);//lottery id
            while (s_dal!.Engineer.Read(_id) != null);
            Enum.TryParse<EngineerExperience>((s_rand.Next(0, 3)).ToString(), out level);//lottery engineer level
            double cost = s_rand.Next(100000, 2000000);//lottery cost

            Engineer newEng = new(_id, _name.name, _name.email, level, cost);

            s_dal!.Engineer.Create(newEng);//creating engineer
        }

    }
    private static void createDependencies()
    {
        int numOfTasks = s_dal!.Task.ReadAll().Count();

        for (int dependentTaskId = 0; dependentTaskId < numOfTasks; dependentTaskId++)
        {
            int numOfDependencies = s_rand.Next(1, 4); // Randomly determine the number of dependencies for each task
            for (int i = 0; i < numOfDependencies; i++)
            {
                int dependenciesOnTaskId;
                do
                {
                    dependenciesOnTaskId = s_rand.Next(0, numOfTasks); // Generate a random task ID for the dependency
                } while (dependenciesOnTaskId == dependentTaskId); // Ensure the dependency is not the same task as the dependent task

                Dependency newDependency = new Dependency(0, dependentTaskId, dependenciesOnTaskId);
                try
                {
                    s_dal!.Dependency!.Create(newDependency);
                }
                catch
                {
                }
            }
        }
    }
    public static void Do(IDal dal)
    {
        s_dal = dal ?? throw new NullReferenceException("DAL object cannot be null!"); // Stage 2
        // Call the private methods to initialize the lists
        createTasks();
        createEngineers();
        createDependencies();
    }
}