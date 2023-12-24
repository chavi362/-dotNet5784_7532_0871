using DO;

namespace BO
{
    public class MilestoneInList
    {
        public int Id { get; init; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public string? status { get; set; } = null;
        public double? CompletionPrecentage { get; set; } = null;


    }
}
