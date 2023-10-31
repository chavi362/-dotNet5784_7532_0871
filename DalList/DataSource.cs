

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
        }
        internal static List<DO.Task> Tasks { get; } = new();
        internal static List<DO.Engineer> Engineers { get; } = new();
        internal static List<DO.Dependency> Dependencies { get; } = new();
    }
   



}

