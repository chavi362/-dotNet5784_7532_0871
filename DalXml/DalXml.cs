

using DalApi;

namespace Dal;

sealed public class DalXml : IDal

{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

    public void Reset()
    {
        Engineer.Delete();
        Task.Delete();
        Dependency.Delete();
    }
}
