

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    int ICrud<Engineer>.Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    void ICrud<Engineer>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Engineer? ICrud<Engineer>.Read(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Engineer?> ICrud<Engineer>.ReadAll(Func<Engineer, bool>? filter)
    {
        throw new NotImplementedException();
    }

    void ICrud<Engineer>.Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
