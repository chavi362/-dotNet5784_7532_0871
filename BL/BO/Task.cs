

using DO;

namespace BO
{
    public class Task
    {
        public int Id { get; init; }
        public required string Description { get; set; }
        public required string Alias { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public Status? Status { get; set; }
        public List<BO.TaskInList>? DependenceList { get; set; }
        public Tuple< int,string>? Milestone { get; set; }
        public DateTime? BaselineStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string? Deliverables { get; set; }
        public string? Remarks { get; set; }
        public BO.EngineerInTask? Engineer { get; set; }
        public EngineerExperience? ComplexityLevel { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
