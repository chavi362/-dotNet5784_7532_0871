
namespace DO;

public record Dependency
(
    int Id,
    int DependentTask,
    int DependensOnTask
    )
{
    // Parameterless constructor required for XmlSerializer
    public Dependency() : this(0, 0,0) { }
}

