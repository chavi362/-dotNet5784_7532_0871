

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    int ICrud<Dependency>.Create(Dependency item)
    {
        throw new NotImplementedException();
    }

    void ICrud<Dependency>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Dependency? ICrud<Dependency>.Read(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Dependency?> ICrud<Dependency>.ReadAll(Func<Dependency, bool>? filter)
    {
        throw new NotImplementedException();
    }

    void ICrud<Dependency>.Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
