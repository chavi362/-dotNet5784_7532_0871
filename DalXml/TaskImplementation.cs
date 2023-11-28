

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    int ICrud<Task>.Create(Task item)
    {
        throw new NotImplementedException();
    }

    void ICrud<Task>.Delete(int id)
    {
        XElement? Tasks = XDocument.Load("../tasks.xml").Root;
        Tasks?.Elements().ToList().Find(task => Convert.ToInt32(task?.Element("Id")?.Value) == id)?.Remove();
        Tasks?.Save("../Product.xml");
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
