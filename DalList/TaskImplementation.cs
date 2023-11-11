

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task t)
    {

        int id = DataSource.Config.NextTaskId;
        Task copy = t with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        Task? toDelate = Read(id);
        if (toDelate == null)
        {
            throw new Exception($"Task with ID={id} is not exist");
        }
        else
        {
            if(!CheckingDependency(toDelate))
                throw new Exception($"another task dependth on thid task with ID={id}");
            DeletingTaskDependency(toDelate);
            DataSource.Tasks.Remove(toDelate);
        }
        
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(x => x.Id == id);

    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task t)
    {

        if (Read(t.Id) == null)
        {
            throw new Exception($"Task with ID={t.Id} is not exist");
        }
        else
        {
            DataSource.Tasks.Remove(t);
            DataSource.Tasks.Add(t);
        }
    }
    public bool CheckingDependency(Task t)
    {
        if (DataSource.Dependencies.Find(x => x.DependensOnTask == t.Id) == null)
            return true;
        return false;
    }
    public void DeletingTaskDependency(Task t)
    {
        List<Dependency> d = DataSource.Dependencies.FindAll(x => x.DependentTask == t.Id);
        foreach (Dependency dependency in d)
        {
           // Delete(dependency.Id);
        }
    }
}
