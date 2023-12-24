

using DO;

namespace BO
{
    public class Milestone
    {
        public int Id { get; init; }
        public string Description { get; set; }
        public string Alias { get; set; }/* = string.Empty*/
        public DateTime? CreatedAt { get; set; }
        public DateTime? ForecastAtDate { get; set; }
        public DateTime? Complete { get; set; }
        public string? Deliverables { get; set; }
        public string? Remarks { get; set; }
        public EngineerExperience? ComplexityLevel { get; set; }

    }
}
