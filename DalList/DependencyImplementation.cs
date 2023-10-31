
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency d)
    {
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
            throw new Exception($"Dependency with ID={id} is not exist");
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
            DataSource.Dependencies.Remove(d);
            DataSource.Dependencies.Add(d);
        }
    }
   
}
