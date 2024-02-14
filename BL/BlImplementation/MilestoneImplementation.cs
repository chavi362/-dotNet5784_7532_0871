using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        // Set the start date of the project
        public void SetStartDate(DateTime? startDate)
        {
            _dal.ProjectStartDate = startDate;
        }

        // Set the end date of the project
        public void SetEndDate(DateTime? endDate)
        {
            _dal.ProjectEndDate = endDate;
        }

        // Create a schedule for the project based on milestones and dependencies
        public void CreateProjectSchedule()
        {
            try
            {
                int milestoneIndex = 0;
                List<DO.Dependency> newDependencies = new List<DO.Dependency>();
                IEnumerable<DO.Task> tasks = _dal.Task.ReadAll()!;
                IEnumerable<DO.Dependency> dependenciesList = _dal.Dependency.ReadAll()!;

                // Find tasks that do not have dependencies; they will depend on the project's start
                var tasksWithoutDependencies = tasks.Where(task => !dependenciesList.Any(dependency => dependency.DependentTask == task.Id));
                int milestoneId = _dal.Task.Create(new DO.Task()
                {
                    Description = $"milestone{milestoneIndex++}",
                    Alias = $"M{milestoneIndex}",
                    Milestone = true,
                    RequiredEffortTime = TimeSpan.Zero,
                    CreatedAtDate = DateTime.Now
                });
                newDependencies.AddRange(tasksWithoutDependencies.Select(t => new DO.Dependency(0, t.Id, milestoneId)));
                // Group tasks based on their dependencies
                var taskDependencies = dependenciesList.OrderBy(dep => dep?.DependsOnTask)
                .GroupBy(dep => dep?.DependentTask, dep => dep?.DependsOnTask,
                (id, dependency) => new { _key = id, _value = dependency })
                .ToList();
                var dependenciesWithoutDuplicates = taskDependencies;
                for (int i = 0; i < taskDependencies.Count(); i++)
                {
                    var dep = from d in taskDependencies
                              where d._key != taskDependencies[i]._key && d._value.SequenceEqual(taskDependencies[i]._value)
                              select d._key;
                    if (dep.Count() >= 1)
                    {
                        dependenciesWithoutDuplicates.Remove(dependenciesWithoutDuplicates[i]);
                    }
                }

                // Create milestones for tasks with dependencies
                foreach (var dependency in dependenciesWithoutDuplicates)
                {
                    milestoneId = _dal.Task.Create(new DO.Task()
                    {
                        Description = $"milestone{milestoneIndex++}",
                        Alias = $"M{milestoneIndex++}",  // Use the same milestone index for the alias
                        Milestone = true,
                        RequiredEffortTime = TimeSpan.Zero,
                        CreatedAtDate = DateTime.Now
                    });

                    // Find the task that this milestone depends on
                    var dependentTask = tasks.FirstOrDefault(t => t.Id == dependency._key);
                    // If the dependent task is found, create a dependency from it to the milestone
                    if (dependentTask != null)
                    {
                        newDependencies.Add(new DO.Dependency(0, dependentTask.Id, milestoneId));
                    }
                }

                // Adding Milestone for project completion
                milestoneId = _dal.Task.Create(new DO.Task()
                {
                    Description = $"milestone{milestoneIndex++}",
                    Alias = "End",
                    Milestone = true,
                    RequiredEffortTime = TimeSpan.Zero,
                    CreatedAtDate = DateTime.Now
                });

                // Find tasks that do not have dependencies; they will depend on the project's end
                IEnumerable<DO.Task?> EndTasks = _dal.Task.ReadAll(t => t.Milestone == false && dependenciesList.All(d => d.DependsOnTask != t.Id));
                newDependencies.AddRange(EndTasks.Select(t => new DO.Dependency(0, milestoneId, t!.Id)));

                // Replace the existing dependencies list
                foreach (DO.Dependency dependency in dependenciesList)
                {
                    _dal.Dependency.Delete(dependency.Id);
                }

                foreach (DO.Dependency dependency in newDependencies)
                {
                    _dal.Dependency.Create(dependency);
                }
            }
            catch (Exception ex)
            {
                throw new BO.BlFailedCreatingSchedule("one problem: ", ex);
            }

        }

        // Get the status of a task based on its completion and start dates
        private BO.Status GetStatus(DO.Task task)
        {
            return task.Complete != null ? Status.InJeopardy
                : task.Start != null ? Status.OnTrack
                : Status.Unscheduled;
        }

        // Update deadlines of milestones and their dependent tasks
        private void updateDeadLines(DO.Task? milestone, DateTime ProjectDeadLine)
        {
            if (milestone!.DeadLineDate == null || milestone.DeadLineDate > ProjectDeadLine)
            {
                // Update milestone's deadline
                _dal.Task.Update(new DO.Task(
                    milestone.Id,
                    milestone.Description,
                    milestone.Alias,
                    milestone.Milestone,
                    milestone.RequiredEffortTime ?? TimeSpan.Zero,
                    milestone.CreatedAtDate,
                    milestone.Start,
                    milestone.ScheduledDate,
                    milestone.Forecast,
                    ProjectDeadLine,
                    milestone.Complete,
                    milestone.Deliverables,
                    milestone.Remarks,
                    milestone.EngineerId,
                    milestone.ComplexityLevel
                ));

                if (milestone.Alias != "M0")
                {
                    Dictionary<int, DateTime> milestones = new Dictionary<int, DateTime>();
                    int milestoneId;
                    DateTime milestoneDeadLineDate;
                    // Update dependent tasks' deadlines
                    foreach (DO.Task? task in _dal.Task.ReadAll(t => _dal.Dependency.ReadAll().Any(d => d?.DependentTask == milestone.Id && d.DependsOnTask == t.Id)))
                    {
                        if (task!.DeadLineDate == null || task.DeadLineDate > ProjectDeadLine)
                        {
                            _dal.Task.Update(new DO.Task(
                                task.Id,
                                task.Description,
                                task.Alias,
                                task.Milestone,
                                task.RequiredEffortTime ?? TimeSpan.Zero,
                                task.CreatedAtDate,
                                task.Start,
                                task.ScheduledDate,
                                task.Forecast,
                                ProjectDeadLine.Subtract(task.RequiredEffortTime!.Value), // Update start date
                                task.Complete, // Update completion date
                                task.Deliverables,
                                task.Remarks,
                                task.EngineerId,
                                task.ComplexityLevel));

                            milestoneId = _dal.Dependency.Read(d => d.DependentTask == task.Id)!.DependsOnTask;
                            milestoneDeadLineDate = ProjectDeadLine.Subtract(task.RequiredEffortTime ?? TimeSpan.Zero);

                            if (milestones.ContainsKey(milestoneId))
                            {
                                if (milestones[milestoneId] > milestoneDeadLineDate)
                                    milestones[milestoneId] = milestoneDeadLineDate;
                            }
                            else
                                milestones.Add(milestoneId, milestoneDeadLineDate);
                        }

                        foreach (var item in milestones)
                        {
                            updateDeadLines(_dal.Task.Read(item.Key), item.Value);
                        }
                    }
                }
            }
        }
        public Milestone? Read(int id)
        {
            try
            {
                // Read the task from the data access la    yer
                var doTask = _dal.Task.Read(id);

                // Read dependencies for the task
                var dependencies = _dal.Dependency.ReadAll(d => d.DependentTask == id);

                // Check if the task is a milestone and if there are dependencies
                IEnumerable<TaskInList>? tasksInList = doTask!.Milestone && dependencies != null
                    ? dependencies.Select(d => ReadTaskInList(d!.DependsOnTask))
                    : null;

                // Map the data object to business object (Milestone)
                return new BO.Milestone
                {
                    Id = doTask.Id,
                    Description = doTask.Description!,
                    Alias = doTask.Alias!,
                    CreatedAtDate = doTask.CreatedAtDate,
                    ForecastAtDate = doTask.Forecast,
                    Complete = doTask.Complete,
                    Remarks = doTask.Remarks,
                    DependenceTasks = tasksInList,
                    Status = GetStatus(doTask),
                    ProgressPercentage = tasksInList != null && tasksInList.Any()
                        ? tasksInList.Count(task => task.Status == Status.InJeopardy) / (double)tasksInList.Count() * 100
                        : 0
                };
            }
            catch (DO.DalDoesNotExistException exception)
            {
                // Handle the case where the task does not exist
                throw new BO.BlDoesNotExistException($"Task with id: {id} does not exist", exception);
            }
        }

        // Helper method to read and map a task in the list
        private BO.TaskInList ReadTaskInList(int id)
        {
            try
            {
                // Read the task from the data access layer
                var doTask = _dal.Task.Read(id);

                // Map the data object to business object (TaskInList)
                return new TaskInList
                {
                    Id = id,
                    Alias = doTask!.Alias!,
                    Description = doTask.Description,
                    Status = GetStatus(doTask)
                };
            }
            catch (DO.DalDoesNotExistException exception)
            {
                // Handle the case where the task does not exist
                throw new BO.BlDoesNotExistException($"Task with id: {id} does not exist", exception);
            }
        }

        // Update method for Milestone
        public int Update(Milestone item)
        {
            // Check for a null Alias
            if (item.Alias == null)
                throw new BO.BlInvalidPropertyException("invalid Alias");

            // Read the existing milestone
            Milestone? milestone = Read(item.Id);

            // Update the milestone properties
            milestone!.Alias = item.Alias;
            milestone.Description = item.Description;
            milestone.Remarks = item.Remarks;

            try
            {
                // Update the corresponding task in the data access layer
                _dal.Task.Update(new DO.Task(
                    milestone.Id,
                    milestone.Description,
                    milestone.Alias,
                    true,
                    null,
                    milestone.CreatedAtDate,
                    null,
                    null,
                    milestone.ForecastAtDate,
                    milestone.Complete,
                    null,
                    milestone.Remarks,
                    null,
                    null
                ));
            }
            catch (DO.DalDoesNotExistException ex)
            {
                // Handle the case where the milestone does not exist
                throw new BO.BlDoesNotExistException($"Milestone with ID={item.Id} does Not exist, you can't update it", ex);
            }
            // Return the updated milestone's ID
            return milestone.Id;
        }
        public IEnumerable<BO.Milestone> ReadAll()
        {
            IEnumerable<BO.Milestone> milestones = _dal.Task.ReadAll(task => task.Milestone == true).Select(doTask => Read(doTask!.Id))!;
            return milestones;
        }
    }
    
}