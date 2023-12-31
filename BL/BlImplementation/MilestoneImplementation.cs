

using BlApi;
using BO;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(Milestone item)
        {
            
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Milestone? Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Milestone> ReadAll(Func<Milestone, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public int Update(Milestone item)
        {
            throw new NotImplementedException();
        }
    }
}
