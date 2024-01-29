

using BlApi;
using BO;
using DalApi;
using System.Collections.Generic;

namespace BlImplementation
{
    internal class TaskImplementation : BlApi.ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(BO.Task item)
        {
            DO.EngineerExperience? engineerExperience = null;
            if (item.ComplexityLevel != null)
                engineerExperience = (DO.EngineerExperience)item.ComplexityLevel!;
            if (item.Id < 0 || item.Alias == "")
                throw new ArgumentException("the item is not valid");
            try
            {
                //item.DependenceList!.ForEach((dependency) =>
                //{
                //    _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id));
                //});
                int? idEngineer = null;
                if (item.Engineer != null)
                {
                    idEngineer = item.Engineer.Id;
                }
                _dal.Task.Create(new DO.Task(
                    0,
                    item.Description,
                    item.Alias,
                    false,  // Assuming the default value for Milestone is false
                    null,   // Assuming the default value for RequiredEffortTime is null
                    DateTime.Today,  // Assuming CreatedAtDate is set to the current date
                    null,   // Assuming the default value for Start is null
                    item.ForecastDate,
                    item.DeadlineDate,
                    item.CompleteDate,  // Assuming item.CompleteDate corresponds to Complete property
                    item.Deliverables,
                    item.Remarks,
                    idEngineer,
                   engineerExperience
                    ));
            }
            catch (DO.DalAlreadyExistsException)
            {
                throw new Exception("cant add the dependencies for this task");
            }
            return item.Id;
        }
        public int Delete(int id)
        {
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDeletionImpossible ex)
            {
                throw new BO.BlDeletionImpossible($"task with id {id} has some dependencies tasks or the project already began",ex);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"task with id {id} doesnt exist", ex);
            }
            return id;
        }
        public BO.Status GetTaskStatus(DO.Task doTask)
        {
            if (doTask.Complete != null && doTask.Complete <= DateTime.Now)
            {
                return BO.Status.Complete;
            }

            if (doTask.Start == null || doTask.Forecast == null || doTask.DeadLineDate == null)
            {
                return BO.Status.Unscheduled;
            }

            if (doTask.Start > DateTime.Now)
            {
                return BO.Status.Scheduled;
            }

            if (DateTime.Now <= doTask.DeadLineDate)
            {
                return BO.Status.OnTrack;
            }

            return BO.Status.InJeopardy;
        }
        public BO.Task? Read(int id)
        {
            IEnumerable<DO.Dependency> dependencies;
            List<BO.TaskInList>? dependenciesOfTask=null;
            try
            {
                dependencies = _dal.Dependency.ReadAll((dependency) => dependency.DependensOnTask == id)!;
                if(dependencies.Any())
                {
                    dependenciesOfTask = dependencies
                     .Select(dependency =>
                     {
                         DO.Task dependTask = _dal.Task.Read(dependency!.DependentTask)!;

                         return new BO.TaskInList
                         {
                             Id = dependTask.Id,
                             Description = dependTask.Description,
                             Alias = dependTask.Alias!,
                             Status = GetTaskStatus(dependTask)
                         };
                     }).Where(dependTask => dependTask != null) // Filter out null values
                        .ToList();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

          //  BO.EngineerExperience? level = null;
            //  if(doTask.ComplexityLevel!=null)
            //{
            //    level = (BO.EngineerExperience)doTask.ComplexityLevel!;
            //}
            // Retrieve the task information from the data access layer
            DO.Task? doTask = _dal.Task.Read(id);
            // Check if the task with the given ID exists
            if (doTask == null)
                throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist", null!);
            BO.EngineerInTask? engineerInTask = null;
            if (doTask?.EngineerId != null)
                engineerInTask = new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name };
            EngineerExperience? complevel = null;
            if (doTask?.ComplexityLevel != null)
                complevel = (BO.EngineerExperience)doTask.ComplexityLevel!;
                return new BO.Task
            {
                  Id = doTask!.Id,
                 Alias = doTask.Alias!,
                 Description = doTask.Description,
                CreatedAtDate = doTask.CreatedAtDate ?? DateTime.MinValue,
                Status = GetTaskStatus(doTask),
                DependenceList = dependenciesOfTask,
                    //Milestone = doTask.Milestone
                    //? new BO.MilestoneInTask(doTask.Id, doTask.Alias) // Set Milestone based on some condition
                    //: ,
                    BaselineStartDate = doTask.CreatedAtDate,
                    StartDate = doTask.Start,
                    ForecastDate = doTask.Forecast,
                    DeadlineDate = doTask.DeadLineDate,
                    CompleteDate = doTask.Complete,
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks,
                    Engineer = engineerInTask,
                    ComplexityLevel =complevel
                };
        }

        public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
        {
            IEnumerable<BO.Task> tasks = _dal.Task.ReadAll().Select(doTask => Read(doTask!.Id))!;
            if (filter == null)
                return tasks;
            return tasks.Where(filter);
        }
        public void Update(BO.Task item)
        {
            int? idEngineer = null;
            if (item.Engineer != null)
            {
                idEngineer = item.Engineer.Id;
            }
            if (item.Id < 0 || item.Alias == "")
                throw new ArgumentException("the item is not valid");
            try
            {
                item.DependenceList!.ForEach((dependency) =>
                {
                    _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id));
                });
                _dal.Task.Update(new DO.Task(
                    0,
                    item.Description,
                    item.Alias,
                    false,  // Assuming the default value for Milestone is false
                    null,   // Assuming the default value for RequiredEffortTime is null
                    DateTime.Today,  // Assuming CreatedAtDate is set to the current date
                    null,   // Assuming the default value for Start is null
                    item.ForecastDate,
                    item.DeadlineDate,
                    item.CompleteDate,  // Assuming item.CompleteDate corresponds to Complete property
                    item.Deliverables,
                    item.Remarks,
                  idEngineer,
                    (DO.EngineerExperience)item.ComplexityLevel!
                    ));
            }
            catch (DO.DalAlreadyExistsException)
            {
                throw new Exception("cant add the dependencies for this task");
            }
           // return item.Id;
        }
    }
}
