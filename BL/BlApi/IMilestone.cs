

using BO;

namespace BlApi
{
    public interface IMilestone
    {
        public int Create(BO.Milestone item);
        public int Update(BO.Milestone item);
        public int Delete(int id);
        public BO.Milestone? Read(int id);
        public IEnumerable<BO.Milestone> ReadAll(Func<Milestone, bool>? filter = null);
    }
}
