

using DO;

namespace BO
{
    public class Milestone
    {
        public int Id { get; init; }
        public required string Description { get; set; }
        public required string Alias { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Status? Status { get; set; }
        public DateTime? StartAtDate { get; set; }
        public DateTime? ForecastAtDate { get; set; }
        public DateTime? Complete { get; set; }
        public Double ProgressPercentage { get; set; }
        //public string? Deliverables { get; set; }
        public string? Remarks { get; set; }
        //public EngineerExperience? ComplexityLevel { get; set; }
        List<BO.TaskInList>? DependenceTasks { get; set; }

    }
}
