using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dal
{
    /// <summary>
    /// Implementation of the task-related operations in the data access layer using XML for persistence.
    /// </summary>
    internal class TaskImplementation : ITask
    {
        /// <summary>
        /// Creates a new task in the XML data store.
        /// </summary>
        /// <param name="t">The task to create.</param>
        /// <returns>The ID of the created task.</returns>
        public int Create(DO.Task t)
        {
            int id = Config.NextTaskId;
            const string tasksFile = @"..\xml\tasks.xml";

            XElement? tasksElements = XDocument.Load(tasksFile).Root;
            XNamespace xsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";  // Define the XML namespace for xsi

            XElement newTaskElement = new XElement("Task",
                new XElement("Id", id),
                new XElement("Description", t.Description),
                new XElement("Alias", t.Alias),
                new XElement("Milestone", t.Milestone),
                new XElement("RequiredEffortTime", t.RequiredEffortTime == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.RequiredEffortTime.ToString()!)),
                new XElement("CreatedAtDate", t.CreatedAtDate),
                new XElement("Start", t.Start == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.Start.ToString()!)),
                new XElement("Forecast", t.Forecast == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.Forecast.ToString()!)),
                new XElement("DeadLineDate", t.DeadLineDate == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.DeadLineDate.ToString()!)),
                new XElement("Complete", t.Complete == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.Complete.ToString()!)),
                new XElement("Deliverables", t.Deliverables == null ? new XAttribute(xsiNamespace + "nil", "true") : new XText(t.Deliverables.ToString()!)),
                new XElement("Remarks", t.Remarks == null ? new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.Remarks.ToString()!)),
                new XElement("EngineerId", t.EngineerId == null ?
                    new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.EngineerId.ToString()!)),
                new XElement("ComplexityLevel", t.ComplexityLevel == null ? new XAttribute(xsiNamespace + "nil", "true") :
                    new XText(t.ComplexityLevel.ToString()!))
            );

            tasksElements?.Add(newTaskElement);
            tasksElements?.Save(@"..\xml\tasks.xml");

            return id;
        }

        /// <summary>
        /// Deletes a task from the XML data store.
        /// </summary>
        /// <param name="id">The ID of the task to delete. If null, deletes all tasks.</param>
        public void Delete(int? id = null)
        {
            XElement tasks = XDocument.Load(@"..\xml\tasks.xml").Root!;

            if (id == null)
            {
                // Delete all tasks
                tasks?.RemoveNodes();
            }
            else
            {
                XElement taskToRemove = tasks?.Elements()
                    .FirstOrDefault(task => Convert.ToInt32(task?.Element("Id")?.Value) == id)!;

                if (taskToRemove != null)
                {
 
                    if (!CheckingDependency(taskToRemove))
                        throw new DO.DalDeletionImpossible($"Another task depends on this task with ID={id}");

                    DeletingTaskDependency(taskToRemove);
                    taskToRemove.Remove();
                }
                else
                {
                    throw new DO.DalDoesNotExistException($"Task with ID={id} does not exist");
                }
            }

            tasks?.Save(@"..\xml\tasks.xml");
        }

        /// <summary>
        /// Checks if there are dependencies on a given task.
        /// </summary>
        /// <param name="t">The task element to check for dependencies.</param>
        /// <returns>True if there are no dependencies, false otherwise.</returns>
        public bool CheckingDependency(XElement? t)
        {
            XElement? tasks = XDocument.Load(@"..\xml\tasks.xml").Root;
            if (tasks?.Elements()
                    .FirstOrDefault(x => Convert.ToInt32(x?.Element("DependsOnTask")?.Value) == Convert.ToInt32(t?.Element("Id")?.Value)) != null)
            {
                return false; // Another task depends on this task
            }

            return true; // No dependencies found
        }

        /// <summary>
        /// Deletes dependencies related to a given task.
        /// </summary>
        /// <param name="t">The task element to delete dependencies for.</param>
        public void DeletingTaskDependency(XElement t)
        {
            XElement? tasks = XDocument.Load(@"..\xml\tasks.xml").Root;
            int taskId = Convert.ToInt32(t.Element("Id")?.Value);

            // Remove all dependencies where this task is the DependentTask
            tasks?.Elements().Where(x => Convert.ToInt32(x?.Element("DependsOnTask")?.Value) == taskId).Remove();
            tasks?.Save(@"..\xml\tasks.xml");
        }

        /// <summary>
        /// Reads a task from the XML data store based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the task to read.</param>
        /// <returns>The task with the given ID, or null if not found.</returns>
        public DO.Task? Read(int id)
        {
            XElement? tasks = XDocument.Load(@"..\xml\tasks.xml").Root;

            if (tasks != null)
            {
                XElement? taskElement = tasks.Elements("Task")
                    .FirstOrDefault(task => Convert.ToInt32(task.Element("Id")?.Value) == id);

                if (taskElement != null)
                {
                    return ConvertToTask(taskElement);
                }
            }

            return null; //Task with the given ID not found
        }

        /// <summary>
        /// Reads all tasks from the XML data store.
        /// </summary>
        /// <param name="filter">An optional filter to apply on the tasks.</param>
        /// <returns>An enumerable of tasks based on the provided filter.</returns>
        public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
        {
            XElement? tasks = XMLTools.LoadListFromXMLElement("tasks");

            if (tasks != null)
            {
                IEnumerable<DO.Task> allTasks = tasks.Elements("Task").Select(ConvertToTask);

                if (filter != null)
                {
                    return allTasks.Where(filter);
                }

                return allTasks;
            }

            return Enumerable.Empty<DO.Task>();
        }

        /// <summary>
        /// Updates a task in the XML data store.
        /// </summary>
        /// <param name="t">The task to update.</param>
        public void Update(DO.Task t)
        {
            const string tasksFile = @"..\xml\tasks.xml";
            XElement? tasksElements = XDocument.Load(tasksFile).Root;
            XElement? taskToRemove = tasksElements?.Elements()
               .FirstOrDefault(task => Convert.ToInt32(task?.Element("Id")?.Value) == Convert.ToInt32(task?.Element("Id")?.Value));

            if (taskToRemove != null)
            {
                Delete(t.Id);
                XElement newTaskElement = new XElement("Task",
                new XElement("Id", t.Id),
                new XElement("Description", t.Description),
                new XElement("Alias", t.Alias),
                new XElement("Milestone", t.Milestone),
                new XElement("RequiredEffortTime", t.RequiredEffortTime),
                new XElement("CreatedAtDate", t.CreatedAtDate),
                new XElement("Start", t.Start),
                new XElement("Forecast", t.Forecast),
                new XElement("DeadLineDate", t.DeadLineDate),
                new XElement("Complete", t.Complete),
                new XElement("Deliverables", t.Deliverables),
                new XElement("Remarks", t.Remarks),
                new XElement("EngineerId", t.EngineerId),
                new XElement("ComplexityLevel", t.ComplexityLevel));
                // Add the new task element back to the XML file
                tasksElements = XDocument.Load(tasksFile).Root;
                tasksElements?.Add(newTaskElement);
                tasksElements?.Save(tasksFile);
            }
            else
                throw new DO.DalDoesNotExistException($"Task with ID={t.Id} does not exist");
        }

        /// <summary>
        /// Reads a task from the XML data store based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to apply on the tasks.</param>
        /// <returns>The task based on the provided filter, or null if not found.</returns>
        public DO.Task? Read(Func<DO.Task, bool> filter)
        {
            XElement? tasks = XDocument.Load(@"..\xml\tasks.xml").Root;

            if (tasks != null)
            {
                XElement? taskElement = tasks.Elements("Task")
                    .FirstOrDefault(task => filter(ConvertToTask(task)));

                if (taskElement != null)
                {
                    return ConvertToTask(taskElement);
                }
            }

            return null; // Task not found based on the provided filter
        }

        /// <summary>
        /// Converts an XML task element to a DO.Task object.
        /// </summary>
        /// <param name="taskElement">The XML task element to convert.</param>
        /// <returns>The converted DO.Task object.</returns>
        private DO.Task ConvertToTask(XElement taskElement)
        {
            int? engineerId = null;
            if (taskElement.Element("EngineerId")?.Value != "")
                engineerId = Convert.ToInt32(taskElement.Element("EngineerId")?.Value);

            return new DO.Task
            {
                Id = Convert.ToInt32(taskElement.Element("Id")?.Value),
                Description = taskElement.Element("Description")!.Value,
                Alias = taskElement.Element("Alias")?.Value,
                Milestone = Convert.ToBoolean(taskElement.Element("Milestone")?.Value),
                RequiredEffortTime = taskElement.Element("RequiredEffortTime")?.Value != ""
                           ? (TimeSpan?)TimeSpan.Parse(taskElement.Element("RequiredEffortTime")!.Value)
                           : null,
                CreatedAtDate = taskElement.Element("CreatedAtDate")?.Value != ""
                           ? (DateTime?)DateTime.Parse(taskElement.Element("CreatedAtDate")!.Value)
                           : null,
                Start = taskElement.Element("Start")?.Value != ""
                           ? (DateTime?)DateTime.Parse(taskElement.Element("Start")!.Value)
                           : null,
                Forecast = taskElement.Element("Forecast")?.Value != ""
                           ? (DateTime?)DateTime.Parse(taskElement.Element("Forecast")!.Value)
                           : null,
                DeadLineDate = taskElement.Element("DeadLineDate")?.Value != ""
                           ? (DateTime?)DateTime.Parse(taskElement.Element("DeadLineDate")!.Value)
                           : null,
                Complete = taskElement.Element("Complete")?.Value != ""
                           ? (DateTime?)DateTime.Parse(taskElement.Element("Complete")!.Value)
                           : null,
                Deliverables = taskElement.Element("Deliverables")?.Value,
                Remarks = taskElement.Element("Remarks")?.Value,
                EngineerId = engineerId,
                ComplexityLevel = taskElement.Element("ComplexityLevel")?.Value != ""
                           ? (DO.EngineerExperience?)Enum.Parse(typeof(DO.EngineerExperience), taskElement.Element("ComplexityLevel")!.Value)
                           : null
            };
        }
    }
}
