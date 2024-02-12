

using DO;

namespace BO
{
    public class Milestone
    {
        public int Id { get; init; }
        public required string Description { get; set; }
        public required string Alias { get; set; }
        public DateTime? CreatedAtDate { get; set; }
        public Status? Status { get; set; }
        public DateTime? StartAtDate { get; set; }
        public DateTime? ForecastAtDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? Complete { get; set; }
        public Double ProgressPercentage { get; set; }
        public string? Remarks { get; set; }
        public IEnumerable<BO.TaskInList>? DependenceTasks { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
