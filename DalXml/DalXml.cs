

using DalApi;

namespace Dal;

public class DalXml : IDal

{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImlementation();
    public IDependency Dependency => new DependencyImplementation();

}
