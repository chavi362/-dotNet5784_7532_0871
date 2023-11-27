namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    // Create a new dependency
    public int Create(Dependency d)
    {
        // Check if the dependency already exists
        if (DataSource.Dependencies.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
            throw new DalAlreadyExistsException($"Dependency is already exists");

        // Check if the dependency is realistic
        if ((DataSource.Dependencies).FirstOrDefault(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
            throw new LogicException($"This doesn't realistic!");

        // Generate a new ID for the dependency
        int id = DataSource.Config.NextDependencyId;

        // Create a copy of the dependency with the new ID
        Dependency copy = d with { Id = id };

        // Add the new dependency to the data source
        DataSource.Dependencies.Add(copy);

        // Return the ID of the new dependency
        return id;
    }

    // Delete a dependency by ID
    public void Delete(int id)
    {
        // Find the dependency with the given ID
        Dependency? toDelete = Read(id);

        // If the dependency does not exist, throw an exception
        if (toDelete == null)
        {
            throw new DalDeletionImpossible( "Dependency with ID ={ id} does not exist");
        }
        else
        {
            // Remove the dependency from the data source
            DataSource.Dependencies.Remove(toDelete);
        }
    }

    // Read a dependency by ID
    public Dependency? Read(int id)
    {
        // Find the dependency with the given ID and return it
        return DataSource.Dependencies.FirstOrDefault(x => x.Id == id);
    }
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(item => filter(item));
    }
    //Read all dependencies
    //public List<Dependency> ReadAll()
    //{
    //    // Create a new list containing all dependencies from the data source
    //    //return new List<Dependency>(DataSource.Dependencies);
    //}
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage  2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    // Update a dependency
    public void Update(Dependency d)
    {
        // Check if the dependency exists
        if (Read(d.Id) == null)
        {
            throw new DalDoesNotExistException(" Dependency with ID ={ d.Id}does not exist");
        }
        else
        {
            // Check if the updated dependency already exists
            if (DataSource.Dependencies.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
                throw new DalAlreadyExistsException("Dependency already exists");

            // Check if the updated dependency is realistic
            if ((DataSource.Dependencies).FirstOrDefault(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
                throw new LogicException(" This dependency is not realistic");

            // Remove the old dependency from the data source
            DataSource.Dependencies.Remove(Read(d.Id)!);

            // Add the updated dependency to the data source
            DataSource.Dependencies.Add(d);
        }
    }
}