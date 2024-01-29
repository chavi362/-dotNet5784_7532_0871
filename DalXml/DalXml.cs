

using DalApi;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml : IDal

{
    // Step 3: Public static property for accessing the single instance
    public static IDal Instance => LazyInstance.Value;

    // Step 5: Make the constructor private
    private DalXml() { }


    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

    public DateTime? ProjectStartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime? ProjectEndDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



    // Step 4: Add a private static instance with lazy initialization
    private static readonly Lazy<IDal> LazyInstance = new Lazy<IDal>(() => new DalXml());

    public void Reset()
    {
        Engineer.Delete();
        Task.Delete();
        Dependency.Delete();
    }
}

