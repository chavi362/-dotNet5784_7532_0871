

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
         List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        try
        {
            ((ICrud<Engineer>)this).Read(item.Id);
        }
       catch (DalDoesNotExistException ex) {
            engineersList.Add(item);
            XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
            return item.Id;
        }
         throw new DalAlreadyExistsException($"An object of type Engineer with ID {item.Id} already exists");
    }

    public void Delete(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? toDelete = Read(id);
        if (toDelete != null)
        {
            if (engineersList.FirstOrDefault(x => x.Id == id) != null)//checking if we can delete it
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
        return engineersList.FirstOrDefault(x => x.Id == id);
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
