

namespace Dal
{
    internal static class DataSource
    {
        internal static class Config
        {
            internal const int startTaskId = 1;
            private static int nextTaskId = startTaskId;
            internal const int startDependencyId = 1;
            private static int nextDependencyId = startDependencyId;
            internal static int NextTaskId { get => nextTaskId++; }
            internal static int NextDependencyId { get => nextDependencyId++; }
            internal static DateTime? projectBegining = new DateTime(2023, 1, 1); // Set your desired start date
            internal static DateTime? projectFinishing = new DateTime(2024, 12, 31); // Set your desired end date

        }
        internal static List<DO.Task> Tasks { get; } = new();
        internal static List<DO.Engineer> Engineers { get; } = new();
        internal static List<DO.Dependency> Dependencies { get; } = new();
    }
   



}

