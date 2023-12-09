

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency d)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        // Check if the dependency already exists
        if (dependenciesList.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
            throw new DalAlreadyExistsException($"Dependency already exists");

        // Check if the dependency is realistic
        if (dependenciesList.FirstOrDefault(dep => dep.DependentTask == d.DependensOnTask && dep.DependensOnTask == d.DependentTask) != null)
            throw new LogicException($"This doesn't seem realistic!");

        // Generate a new ID for the dependency
        int id = Config.NextDependencyId;
        Dependency copy = d with { Id = id };

        // Add the new dependency to the data source
        dependenciesList.Add(copy);

        // Save the updated list back to the XML file
        XMLTools.SaveListToXMLSerializer<Dependency>(dependenciesList, "dependencies");
        // Return the ID of the new dependency
        return id;
    }


    public void Delete(int id)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        Dependency? toDelete = Read(id);

        // If the dependency does not exist, throw an exception
        if (toDelete == null)
        {
            throw new DalDeletionImpossible("Dependency with ID ={ id} does not exist");
        }
        else
        {
            // Remove the dependency from the data source
            dependenciesList.Remove(toDelete);
            XMLTools.SaveListToXMLSerializer<Dependency>(dependenciesList, "dependencies");
        }
    }



    public Dependency? Read(int id)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependenciesList.FirstOrDefault(x => x.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        return dependenciesList.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        if (filter != null)
        {
            return from item in dependenciesList
                   where filter(item)
                   select item;
        }
        return from item in dependenciesList
               select item;
    }

    public void Update(Dependency d)
    {
        List<Dependency> dependenciesList = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        if (Read(d.Id) == null)
        {
            throw new DalDoesNotExistException(" Dependency with ID ={ d.Id}does not exist");
        }
        else
        {
            // Check if the updated dependency already exists
            if (dependenciesList.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
                throw new DalAlreadyExistsException("Dependency already exists");

            // Check if the updated dependency is realistic
            if ((dependenciesList).FirstOrDefault(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
                throw new LogicException(" This dependency is not realistic");

            // Remove the old dependency from the data source
            dependenciesList.Remove(Read(d.Id)!);

            // Add the updated dependency to the data source
            dependenciesList.Add(d);
            XMLTools.SaveListToXMLSerializer<Dependency>(dependenciesList, "dependencies");
        }
    }
}
