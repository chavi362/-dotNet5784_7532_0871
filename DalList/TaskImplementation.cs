

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using static Dal.DataSource;

internal class TaskImplementation : ITask
{
    public int Create(Task t)//add a task
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = t with { Id = id };//puting the Running ID number
        DataSource.Tasks.Add(copy);//add the task to the data
        return id;
    }

    public void Delete(int? id=null)//erase a task
    {
        if (id == null)
            DataSource.Tasks.Clear();
        Task? toDelate = Read((int)id!);//find the task with the id we got
        if (toDelate == null)
        {
            throw new DalDoesNotExistException($"Task with ID={id} is not exist");
        }
        else
        {
            if (!CheckingDependency(toDelate))//if we can erase the task
                throw new DalDeletionImpossible($"another task dependth on thid task with ID={id}");
            if (Config.projectBegining == null)
                throw new DalDeletionImpossible("the project alredy began");
            DeletingTaskDependency(toDelate);
            DataSource.Tasks.Remove(toDelate);//remove the task from the data
        }

    }

    public Task? Read(int id)//find the task with the id we got
    {
        return DataSource.Tasks.FirstOrDefault(x => x.Id == id);

    }

    //public List<Task> ReadAll()//read all the tasks
    //{
    //    return new List<Task>(DataSource.Tasks);
    //}
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }
    public void Update(Task t)//change somes in a task
    {

        if (Read(t.Id) == null)//first find the task
        {
            throw new DalDoesNotExistException($"Task with ID={t.Id} is not exist");
        }
        else
        {
            DataSource.Tasks.Remove(t);//erase it from the data
            DataSource.Tasks.Add(t);//add the new one
        }
    }
    public bool CheckingDependency(Task t)//looking if the task depend on another tasks
    {
        if (DataSource.Dependencies.FirstOrDefault(x => x.DependensOnTask == t.Id) == null)//finding dependency
            return true;
        return false;
    }
    public void DeletingTaskDependency(Task t)//delete all the dependencies with this tassk
    {
        DataSource.Dependencies.RemoveAll(x => x.DependentTask == t.Id);
        //  //    DataSource.Dependencies = DataSource.Dependencies
        //  //.Where(x => x.DependentTask != t.Id)
        //  //.Select(x => new Task { /* Copy relevant properties here */ })
        //  //.ToList(); 
        //  var list = DataSource.Dependencies
        //  .Where(x => x.DependentTask != t.Id)
        //  .ToList().Fo
        //// DataSource.Dependencies = list;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(item=>filter(item));
    }
}
