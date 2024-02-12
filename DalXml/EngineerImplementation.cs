using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dal
{
    /// <summary>
    /// Implementation of the engineer-related operations in the data access layer using XML for persistence.
    /// </summary>
    internal class EngineerImplementation : IEngineer
    {
        /// <summary>
        /// Creates a new engineer in the XML data store.
        /// </summary>
        /// <param name="item">The engineer to create.</param>
        /// <returns>The ID of the created engineer.</returns>
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

        /// <summary>
        /// Deletes an engineer from the XML data store.
        /// </summary>
        /// <param name="id">The ID of the engineer to delete. If null, deletes all engineers.</param>
        public void Delete(int? id = null)
        {
            List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            XElement? tasksElement = XMLTools.LoadListFromXMLElement("tasks");
            IEnumerable<XElement>? taskElementsEnumerable = null;
            if (tasksElement != null)
            {
                taskElementsEnumerable = tasksElement.Elements("Task");
            }

            if (id == null)
            {
                List<Engineer> emptyList = new List<Engineer>();
                XMLTools.SaveListToXMLSerializer<Engineer>(emptyList, "engineers");
                return;
            }

            Engineer? toDelete = Read((int)id);

            if (toDelete != null && taskElementsEnumerable != null)
            {
                if (taskElementsEnumerable!.FirstOrDefault(x =>
                        int.TryParse(x.Element("EngineerId")?.Value, out var engineerId) && // Parse EngineerId
                        engineerId == id &&
                        DateTime.TryParse(x.Element("Start")?.Value, out var startDate) && // Parse Start date
                        startDate < DateTime.Now) != null)
                {
                    throw new DalDeletionImpossible($"Engineer with ID={id} has some tasks");
                }
                else
                {
                    engineersList.Remove(toDelete);
                    XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
                }
            }
            else
            {
                throw new DalDoesNotExistException($"Engineer with ID={id} doesn't exist");
            }
        }

        /// <summary>
        /// Reads an engineer from the XML data store based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the engineer to read.</param>
        /// <returns>The engineer with the given ID, or null if not found.</returns>
        public Engineer? Read(int id)
        {
            List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            return engineersList.FirstOrDefault(engineer => engineer.Id == id);
        }

        /// <summary>
        /// Reads an engineer from the XML data store based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to apply on the engineers.</param>
        /// <returns>The engineer based on the provided filter, or null if not found.</returns>
        public Engineer? Read(Func<Engineer, bool> filter)
        {
            List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            return engineersList.FirstOrDefault(item => filter(item));
        }

        /// <summary>
        /// Reads all engineers from the XML data store.
        /// </summary>
        /// <param name="filter">An optional filter to apply on the engineers.</param>
        /// <returns>An enumerable of engineers based on the provided filter.</returns>
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

        /// <summary>
        /// Updates an engineer in the XML data store.
        /// </summary>
        /// <param name="engineer">The engineer to update.</param>
        public void Update(Engineer engineer)
        {
            List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            Engineer? prev = Read(engineer.Id);//checking if there is this engineer
            if (prev is null)
            {
                throw new DalDoesNotExistException($"Engineer with ID={engineer.Id} doesn't exist");
            }
            engineersList.Remove(prev);//remove from the data
            engineersList.Add(engineer);//add the new one
            XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
        }
    }
}
