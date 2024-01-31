

using DalApi;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal

{
    // Step 3: Public static property for accessing the single instance
    public static IDal Instance => LazyInstance.Value;

    // Step 5: Make the constructor private
    private DalXml() { }


    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();

   

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

    // Step 4: Add a private static instance with lazy initialization
    private static readonly Lazy<IDal> LazyInstance = new Lazy<IDal>(() => new DalXml());

    public void Reset()
    {
        Engineer.Delete();
        Task.Delete();
        Dependency.Delete();
    }
}

