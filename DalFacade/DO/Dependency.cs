
namespace DO;

public record Dependency
(
    int Id,
    int DependentTask,
    int DependsOnTask
    )
{
    // Parameterless constructor required for XmlSerializer
    public Dependency() : this(0, 0,0) { }
}

