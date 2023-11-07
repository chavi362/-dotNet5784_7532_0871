
namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency d)
    {
       if((DataSource.Dependencies).Find(dep=>dep.DependentTask==dep.DependentTask && dep.DependensOnTask==dep.DependensOnTask)!=null)
                 throw new Exception($"Dependency  is already exists");
        if ((DataSource.Dependencies).Find(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
            throw new Exception($"This doesn't realistic!");
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = d with { Id = id };
  
        DataSource.Dependencies.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        Dependency? toDelate = Read(id);
        if (toDelate == null)
        {
            throw new Exception($"Dependency with ID={id} is not exists");
        }
        else
        {
            DataSource.Dependencies.Remove(toDelate);
        }

    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(x => x.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency d)
    {
        if (Read(d.Id) == null)
        {
            throw new Exception($"Dependencies with ID={d.Id} is not exist");
        }
        else
        {
            DataSource.Dependencies.Remove(Read(d.Id)!);
            DataSource.Dependencies.Add(d);
        }
    }
   
}
