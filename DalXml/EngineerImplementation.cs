

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

        // Check if Engineer with the same ID already exists
        if (Read(item.Id) == null)
        {
            engineersList.Add(item);
            XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
            return item.Id;
        }

        throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.Id} already exists");
    }

    public void Delete(int? id=null)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        XElement? tasksElement = XMLTools.LoadListFromXMLElement("tasks");
        IEnumerable<XElement>? taskElementsEnumerable = null;
        if (tasksElement != null)
        {
            taskElementsEnumerable = tasksElement.Elements("Task");
        }
       // List<Task> taskList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (id == null)
        {
            List<Engineer> emptyList = new List<Engineer>();
            XMLTools.SaveListToXMLSerializer<Engineer>(emptyList, "engineers");
            return;
        }
        Engineer? toDelete = Read((int)id);
        if (toDelete != null&& taskElementsEnumerable != null)
        {
            if (taskElementsEnumerable!.FirstOrDefault(x =>
    int.TryParse(x.Element("EngineerId")?.Value, out var engineerId) && // Parse EngineerId
    engineerId == id &&
    DateTime.TryParse(x.Element("Start")?.Value, out var startDate) && // Parse Start date
    startDate < DateTime.Now) != null)  
                throw new DalDeletionImpossible($"Engineer with ID={id} has some tasks");
            else
            {
                engineersList.Remove(toDelete);//remove from tha data base}
                XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
            }
        }
        else
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} doern't exists");
        }
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineersList.FirstOrDefault(engineer => engineer.Id == id);
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineersList.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter != null)
        {
            return from item in engineersList
                   where filter(item)
                   select item;
        }
        return from item in engineersList
               select item;
    }

    public void Update(Engineer engineer)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? prev = Read(engineer.Id);//checking if there is this engineer
        if (prev is null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} doern't exists");
        }
        engineersList.Remove(prev);//remove from the data
        engineersList.Add(engineer);//add the new one
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
    }
}
