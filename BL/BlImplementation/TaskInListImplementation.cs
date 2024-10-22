﻿using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlImplementation
{
    internal class TaskInListImplementation : ITaskInList
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Reads all tasks in a simplified list form.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The list of tasks in simplified form.</returns>
        public IEnumerable<TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
        {
            IEnumerable<TaskInList> tasks = _dal.Task.ReadAll().Select(doTask =>
                new TaskInList
                {
                    Id = doTask!.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias!,
                    Status = (Status)GetTaskStatus(doTask),
                });

            if (filter == null)
                return tasks;

            return tasks.Where(filter);
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
    }
}
