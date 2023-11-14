using DalApi;
using DO;

namespace Dal;


public class EngineerImplementation : IEngineer
{
    public int Create(Engineer engineer)//build a new engineer
    {
        if (Read(engineer.Id) is not null)//checking if the engineer alredy exist
            throw new Exception($"Engineer with ID={engineer.Id} already exists");
        DataSource.Engineers.Add(engineer);//adding to the data list
        return engineer.Id;
    }

    public void Delete(int id)//erase an engineer
    {
        Engineer? toDelete = Read(id);
        if (toDelete != null)
        {
            if (DataSource.Tasks.Find(x => x.EngineerId == id) != null)//checking if we can delete it
                throw new Exception($"Engineer with ID={id} has some tasks");
            else
                DataSource.Engineers.Remove(toDelete);//remove from tha data base
        }
        else
        {
            throw new Exception($"Engineer with ID={id} doern't exists");
        }
    }

    public Engineer? Read(int id)//find the engineer with the id we got
    {
        return DataSource.Engineers.Find(x => x.Id == id);
    }

    public List<Engineer> ReadAll()//read all the engineer
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer engineer)//change some attributes in a emgineer
    {
        Engineer? prev = Read(engineer.Id);//checking if there is this engineer
        if (prev is null)
        {
            throw new Exception($"Engineer with ID={engineer.Id} doern't exists");
        }
        DataSource.Engineers.Remove(prev);//remove from the data
        DataSource.Engineers.Add(engineer);//add the new one

    }
}
