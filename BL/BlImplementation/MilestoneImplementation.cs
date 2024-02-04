

using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void SetStartDate(DateTime? startDate)
        {
            _dal.ProjectStartDate = startDate;
        }
        public void SetEndDate(DateTime? endDate)
        {
            _dal.ProjectEndDate = endDate;
        }
        public void CreateProjectSchedule()
        {
            int milestoneIndex = 0;
            List<DO.Dependency> newDependencies = new List<DO.Dependency>();
            IEnumerable<DO.Task> tasks = _dal.Task.ReadAll()!;
            IEnumerable<DO.Dependency> dependenciesList = _dal.Dependency.ReadAll()!;
            // Find tasks that do not have dependencies they will depend on start
            var tasksWithoutDependencies = tasks.Where(task => !dependenciesList.Any(dependency => dependency.DependentTask == task.Id));
            int milestoneId = _dal.Task.Create(new DO.Task()
            {
                Description = $"milestone{milestoneIndex++}",
                Alias = $"M{milestoneIndex}",
                Milestone = true,
                CreatedAtDate = DateTime.Now
            });
            newDependencies.AddRange(tasksWithoutDependencies.Select(t => new DO.Dependency(0, t.Id, milestoneId)));
            var taskDependencies = (from dependency in dependenciesList
                                    where dependency?.DependentTask != null && dependency?.DependensOnTask != null
                                    group dependency by dependency.DependentTask
                        into dependencyListAfterGrouping
                                    let dependencyList = dependencyListAfterGrouping
                                                          .Select(dependency => dependency.DependensOnTask)
                                                          .OrderBy(dependency => dependency)
                                                          .ToList()
                                    select new { _key = dependencyListAfterGrouping.Key, _value = dependencyList })
                        .ToList();
            foreach (var dependency in taskDependencies)
            {
                milestoneId = _dal.Task.Create(new DO.Task()
                {
                    Description = $"milestone{milestoneIndex++}",
                    Alias = $"M{milestoneIndex++}",  // Use the same mindex for alias
                    Milestone = true,
                    CreatedAtDate = DateTime.Now
                });
                //newDependencies.AddRange(item.Select(d => new DO.Dependency(0, milestoneId, d)));
                ////הוספת תלויות של כל המשימות שתלויות באבן הדרך החדשה
                //newDependencies.AddRange(dependenciesGroups.Where(g => g.DependsOn.SequenceEqual(item))
                //    .Select(g => new DO.Dependency(0, g.Key, milestoneId)));
            }
            // Adding Milestone for project completing
            milestoneId = _dal.Task.Create(new DO.Task()
            {
                Description = $"milestone{milestoneIndex++}",
                Alias = "End",
                Milestone = true,
                CreatedAtDate = DateTime.Now
            });
            IEnumerable<DO.Task?> EndTasks = _dal.Task.ReadAll(t => t.Milestone == false && dependenciesList.All(d => d.DependensOnTask != t.Id));
            newDependencies.AddRange(EndTasks.Select(t => new DO.Dependency(0, milestoneId, t!.Id)));
            //Replace the dependencies list
            foreach (DO.Dependency dependency in dependenciesList)
            {
                _dal.Dependency.Delete(dependency.Id);
            }
            foreach (DO.Dependency dependency in newDependencies)
            {
                _dal.Dependency.Create(dependency);
            }
        }
        private BO.Status GetStatus(DO.Task task)
        {
            return task.Complete != null ? Status.InJeopardy
                : task.Start != null ? Status.OnTrack

                : Status.Unscheduled;
        }

        private void updateDeadLines(DO.Task milestone, DateTime ProjectDeadLine)
        {
            if (milestone.DeadLineDate == null || milestone.DeadLineDate > ProjectDeadLine)
            {
                _dal.Task.Update(new DO.Task(
                    milestone.Id,
                    milestone.Description,
                    milestone.Alias,
                    milestone.Milestone,
                   milestone.RequiredEffortTime, 
                    milestone.CreatedAtDate,
                    milestone.Start,
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

                    foreach (DO.Task task in _dal.Task.ReadAll(t => _dal.Dependency.ReadAll().Any(d => d?.DependentTask == milestone.Id && d.DependensOnTask == t.Id)))
                    {
                        if (task!.DeadLineDate == null || task.DeadLineDate > ProjectDeadLine)
                        {

                            _dal.Task.Update(new DO.Task(
                                task.Id,
                                 task.Description,
                                  task.Alias, 
                                  task.Milestone, 
                                  task.RequiredEffortTime, 
                                  task.CreatedAtDate, 
                                  task.Start,
                                    task.Forecast,
                            ProjectDeadLine.Subtract(task.RequiredEffortTime!.Value),//עדכון תאריך התחלה
                  //עדכון תאריך אחרון לסיום
                            task.Complete, 
                            task.Deliverables,
                            task.Remarks, 
                            task.EngineerId, 
                            task.ComplexityLevel));

                            milestoneId = _dal.Dependency.Read(d => d.DependentTask == task.Id)!.DependensOnTask;
                            milestoneDeadLineDate = ProjectDeadLine.Subtract(task.RequiredEffortTime ?? TimeSpan.Zero);
                            if (milestones.ContainsKey(milestoneId))
                            {
                                if (milestones[milestoneId] > milestoneDeadLineDate)
                                    milestones[milestoneId] = milestoneDeadLineDate;
                            }
                            else
                                milestones.Add(milestoneId, milestoneDeadLineDate);
                        }
                    }
                }
            }

        }

        public Milestone? Read(int id)
        {
            try
            {
                var doTask = _dal.Task.Read(id);
                var dependencies = _dal.Dependency.ReadAll(d => d.DependentTask == id);
                IEnumerable<TaskInList>? tasksInList = doTask!.Milestone && dependencies != null ? dependencies.Select(d => ReadTaskInList(d!.DependensOnTask)) : null;

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
                throw new BO.BlDoesNotExistException($"Task with id: {id} does not exist", exception);
            }
        }


        private BO.TaskInList ReadTaskInList(int id)
        {
            try
            {
                var doTask = _dal.Task.Read(id);
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
                throw new BO.BlDoesNotExistException($"Task with id: {id} does not exist", exception);
            }
        }

        public int Update(Milestone item)
        {
            if (item.Alias == null)
                throw new BO.BlInvalidPropertyException("invalid Alias");
            Milestone? milestone = Read(item.Id);
            milestone!.Alias = item.Alias;
            milestone.Description = item.Description;
            milestone.Remarks = item.Remarks;

            try
            {
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
                    milestone.Complete, // Use Complete instead of CompleteDate
                    null,
                    milestone.Remarks,
                    null,
                    null
                ));
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Milestone with ID={item.Id} does Not exist, you can't update it", ex);
            }

            return milestone.Id;
        }


    }

    }
