

namespace BlApi
{
    public interface ITask
    {
        public int Create(BO.Task item);
        public int Update(BO.Task item);    
        public int Delete(int id);
        public BO.Task? Read(int id);
        public IEnumerable<BO.Task> ReadAll(Func<Task, bool>? filter = null);

    }
}
