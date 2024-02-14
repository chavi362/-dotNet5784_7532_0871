

using BO;

namespace BlApi
{
    public interface IMilestone
    {
        public void CreateProjectSchedule();
//walden think it's wrong
        public int Update(BO.Milestone item);
        //public int Delete(int id);
        public BO.Milestone? Read(int id);
        public void SetStartDate(DateTime? date);
        public void SetEndDate(DateTime? date);
        public IEnumerable<BO.Milestone> ReadAll();
    }
}
