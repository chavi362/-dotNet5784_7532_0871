
namespace BO
{
    internal class Dependency
    {
        public int Id { get; init; }
        public int DependentTask { get; set; }
        public int DependensOnTask { get; set; }
    }
}
