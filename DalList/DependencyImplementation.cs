
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    int IDependency.Create(Dependency item)
    {
        throw new NotImplementedException();
    }

    void IDependency.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Dependency? IDependency.Read(int id)
    {
        throw new NotImplementedException();
    }

    List<Dependency> IDependency.ReadAll()
    {
        throw new NotImplementedException();
    }

    void IDependency.Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
