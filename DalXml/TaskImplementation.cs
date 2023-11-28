

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    int ICrud<Task>.Create(Task item)
    {
        throw new NotImplementedException();
    }

    void ICrud<Task>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Task? ICrud<Task>.Read(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Task?> ICrud<Task>.ReadAll(Func<Task, bool>? filter)
    {
        throw new NotImplementedException();
    }

    void ICrud<Task>.Update(Task item)
    {
        throw new NotImplementedException();
    }
}
