using DalApi;
using DO;

namespace Dal;


public class EngineerImplementation : IEngineer
{
    public int Create(Engineer engineer)
    {
         if (Read(engineer.Id) is not null)
            throw new Exception($"Engineer with ID={engineer.Id} already exists");
        DataSource.Engineers.Add(engineer);
        return engineer.Id;
    }

    public void Delete(int id)
    {
        Engineer? toDelete = Read(id);
        if (toDelete != null )
        {
            DataSource.Engineers.Remove(toDelete);
        }
        else
        {
            throw new Exception($"Engineer with ID={id} doern't exists");
        }
    }
    
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(x => x.Id == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer engineer)
    {
        Engineer? prev = Read(engineer.Id);
        if (prev is  null)
        {
            throw new Exception($"Engineer with ID={engineer.Id} doern't exists");
        }
        DataSource.Engineers.Remove(prev);
        DataSource.Engineers.Add(engineer);

    }
}
