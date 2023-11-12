namespace Dal;
using DalApi;
using DO;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    // Create a new dependency
    public int Create(Dependency d)
    {
        // Check if the dependency already exists
        if (DataSource.Dependencies.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
            throw new Exception($"Dependency is already exists");

        // Check if the dependency is realistic
        if ((DataSource.Dependencies).Find(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
            throw new Exception($"This doesn't realistic!");

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
            throw new Exception( "Dependency with ID ={ id} does not exist");
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
        return DataSource.Dependencies.Find(x => x.Id == id);
    }

    // Read all dependencies
    public List<Dependency> ReadAll()
    {
        // Create a new list containing all dependencies from the data source
        return new List<Dependency>(DataSource.Dependencies);
    }

    // Update a dependency
    public void Update(Dependency d)
    {
        // Check if the dependency exists
        if (Read(d.Id) == null)
        {
            throw new Exception(" Dependency with ID ={ d.Id}does not exist");
        }
        else
        {
            // Check if the updated dependency already exists
            if (DataSource.Dependencies.Any(dep => dep.DependentTask == d.DependentTask && dep.DependensOnTask == d.DependensOnTask))
                throw new Exception("Dependency already exists");

            // Check if the updated dependency is realistic
            if ((DataSource.Dependencies).Find(dep => dep.DependentTask == dep.DependensOnTask && dep.DependensOnTask == dep.DependentTask) != null)
                throw new Exception(" This dependency is not realistic");

            // Remove the old dependency from the data source
            DataSource.Dependencies.Remove(Read(d.Id)!);

            // Add the updated dependency to the data source
            DataSource.Dependencies.Add(d);
        }
    }
}