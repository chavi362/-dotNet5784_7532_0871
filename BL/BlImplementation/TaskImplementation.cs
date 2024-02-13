using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlImplementation
{
    internal class TaskImplementation : BlApi.ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Creates a new task based on the provided details.
        /// </summary>
        /// <param name="item">The task details to create.</param>
        /// <returns>The ID of the newly created task.</returns>
        public int Create(BO.Task item)
        {
            DO.EngineerExperience? engineerExperience = null;
            if (item.ComplexityLevel != null)
                engineerExperience = (DO.EngineerExperience)item.ComplexityLevel!;

            if (item.Id < 0 || string.IsNullOrEmpty(item.Alias))
                throw new ArgumentException("The item is not valid");

            try
            {
                if (item.DependenceList != null)
                {
                    item.DependenceList.ForEach((dependency) =>
                    {
                        _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id));
                    });
                }

                int? idEngineer = item.Engineer?.Id;
                _dal.Task.Create(new DO.Task(
                    0,
                    item.Description,
                    item.Alias,
                    false,
                    null,
                    DateTime.Today,
                    null,
                    item.ForecastDate,
                    item.DeadlineDate,
                    item.CompleteDate,
                    item.Deliverables,
                    item.Remarks,
                    idEngineer,
                    engineerExperience
                ));
            }
            catch (DO.DalAlreadyExistsException)
            {
                throw new Exception("Can't add the dependencies for this task");
            }
            return item.Id;
        }

        /// <summary>
        /// Deletes a task with the given ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>The ID of the deleted task.</returns>
        public int Delete(int id)
        {
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDeletionImpossible ex)
            {
                throw new BO.BlDeletionImpossible($"Task with id {id} has some dependencies tasks or the project already began", ex);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with id {id} doesn't exist", ex);
            }
            return id;
        }

        /// <summary>
        /// Gets the status of a task based on its completion and deadlines.
        /// </summary>
        /// <param name="doTask">The task to determine the status for.</param>
        /// <returns>The status of the task.</returns>
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

        /// <summary>
        /// Reads and retrieves task details for a given task ID.
        /// </summary>
        /// <param name="id">The ID of the task to read.</param>
        /// <returns>The task details.</returns>
        public BO.Task? Read(int id)
        {
            IEnumerable<DO.Dependency> dependencies;
            List<BO.TaskInList>? dependenciesOfTask = null;

            try
            {
                dependencies = _dal.Dependency.ReadAll((dependency) => dependency.DependsOnTask == id)!;

                if (dependencies.Any())
                {
                    dependenciesOfTask = dependencies
                        .Select(dependency =>
                        {
                            DO.Task? dependTask = _dal.Task.Read(dependency!.DependentTask);

                            if (dependTask != null)
                            {
                                return new BO.TaskInList
                                {
                                    Id = dependTask.Id,
                                    Description = dependTask.Description,
                                    Alias = dependTask.Alias!,
                                    Status = GetTaskStatus(dependTask)
                                };
                            }

                            return null;
                        })
                        .OfType<BO.TaskInList>()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            DO.Task? doTask = _dal.Task.Read(id);

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
                BaselineStartDate = doTask.CreatedAtDate,
                StartDate = doTask.Start,
                ForecastDate = doTask.Forecast,
                DeadlineDate = doTask.DeadLineDate,
                CompleteDate = doTask.Complete,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = engineerInTask,
                ComplexityLevel = complevel
            };
        }

        /// <summary>
        /// Reads all tasks based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The list of tasks that satisfy the filter.</returns>
        public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
        {
            IEnumerable<BO.Task> tasks = _dal.Task.ReadAll().Select(doTask => Read(doTask!.Id))!;

            if (filter == null)
                return tasks;

            return tasks.Where(filter);
        }

        /// <summary>
        /// Updates a task based on the provided details.
        /// </summary>
        /// <param name="item">The task details to update.</param>
        public void Update(BO.Task item)
        {
            int? idEngineer = item.Engineer?.Id;

            if (item.Id < 0 || string.IsNullOrEmpty(item.Alias))
                throw new ArgumentException("The item is not valid");

            try
            {
                if (item.DependenceList != null)
                {
                    item.DependenceList!.ForEach((dependency) =>
                    {
                        _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id));
                    });
                }

                DO.EngineerExperience? experience = null;
                if (item.ComplexityLevel != null)
                    experience = (DO.EngineerExperience)item.ComplexityLevel!;

                _dal.Task.Update(new DO.Task(
                    item.Id,
                    item.Description,
                    item.Alias,
                    false,
                    null,
                    DateTime.Today,
                    null,
                    item.ForecastDate,
                    item.DeadlineDate,
                    item.CompleteDate,
                    item.Deliverables,
                    item.Remarks,
                    idEngineer,
                    experience
                ));
            }
            catch (DO.DalAlreadyExistsException)
            {
                throw new Exception("Can't add the dependencies for this task");
            }
        }
    }
}
