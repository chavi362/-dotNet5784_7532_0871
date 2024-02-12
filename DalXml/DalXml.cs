using DalApi;
using System;
using System.Xml.Linq;

namespace Dal
{
    /// <summary>
    /// Implementation of the data access layer using XML for persistence.
    /// </summary>
    internal sealed class DalXml : IDal
    {
        //  Public static property for accessing the single instance
        public static IDal Instance => LazyInstance.Value;

        //  Make the constructor private
        private DalXml() { }

        /// <summary>
        /// Gets the single instance of the DalXml class.
        /// </summary>
        private static readonly Lazy<IDal> LazyInstance = new Lazy<IDal>(() => new DalXml());

        /// <summary>
        /// Gets or sets the project start date from the XML configuration file.
        /// </summary>
        public DateTime? ProjectStartDate
        {
            get
            {
                var value = XDocument.Load(@"..\xml\data-config.xml").Root?.Element("StartProjectDate")?.Value;
                return string.IsNullOrEmpty(value) ? null : DateTime.Parse(value);
            }
            set
            {
                var xDocument = XDocument.Load(@"..\xml\data-config.xml");
                xDocument.Root?.Element("StartProjectDate")?.SetValue(value?.ToString("yyyy-MM-ddTHH:mm:ss")!);
                xDocument.Save(@"..\xml\data-config.xml");
            }
        }

        /// <summary>
        /// Gets or sets the project end date from the XML configuration file.
        /// </summary>
        public DateTime? ProjectEndDate
        {
            get
            {
                var value = XDocument.Load(@"..\xml\data-config.xml").Root?.Element("FinishProjectDate")?.Value;
                return string.IsNullOrEmpty(value) ? null : DateTime.Parse(value);
            }
            set
            {
                var xDocument = XDocument.Load(@"..\xml\data-config.xml");
                xDocument.Root?.Element("FinishProjectDate")?.SetValue(value?.ToString("yyyy-MM-ddTHH:mm:ss")!);
                xDocument.Save(@"..\xml\data-config.xml");
            }
        }

        /// <summary>
        /// Gets the implementation of the engineer-related operations in the data access layer.
        /// </summary>
        public IEngineer Engineer => new EngineerImplementation();

        /// <summary>
        /// Gets the implementation of the task-related operations in the data access layer.
        /// </summary>
        public ITask Task => new TaskImplementation();

        /// <summary>
        /// Gets the implementation of the dependency-related operations in the data access layer.
        /// </summary>
        public IDependency Dependency => new DependencyImplementation();

        /// <summary>
        /// Resets the data in the data access layer, deleting all engineers, tasks, and dependencies.
        /// </summary>
        public void Reset()
        {
            Engineer.Delete();
            Task.Delete();
            Dependency.Delete();
        }
    }
}
