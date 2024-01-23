

using BlApi;
using BO;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void CreateProjectSchedule()
        {
            var tasks = _dal.Task.ReadAll();
            var dependencyList=_dal.Dependency.ReadAll();

            var taskDEpendency = (from dependency in dependencyList
                                  where dependency?.DependentTask != null && dependency?.DependensOnTask != null
                                  group dependency by dependency.DependentTask
                                      into dependencyListAfterGrouping
                                  //let dependencyList = (from dependency in dependencyListAfterGrouping
                                  //                      select dependency.DependsOnTask).Order()
                                  //select new { _key = dependencyListAfterGrouping.Key, _value = dependencyList }).ToList();
        }

        public Milestone? Read(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Milestone item)
        {
            throw new NotImplementedException();
        }
        
    }
}
