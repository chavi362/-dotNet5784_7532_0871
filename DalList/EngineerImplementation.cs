using DalApi;
using DO;

namespace Dal;


public class EngineerImplementation : IEngineer
{
    int IEngineer.Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    void IEngineer.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Engineer? IEngineer.Read(int id)
    {
        throw new NotImplementedException();
    }

    List<Engineer> IEngineer.ReadAll()
    {
        throw new NotImplementedException();
    }

    void IEngineer.Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
