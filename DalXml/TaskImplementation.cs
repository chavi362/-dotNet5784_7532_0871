 
namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{

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
            new XElement("Remarks", t.Remarks==null? new XAttribute(xsiNamespace + "nil", "true") :
                new XText(t.Remarks.ToString()!)),
            new XElement("EngineerId", t.EngineerId == null ?
                new XAttribute(xsiNamespace + "nil", "true") :
                new XText(t.EngineerId.ToString()!)),
            new XElement("ComplexityLevel", t.ComplexityLevel==null? new XAttribute(xsiNamespace + "nil", "true") :
                new XText(t.ComplexityLevel.ToString()!))
        );

        tasksElements?.Add(newTaskElement);
        tasksElements?.Save(@"..\xml\tasks.xml");


        //List<DO.Task> taskList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        //taskList.Add(t);
        //XMLTools.SaveListToXMLSerializer<DO.Task>(taskList, "tasks");
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
                if (Config.projectBegining >= DateTime.Now)
                    throw new DO.DalDeletionImpossible("the project alredy began");
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
        int? engineerId = null;
        if (taskElement.Element("EngineerId")?.Value !="")
            engineerId = Convert.ToInt32(taskElement.Element("EngineerId")?.Value);
        return new DO.Task
        {
            Id = Convert.ToInt32(taskElement.Element("Id")?.Value),
            Description = taskElement.Element("Description")!.Value,
            Alias = taskElement.Element("Alias")?.Value,
            Milestone = Convert.ToBoolean(taskElement.Element("Milestone")?.Value),
            RequiredEffortTime = taskElement.Element("RequiredEffortTime")?.Value != null
                       ? (TimeSpan?)TimeSpan.Parse(taskElement.Element("RequiredEffortTime")!.Value)
                       : null,
            CreatedAtDate = taskElement.Element("CreatedAtDate")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("CreatedAtDate")!.Value)
                       : null,
            Start = taskElement.Element("Start")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Start")!.Value)
                       : null,
            Forecast = taskElement.Element("Forecast")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Forecast")!.Value)
                       : null,
            DeadLineDate = taskElement.Element("DeadLineDate")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("DeadLineDate")!.Value)
                       : null,
            Complete = taskElement.Element("Complete")?.Value != null
                       ? (DateTime?)DateTime.Parse(taskElement.Element("Complete")!.Value)
                       : null,
            Deliverables = taskElement.Element("Deliverables")?.Value,
            Remarks = taskElement.Element("Remarks")?.Value,
            EngineerId = engineerId,
            ComplexityLevel = taskElement.Element("ComplexityLevel")?.Value !=""
                       ? (DO.EngineerExperience?)Enum.Parse(typeof(DO.EngineerExperience), taskElement.Element("ComplexityLevel")!.Value)
                       : null
        };
    }
}
