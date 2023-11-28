

using DalApi;

namespace Dal;

public class DalXml : IDal


{
    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImlementation();
    public ILink Link => new LinkImplementation();

    public IEngineer Engineer => throw new NotImplementedException();

    public ITask Task => throw new NotImplementedException();

    public IDependency Dependency => throw new NotImplementedException();
}
