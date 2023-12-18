namespace Dal
{
    using DalApi;
    using System;

    internal sealed class DalList : IDal
    {
        // Step 3: Public static property for accessing the single instance
        public static IDal Instance => LazyInstance.Value;

        // Step 5: Make the constructor private
        private DalList() { }

        

        public IEngineer Engineer => new EngineerImplementation();
        public ITask Task => new TaskImplementation();
        public IDependency Dependency => new DependencyImplementation();

        

        // Step 4: Add a private static instance with lazy initialization
        private static readonly Lazy<IDal> LazyInstance = new Lazy<IDal>(() => new DalList());

        public void Reset()
        {
            Engineer.Delete();
            Task.Delete();
            Dependency.Delete();
        }
    }
}
