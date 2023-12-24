using DO;

namespace BO
{
    public class TaskInList
    {
        public int Id { get; init; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string? Status { get; set; } = null;
    }
}
