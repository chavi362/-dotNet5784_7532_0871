using DalApi;
using DO;

namespace Dal;


internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer engineer)//build a new engineer
    {
        if (Read(engineer.Id) is not null)//checking if the engineer alredy exist
            throw new DalAlreadyExistsException($"Engineer with ID={engineer.Id} already exists");
        DataSource.Engineers.Add(engineer);//adding to the data list
        return engineer.Id;
    }

    public void Delete(int? id=null)//erase an engineer
    {
        if (id == null)
            DataSource.Engineers.Clear();
        Engineer? toDelete = Read((int)id!);
        if (toDelete != null)
        {
            if (DataSource.Tasks.FirstOrDefault(x => x.EngineerId == id&&x.Start<DateTime.Now) != null)//checking if we can delete it
                throw new DalDeletionImpossible($"Engineer with ID={id} has some tasks");
            else
                DataSource.Engineers.Remove(toDelete);//remove from tha data base
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} doern't exists");
        }
    }

    public Engineer? Read(int id)//find the engineer with the id we got
    {
        return DataSource.Engineers.FirstOrDefault(x => x.Id == id);
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(item => filter(item));
    }
    //public List<Engineer> ReadAll()//read all the engineer
    //{
    //    return new List<Engineer>(DataSource.Engineers);
    //}
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }



    public void Update(Engineer engineer)//change some attributes in a emgineer
    {
        Engineer? prev = Read(engineer.Id);//checking if there is this engineer
        if (prev is null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} doern't exists");
        }
        DataSource.Engineers.Remove(prev);//remove from the data
        DataSource.Engineers.Add(engineer);//add the new one

    }
}
