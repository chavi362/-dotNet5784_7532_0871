

using BlApi;
using BO;
using System.Collections.Generic;

namespace BlImplementation
{
    internal class TaskImplementation : ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(BO.Task item)
        {
            if (item.Id < 0 || item.Alias == "")
                throw new ArgumentException("the item is not valid");
            try
            {
                item.DependenceList!.ForEach((dependency) =>
                {
                    _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id));
                });
                int? idEngineer = null;
                if(item.Engineer!=null) {
                    idEngineer = item.Engineer.Id; 
                }
                _dal.Task.Create(new DO.Task(
                    0,
                    item.Description,
                    item.Alias,
                    false,  // Assuming the default value for Milestone is false
                    null,   // Assuming the default value for RequiredEffortTime is null
                    DateTime.Today,  // Assuming CreatedAt is set to the current date
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
                throw new BlDeletionImpossible($"task with id {id} has some dependencies tasks or the project already began",ex);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException($"task with id {id} doesnt exist", ex);
            }
            return id;
        }
        public Status GetTaskStatus(DO.Task doTask)
        {
            if (doTask.Complete != null && doTask.Complete <= DateTime.Now)
            {
                return Status.Complete;
            }

            if (doTask.Start == null || doTask.Forecast == null || doTask.DedLine == null)
            {
                return Status.Unscheduled;
            }

            if (doTask.Start > DateTime.Now)
            {
                return Status.Scheduled;
            }

            if (DateTime.Now <= doTask.DedLine)
            {
                return Status.OnTrack;
            }

            return Status.InJeopardy;
        }
        public BO.Task? Read(int id)
        {
            DO.Task? doTask = _dal.Task.Read(id);

            if (doTask == null)
                throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist", null!);
            EngineerInTask? engineerInTask = null;
            if (doTask?.EngineerId != null)
                engineerInTask = new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name };
            return new BO.Task
            {
                Id = doTask!.Id,
                 Alias = doTask.Alias!,
                 Description = doTask.Description,
                CreatedAtDate = doTask.CreatedAt ?? DateTime.MinValue,
                Status = GetTaskStatus(doTask),
                DependenceList = _dal.Dependency
                    .ReadAll((dependency) => dependency.DependensOnTask == doTask.Id)
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
                         })
                        .ToList(),
                //Milestone = doTask.Milestone
                //? new BO.MilestoneInTask(doTask.Id, doTask.Alias) // Set Milestone based on some condition
                //: ,
                BaselineStartDate = doTask.CreatedAt,
                StartDate = doTask.Start,
                ForecastDate = doTask.Forecast,
                DeadlineDate = doTask.DedLine,
                CompleteDate = doTask.Complete,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer =  engineerInTask,
                ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel!
            };
        }


        public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
        {
            IEnumerable<BO.Task> tasks = _dal.Task.ReadAll().Select(doTask =>
            {
                EngineerInTask? engineerInTask = null;

                if (doTask?.EngineerId != null)
                {
                    engineerInTask = new BO.EngineerInTask
                    {
                        Id = (int)doTask.EngineerId,
                        Name = _dal.Engineer.Read((int)doTask.EngineerId)!.Name
                    };
                }

                return new BO.Task
                {
                    Id = doTask!.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias!,
                    CreatedAtDate = doTask.CreatedAt ?? DateTime.MinValue,
                    Status = GetTaskStatus(doTask),
                    DependenceList = _dal.Dependency
                        .ReadAll(dependency => dependency.DependensOnTask == doTask.Id)
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
                        })
                        .ToList(),
                    BaselineStartDate = doTask.CreatedAt,
                    StartDate = doTask.Start,
                    ForecastDate = doTask.Forecast,
                    DeadlineDate = doTask.DedLine,
                    CompleteDate = doTask.Complete,
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks,
                    Engineer = engineerInTask,
                    ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel!
                };
            });
            if (filter == null)
                return tasks;
            return tasks.Where(filter);
        }



        public int Update(BO.Task item)
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
                    DateTime.Today,  // Assuming CreatedAt is set to the current date
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
            return item.Id;
        }
    }
}
