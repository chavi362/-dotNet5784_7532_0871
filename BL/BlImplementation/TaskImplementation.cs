

using BlApi;

namespace BlImplementation
{
    internal class TaskImplementation : ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(BO.Task item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Task? Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Task> ReadAll(Func<System.Threading.Tasks.Task, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public int Update(BO.Task item)
        {
            throw new NotImplementedException();
        }
    }
}
