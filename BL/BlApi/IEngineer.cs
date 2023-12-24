

namespace BlApi
{
    public interface IEngineer
    {
        public int Create(BO.Engineer item);
        public int Update(BO.Engineer item);
        public int Delete(int id);
        public BO.Engineer? Read(int id);
        public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    }
}
