namespace Dal
{
    using DalApi;
    using System;
    using System.Reflection.Metadata.Ecma335;

    internal sealed class DalList : IDal
    {
        // Step 3: Public static property for accessing the single instance
        public static IDal Instance => LazyInstance.Value;

        // Step 5: Make the constructor private
        private DalList() { }

        

        public IEngineer Engineer => new EngineerImplementation();
        public ITask Task => new TaskImplementation();
        public IDependency Dependency => new DependencyImplementation();

        public DateTime? ProjectStartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? ProjectEndDate { get => DataSource.Config.projectBegining; set => DataSource.Config.projectBegining = value; }

        // Step 4: Add a private static instance with lazy initialization
        private static readonly Lazy<IDal> LazyInstance = new Lazy<IDal>(() => new DalList());

        public void Reset()
        {
            //Engineer.Delete();
            //Task.Delete();
            //Dependency.Delete();
        }
    }
}
