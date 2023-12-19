

using DO;

namespace BO
{
    public class Task
    {
        public int Id { get; init; }
        public string? Description { get; set; }
        public string? Alias { get; set; }
        public bool Milestone { get; set; }
        public TimeSpan? RequiredEffortTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Forecast { get; set; }
        public DateTime? Complete { get; set; }
        public string? Deliverables { get; set; }
        public string? Remarks { get; set; }
        public EngineerExperience? ComplexityLevel { get; set; }

    }
}
