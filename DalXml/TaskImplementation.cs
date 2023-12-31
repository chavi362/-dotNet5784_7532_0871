 
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{

    public int Create(DO.Task t)
    {
        int id = Config.NextTaskId;
        const string tasksFile = @"..\xml\tasks.xml";
        XElement? tasksElements = XDocument.Load(tasksFile).Root;
        XElement newTaskElement = new XElement("Task",
            new XElement("Id", id),
            new XElement("Description", t.Description),
            new XElement("Alias", t.Alias),
            new XElement("Milestone", t.Milestone),
            new XElement("RequiredEffortTime", t.RequiredEffortTime.ToString()),
            new XElement("CreatedAt", t.CreatedAt),
            new XElement("Start", t.Start),
            new XElement("Forecast", t.Forecast),
            new XElement("DedLine", t.DedLine),
            new XElement("Complete", t.Complete),
            new XElement("Deliverables", t.Deliverables),
            new XElement("Remarks", t.Remarks),
            new XElement("EngineerId", t.EngineerId),
            new XElement("ComplexityLevel", t.ComplexityLevel));
        tasksElements?.Add(newTaskElement);
        tasksElements?.Save(@"..\xml\tasks.xml");
        return id;
    }
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
                if (Config.projectBegining == null)
                    throw new DalDeletionImpossible("the project alredy began");
                if (!CheckingDependency(taskToRemove))
                    throw new DalDeletionImpossible($"Another task depends on this task with ID={id}");

                DeletingTaskDependency(taskToRemove);
                taskToRemove.Remove();
            }
            else
            {
                throw new DalDoesNotExistException($"Task with ID={id} does not exist");
            }
        }

        tasks?.Save(@"..\xml\tasks.xml");
    }


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

    public void DeletingTaskDependency(XElement t)
    {
        XElement? tasks = XDocument.Load(@"..\xml\tasks.xml").Root;
        int taskId = Convert.ToInt32(t.Element("Id")?.Value);

        // Remove all dependencies where this task is the DependentTask
        tasks?.Elements().Where(x => Convert.ToInt32(x?.Element("DependsOnTask")?.Value) == taskId).Remove();
        tasks?.Save(@"..\xml\tasks.xml");
    }

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
            new XElement("CreatedAt", t.CreatedAt),
            new XElement("Start", t.Start),
            new XElement("Forecast", t.Forecast),
            new XElement("DedLine", t.DedLine),
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
            throw new DalDoesNotExistException($"Task with ID={t.Id} does not exist");
    }

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

    private DO.Task ConvertToTask(XElement taskElement)
    {
        return new DO.Task
        {
            Id = Convert.ToInt32(taskElement.Element("Id")?.Value),
            Description = taskElement.Element("Description")!.Value,
            Alias = taskElement.Element("Alias")?.Value,
            Milestone = Convert.ToBoolean(taskElement.Element("Milestone")?.Value),
            RequiredEffortTime = taskElement.Element("RequiredEffortTime")?.Value != null
                       ? (TimeSpan?)TimeSpan.Parse(taskElement.Element("RequiredEffortTime")!.Value)
                       : null,
            CreatedAt = taskElement.Element("CreatedAt")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("CreatedAt")!.Value)
                       : null,
            Start = taskElement.Element("Start")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Start")!.Value)
                       : null,
            Forecast = taskElement.Element("Forecast")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Forecast")!.Value)
                       : null,
            DedLine = taskElement.Element("DedLine")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("DedLine")!.Value)
                       : null,
            Complete = taskElement.Element("Complete")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Complete")!.Value)
                       : null,
            Deliverables = taskElement.Element("Deliverables")?.Value,
            Remarks = taskElement.Element("Remarks")?.Value,
            EngineerId = Convert.ToInt32(taskElement.Element("EngineerId")?.Value),
            ComplexityLevel = taskElement.Element("ComplexityLevel")?.Value !=""
                       ? (EngineerExperience?)Enum.Parse(typeof(EngineerExperience), taskElement.Element("ComplexityLevel")!.Value)
                       : null
        };
    }
}
